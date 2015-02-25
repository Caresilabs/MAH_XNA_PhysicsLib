using Microsoft.Xna.Framework;
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


        public void Initialize()
        {
            BodyA = A.Body;
            BodyB = B.Body;

            // COEFFIECIENTS! - Use to approximate mu given friction coefficients of each body
            StaticFriction = (float)Math.Sqrt((BodyA.FrictionStatic * BodyA.FrictionStatic) + (BodyB.FrictionStatic * BodyB.FrictionStatic));
            KineticFriction = (float)Math.Sqrt((BodyA.FrictionKinetic * BodyA.FrictionKinetic) + (BodyB.FrictionKinetic * BodyB.FrictionKinetic));

            // Calculate restitution
            RestitutionMix = Math.Max(BodyA.Restitution, BodyB.Restitution);
                //(float)Math.Sqrt((BodyA.Restitution * BodyA.Restitution) + (BodyB.Restitution * BodyB.Restitution)); //Math.Min(BodyA.Restitution, BodyB.Restitution);
        }

        public void ApplyImpulse()
        {
            //  ==== START COLLISION IMPULSE ==== //

            //relative velocity
            var diffVelocity = BodyB.Velocity - BodyA.Velocity;

            // Calculate relative velocity in terms of the normal direction
            float velAlongNormal = Vector2.Dot(diffVelocity, Normal);

            // Do not resolve if velocities are separating
            if (velAlongNormal > 0)
                return;

            // Calculate impulse scalar 
            float impulseScalar = -( 1 + RestitutionMix) * velAlongNormal; 
            impulseScalar /= (BodyA.InvMass + BodyB.InvMass); // times with mass

            // Apply impulse
            Vector2 impulse = (impulseScalar * Normal);

            // Apply impulse from normal
            BodyA.ApplyImpulse(-impulse);
            BodyB.ApplyImpulse(impulse);


            //  ==== START FRICTION ==== //

            diffVelocity = BodyB.Velocity - BodyA.Velocity; 

            // Solve the tangent vector
            var tangent = diffVelocity - Vector2.Dot(diffVelocity, Normal) * Normal;
            tangent.Normalize();

            // Tangent cannot be NaN
            if (float.IsInfinity(tangent.X) || float.IsInfinity(tangent.Y) || float.IsNaN(tangent.X) || float.IsNaN(tangent.Y))
                tangent = new Vector2(-1, 0);

            // Solve for magnitude to apply along the friction vector
            float frictionForce = -Vector2.Dot(diffVelocity, tangent);
            frictionForce /= (BodyA.InvMass + BodyB.InvMass); // times with mas to get force

            // Check if its a static of kinetic friction
            Vector2 frictionImpulse;
            if (Math.Abs(frictionForce) < impulseScalar * StaticFriction) // if max friction is less than normal impulse * static friction
            {
                // Static
               // BodyA.SetVelocity(0, 0);
               // BodyB.SetVelocity(0,0);

                frictionImpulse = -frictionForce * tangent * KineticFriction; //-impulseScalar * KineticFriction * tangent;

                BodyA.ApplyImpulse(-frictionImpulse);
                BodyB.ApplyImpulse(frictionImpulse);
            }
            else
            {
                // Kinetic
                frictionImpulse = -impulseScalar * KineticFriction * tangent;

                BodyA.ApplyImpulse(-frictionImpulse);
                BodyB.ApplyImpulse(frictionImpulse);
            }
        }

        public void PositionalCorrection()
        {
            const float percent = .9f; // usually 20% to 80%
            const float slop = 0.05f; // usually 0.01 to 0.1
            var correction = (Math.Max(Penetration - slop, 0.0f) / (BodyA.InvMass + BodyB.InvMass)) * percent * Normal;
            correction.X = 0;
            //correction.Y = 0;
            BodyA.Position -= BodyA.InvMass * correction;
            BodyB.Position += BodyB.InvMass * correction;
        }

        void InfiniteMassCorrection()
        {
        }

    }
}
