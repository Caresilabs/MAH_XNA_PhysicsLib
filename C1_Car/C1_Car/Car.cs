using FysikLib;
using FysikLib.Fixtures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C1_Car
{
    class Car : IGameObject
    {
        RigidBody body;
        private float length = 2, width = 1.2f;
        private float friction, mass, speed;
        private Road road;
        private bool turn, madeTurn;
        private World world;
        public Car(World world, float mass, float friction, Road road, int speed, Texture texture)
        {
            this.world = world;
            this.road = road;
            this.friction = friction;
            this.mass = mass;
            this.speed = speed;

            //Setup body and fixture for car
            body = new RigidBody(world, mass, road.GetStartPosition().X, road.GetStartPosition().Y);

            FixturePolygon fixture = new FixturePolygon(0, 0,
                new Segment(new Vector2(-width / 2, -length / 2), new Vector2(-width / 2, length/2)),
                new Segment(new Vector2(-width / 2, length/2), new Vector2(width/2, length/2)),
                new Segment(new Vector2(width/2, length/2), new Vector2(width/2, -length / 2)),
                new Segment(new Vector2(width / 2, -length / 2), new Vector2(-width / 2, -length / 2)));

            body.AddFixture(fixture);
            body.Rotate(90);
            body.UseGravity = false;
            body.UseCollision = false;
            body.SetVelocity(speed, 0);
            world.AddBody(body);
        }

        public void Update(float delta)
        {
            if (body.Position.X > road.GetCentrum().X)
            {
                turn = true;
                madeTurn = true;
            }
            else if (body.Position.X < road.GetCentrum().X && body.Position.Y < road.GetLowerRoadPosition().Y + 1.5f)
            {
                turn = false;
                //Fix update, for roatating to much
                if(madeTurn)
                {
                    body.Rotation = 270;
                    body.SetVelocity(-speed, 0);
                }
            }

            if (turn)
            {
                float speed = body.Velocity.Length();
                float forceMagnitude = (float)(Math.Pow(speed, 2) / road.GetRadius());

                if (Math.Abs(world.GetGravity().Y * friction) < forceMagnitude)
                    forceMagnitude = Math.Abs(world.GetGravity().Y * friction) * 0.6f;

                Vector2 VectorTowardsCentrum = road.GetCentrum() - body.Position;
                VectorTowardsCentrum.Normalize();

                body.ApplyForce(Forces.FRICTION, VectorTowardsCentrum * forceMagnitude);
                body.Rotation = (float)(-Math.Atan2(body.Velocity.X, body.Velocity.Y) * (180 / Math.PI));
            }
        }

        public void Draw(SpriteBatch batch)
        {
            
        }
    }
}
