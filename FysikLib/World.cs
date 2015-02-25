using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FysikLib
{
    public class World : IGameObject
    {
        public int pixelMeterRatio { get; set; }

        private Vector2 gravity;

        public List<RigidBody> Bodies { get; private set; }

        public World(Vector2 gravity, int pixelMeterRatio)
        {
            this.pixelMeterRatio = pixelMeterRatio;
            this.Bodies = new List<RigidBody>();
            this.gravity = gravity;
        }

        public Vector2 GetGravity()
        {
            return gravity;
        }

        public void AddBody(RigidBody body)
        {
            Bodies.Add(body);
        }

        public void RemoveBody(RigidBody body)
        {
            Bodies.Remove(body);
        }
        public void RemoveAllBodies()
        {
            Bodies.Clear();
        }

        public void Update(float delta)
        {
            foreach (var body in Bodies)
            {
                body.Update(delta);

                foreach (var item in body.manifolds)
                    item.Initialize();

                foreach (var item in body.manifolds)
                    item.ApplyImpulse();


                foreach (var item in body.manifolds)
                    item.PositionalCorrection();

                body.UpdateFixtures();
            }

            // Clear all forces
            foreach (var item in Bodies)
            {
                item.ClearForces();
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, 
                SamplerState.LinearWrap,
                DepthStencilState.None, 
                RasterizerState.CullCounterClockwise,
                null, 
                Matrix.CreateScale(pixelMeterRatio, pixelMeterRatio, 1f));

            foreach (var item in Bodies)
            {
                item.Draw(batch);
            }

            batch.End();
        }
    }
}
