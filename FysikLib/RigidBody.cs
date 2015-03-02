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

        public float Restitution { get; set; }

        public Vector2 Position { get; set; }

        // rotation

        public Vector2 Velocity { get; set; }

        public float AngularVelocity { get; set; }

        public float Torque { get; set; }

        private float _inertia;
        public float Inertia { get { return _inertia; } set { _inertia = value; InvInertia = value == 0 ? 0 : 1 / value; } }

        public float InvInertia { get; private set; }


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

        public List<Manifold> manifolds = new List<Manifold>();

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
            this.UseCollision = true;

            this.Restitution = 1f;
            this.FrictionKinetic = .25f;
            this.FrictionStatic = .35f;
            this.Torque = 0;
            this.Inertia = .2f;
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

                Vector2 pastVelocity = Velocity;
                Velocity = pastVelocity + new Vector2(sumX, sumY) * delta;
                Position = Position + 0.5f * (pastVelocity + Velocity) * delta;

                AngularVelocity += Torque * InvInertia * (delta / 2.0f);
                Rotation += MathHelper.ToDegrees(AngularVelocity * delta);
                //Velocity += new Vector2(sumX, sumY) * delta;
                //Position += Velocity * delta;

            }

            // Update Collision
            manifolds.Clear();
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

            //foreach (var item in manifolds)
            //    item.Initialize();

            //foreach (var item in manifolds)
            //    item.ApplyImpulse();


            //foreach (var item in manifolds)
            //    item.PositionalCorrection();

            // update fixtures position

        }

        public void UpdateFixtures()
        {
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

        public void ApplyImpulse(Vector2 impulse, Vector2 contact)
        {
            Velocity += InvMass * impulse;
            AngularVelocity += ((InvInertia) * CrossProduct(contact, impulse));
        }

        private float CrossProduct(Vector2 a, Vector2 b )
        {
            return a.X * b.Y - a.Y * b.X;
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
