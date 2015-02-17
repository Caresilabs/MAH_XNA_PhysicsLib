using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace FysikLib.Fixtures
{
    public class FixturePolygon : Fixture
    {
        public readonly List<Segment> lines;

        public FixturePolygon(float offsetX, float offsetY, params Segment[] line)
            : base(offsetX, offsetY)
        {
            this.lines = line.ToList();
        }

        public FixturePolygon(params float[] points)
            : base(0, 0)
        {
            this.lines = new List<Segment>();

            for (int i = 2; i < points.Length; i += 2)
            {
                lines.Add(new Segment(points[i - 2], points[i - 1], points[i], points[i + 1]));
            }
        }

        public void AddLine(Segment line)
        {
            lines.Add(line);
        }

        public override Manifold[] UpdateCollision(Fixture fixture)
        {
            if (fixture is FixturePolygon)
            {
                Manifold[] manifolds = Collision.Collide(this, (FixturePolygon)fixture);
                return manifolds;
            }
            return null;
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (var line in lines)
            {
                batch.DrawLine(line.start + Position, line.end + Position, Color, .1f);
            }
        }

        public override void OnRotate(float angle)
        {
            foreach (var line in lines)
            {
                line.start = Vector2.Transform(line.start, Matrix.CreateRotationZ(MathHelper.ToRadians(angle)));
                line.end = Vector2.Transform(line.end, Matrix.CreateRotationZ(MathHelper.ToRadians(angle)));
            }
        }
    }

    public class Segment
    {
        public Vector2 start, end;

        public Segment(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public Segment(float sx, float sy, float ex, float ey)
        {
            this.start = new Vector2(sx, sy);
            this.end = new Vector2(ex, ey);
        }

        public Vector2 GetNormal()
        {
            var len = (end - start).Length();
            float dx = (end.X - start.X) / len;
            float dy = (end.Y - start.Y) / len;

            //(-dy, dx) and (dy, -dx)
            if (dx < 0)
            {
                return new Vector2(dy, -dx);
            }
            else
            {
                return new Vector2(-dy, dx);
            }
        }

        public float GetRadians()
        {
            return (float)Math.Atan2((end - start).Y, (end - start).X);
        }
    }
}
