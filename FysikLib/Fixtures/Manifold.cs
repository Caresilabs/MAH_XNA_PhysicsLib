﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FysikLib.Fixtures
{
    public class Manifold
    {
        public Fixture A { get; set; }

        public Fixture B { get; set; }

        private RigidBody BodyA { get; set; }

        private RigidBody BodyB { get; set; }

        public float Penetration { get; set; }

        public Vector2 Normal { get; set; }

        private float RestitutionMix { get; set; }

        private float KineticFriction { get; set; }

        private float StaticFriction { get; set; }


        void Solve()
        {
        }

        public void Initialize()
        {
            BodyA = A.Body;
            BodyB = B.Body;

            StaticFriction = (float)Math.Sqrt((BodyA.FrictionStatic * BodyA.FrictionStatic) + (BodyB.FrictionStatic * BodyB.FrictionStatic));
            KineticFriction = (float)Math.Sqrt((BodyA.FrictionKinetic * BodyA.FrictionKinetic) + (BodyB.FrictionKinetic * BodyB.FrictionKinetic));

            // Calculate restitution
            RestitutionMix = Math.Min(BodyA.Restitution, BodyB.Restitution);
        }

        public void ApplyImpulse()
        {
            //relative velocity
            var diffVelocity = BodyB.Velocity - BodyA.Velocity;

            // Calculate relative velocity in terms of the normal direction
            float velAlongNormal = Vector2.Dot(diffVelocity, Normal);

            // Do not resolve if velocities are separating
            if (velAlongNormal > 0)
                return;

            // Calculate impulse scalar 
            float impulseScalar = -(1 + RestitutionMix) * velAlongNormal; //1
            impulseScalar /= BodyA.InvMass + BodyB.InvMass;

            // Apply impulse
            var impulse = (impulseScalar * Normal);

            // Apply impulse from normal
            //BodyA.ApplyForce(Forces.NORMAL, -impulse / delta);
            BodyA.ApplyImpulse(-impulse);
            BodyB.ApplyImpulse(impulse);



            //  ==== START FRICTION ==== //
            diffVelocity = BodyB.Velocity - BodyA.Velocity; //(BodyB.GetVelocity() + (BodyB.InvMass * impulse)) - (BodyA.GetVelocity() - (BodyA.InvMass * impulse));

            // Solve the tangent vector
            var tangent = diffVelocity - Vector2.Dot(diffVelocity, Normal) * Normal;
            tangent.Normalize();

            // Tangent cannot be NaN
            if (tangent.X.Equals(float.NaN))
                tangent = new Vector2(-1, 0);

            // Solve for magnitude to apply along the friction vector
            float frictionForce = -Vector2.Dot(diffVelocity, tangent);
            frictionForce = frictionForce / (BodyA.InvMass + BodyB.InvMass); // times with mas to get force

            // COEFFIECIENTS! - Use to approximate mu given friction coefficients of each body


            // Check if its a static of kinetic friction
            Vector2 frictionImpulse;
            if (Math.Abs(frictionForce) < impulseScalar * StaticFriction) // if max friction is less than normal impulse * static friction
            {
                // Static
                //frictionAcceleration = (tangent * (BodyA.GetVelocity() / delta)); //jt * tangent;
                frictionImpulse = frictionForce * tangent;
            }
            else
            {
                // Kinetic
                // frictionAcceleration = (impulseScalar * tangent * kineticFriction) / delta;
                frictionImpulse = -impulseScalar * KineticFriction * tangent;
                // BodyA.ApplyForce(Forces.FRICTION, frictionAcceleration);
            }

            // Apply
            BodyA.ApplyImpulse(-frictionImpulse);
            BodyB.ApplyImpulse(frictionImpulse);

            // Position offset for sinking objects. TIME STEP
        }

        public void PositionalCorrection()
        {
            const float percent = 0.999f; // usually 20% to 80%
            const float slop = 0.01f; // usually 0.01 to 0.1
            var correction = (Math.Max(Penetration - slop, 0.0f) / (BodyA.InvMass + BodyB.InvMass)) * percent * Normal;
            correction.X = 0; //TODO NASTY HACK
            BodyA.Position -= BodyA.InvMass * correction;
            BodyB.Position += BodyB.InvMass * correction;
        }

        void InfiniteMassCorrection()
        {
        }

    }
}
