using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;
using FysikLib.Fixtures;

namespace FysikLib
{
    public enum Forces
    {
        NORMAL, GRAVITY, FRICTION, IMPULSE, CLICK
    }

    public class RigidBody : IGameObject, IBody
    {
        public bool UseGravity { get; set; }

        public bool UseCollision { get; set; }

        public bool IsStatic { get; set; }

        public float FrictionStatic { get; set; }

        public float FrictionKinetic { get; set; }

        public World World { get; private set; }

        public float Mass { get; private set; }

        public float InvMass { get; private set; }

        public float Restitution  { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }


        private readonly Dictionary<Forces, Vector2> forces;

        private List<Fixture> Fixtures { get; set; }

        private float _rotation;
        public float Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                Fixtures.ForEach(x => x.OnRotate(value - _rotation));
                _rotation = value;
            }
        }

        public RigidBody(World world, float mass, float x = 0, float y = 0)
        {
            this.Mass = mass;
            this.InvMass = mass == 0 ? 0 : 1 / mass;
            this.Velocity = new Vector2();
            this.Position = new Vector2(x, y);
            this.forces = new Dictionary<Forces, Vector2>();
            this.Fixtures = new List<Fixture>();
            this.World = world;
            this.UseGravity = true;
            this.IsStatic = false;
            this.Restitution = 1.5f;
            this.UseCollision = true;
            this.FrictionKinetic = .35f;
            this.FrictionStatic = .45f;
        }

        // Loop
        public void Update(float delta)
        {
            if (UseGravity)
                ApplyForce(Forces.GRAVITY, World.GetGravity());


            // Update net force
            if (!IsStatic)
            {
                float sumX = forces.Sum(x => x.Value.X) / (float)Mass;
                float sumY = forces.Sum(x => x.Value.Y) / (float)Mass;

                Velocity += new Vector2(sumX, sumY) * delta;

                Position += Velocity * delta; // fel lennart, RECT MAN!! * 1 / 2f; 
            }

            // Update Collision
            List<Manifold> manifolds = new List<Manifold>();
            if (UseCollision)
            {
                foreach (var fix1 in Fixtures)
                {
                    foreach (var body in World.Bodies)
                    {
                        if (body == this) continue; // Dont check itself

                        foreach (var fix2 in body.Fixtures)
                        {
                            var m = fix1.UpdateCollision(fix2);
                            if (m != null)
                                manifolds.AddRange(m);
                        }
                    }
                }
            }

            foreach (var item in manifolds)
                item.Initialize();

            foreach (var item in manifolds)
                item.ApplyImpulse();


            foreach (var item in manifolds)
                item.PositionalCorrection();

            // update fixtures position
            foreach (var fixture in Fixtures)
                fixture.Update(Position);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (var fixture in Fixtures)
            {
                fixture.Draw(batch);
            }
        }

        public void AddFixture(Fixture fixture)
        {
            fixture.Body = this;
            fixture.Update(Position);
            this.Fixtures.Add(fixture);
        }

        public void Rotate(float degrees)
        {
            Rotation += degrees;
        }

        // === IBODY ==== //

        public void ApplyForce(Forces type, Vector2 acceleration)
        {
            if (forces.ContainsKey(type))
                forces[type] += acceleration * Mass; 
            else
                forces.Add(type, acceleration * Mass);

          //  Velocity += acceleration * 1/60f;
        }

        public void ApplyImpulse(Vector2 impulse)
        {
            Velocity += InvMass * impulse;
        }

        public Vector2 GetForce(Forces type)
        {
            return forces[type];
        }

        public void ClearForces()
        {
            forces.Clear();
        }

        public float GetSpeed()
        {
            return Velocity.Length();
        }

        public void SetVelocity(float x, float y)
        {
            Velocity = new Vector2(x, y);
        }
    }
}
