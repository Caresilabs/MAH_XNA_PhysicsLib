using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FysikLib
{
    public interface IBody
    {
        void ApplyForce(Forces type, Vector2 acceleration);

        Vector2 GetForce(Forces type);

        void ClearForces();

        void SetVelocity(float x, float y);

        void ApplyImpulse(Vector2 impulse, Vector2 contact);

        float GetSpeed();
    }
}
