using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace FysikLib.Fixtures
{
    public class FixtureCircle : Fixture
    {
        public float Radius { get; set; }

        public FixtureCircle(float offsetX, float offsetY, float radius)
            : base(offsetX, offsetY)
        {
            this.Radius = radius;
        }

        public override Manifold[] UpdateCollision(Fixture fixture)
        {
            if (fixture is FixturePolygon)
            {
                Manifold[] manifolds = Collision.Collide((FixturePolygon)fixture, this);
                return manifolds;
            }

            if (fixture is FixtureCircle)
            {
                Manifold[] manifolds = Collision.Collide((FixtureCircle)fixture, this);
                return manifolds;
            }
            return null;
        }


        public override void Draw(SpriteBatch batch)
        {
            batch.DrawCircle(Position, Radius, 32, Color.Red, Radius);
        }

        public override void OnRotate(float angle)
        {
        }
    }
}
