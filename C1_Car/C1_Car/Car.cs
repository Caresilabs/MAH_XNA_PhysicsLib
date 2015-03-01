using FysikLib;
using FysikLib.Fixtures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C3.XNA;

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
        float forceMagnitude;

        public Car(World world, float mass, float friction, Road road, int speed)
        {
            this.world = world;
            this.road = road;
            this.friction = friction;
            this.mass = mass;
            this.speed = speed;
            this.forceMagnitude = (float)(Math.Pow(speed, 2) / road.GetRadius());

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
                //Start turn
                turn = true;
                madeTurn = true;
            }
            else if (body.Position.X < road.GetCentrum().X)
            {
                //End Turn
                turn = false;
                if(madeTurn)
                {
                    body.Rotation = 270;
                    body.SetVelocity(-speed, 0);
                }
            }

            if (turn)
            {
                //While turn
                float speed = body.Velocity.Length();
                float forceMagnitude = (float)(Math.Pow(speed, 2) / road.GetRadius());

                //If the force needed is more then the friction can handle, then make the car slide out (kinetik = 60% of Static friction)
                if (Math.Abs(world.GetGravity().Y * friction) < this.forceMagnitude)
                    forceMagnitude = Math.Abs(world.GetGravity().Y * friction) * 0.6f;

                //A vector pointing 90 Degress to the cars direction
                Vector2 vectorPerpendicularToCar = -new Vector2((float)Math.Cos(MathHelper.ToRadians(body.Rotation)), (float)Math.Sin(MathHelper.ToRadians(body.Rotation)));
                vectorPerpendicularToCar.Normalize();

                //Applay force to car and roatate car model
                body.ApplyForce(Forces.FRICTION, vectorPerpendicularToCar * forceMagnitude);
                body.Rotation = (float)(-Math.Atan2(body.Velocity.X, body.Velocity.Y) * (180 / Math.PI));
            }
        }

        public void Draw(SpriteBatch batch)
        {

        }
    }
}
