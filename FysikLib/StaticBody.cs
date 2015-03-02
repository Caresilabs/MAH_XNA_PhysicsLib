using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FysikLib
{
    public class StaticBody : RigidBody
    {
        public StaticBody(World world, float x = 0, float y = 0) : base(world, 0, x, y)
        {
            Inertia = 0;
            IsStatic = true;
        }
    }
}
