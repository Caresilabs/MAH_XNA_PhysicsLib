using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FysikLib.Fixtures
{
    public abstract class Fixture
    {
        public RigidBody Body { get; set; }

        public Color Color { get; set; }

        public Vector2 Offset { get; private set; }

        public Vector2 Position { get; private set; }

        public Fixture(float offsetX, float offsetY)
        {
            this.Offset = new Vector2(offsetX, offsetY);
            var rand = new Random(DateTime.Now.Millisecond);
            this.Color = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble(), 1);
        }

        public void Update(Vector2 position)
        {
            this.Position = position + Offset;
        }

        public abstract void OnRotate(float angle);

        public abstract Manifold[] UpdateCollision( Fixture fixture);

        public abstract void Draw(SpriteBatch batch);

    }
}
