using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FysikLib.Fixtures
{
    public static class Collision
    {
        public static Manifold[] Collide(FixtureCircle c1, FixtureCircle c2)
        {
            var range = (c1.Position - c2.Position).Length();
            if (range <= c1.Radius + c2.Radius)
            {
                var normal = (c1.Position - c2.Position);
                normal.Normalize();
                return new Manifold[] { new Manifold() { A = c1, B = c2, Normal = normal, Penetration = (c1.Radius + c2.Radius) - range }}; // TODO
            }
            return null ;
        }

        public static Manifold[] Collide(FixturePolygon pol, FixtureCircle circle)
        {
            List<Manifold> list = new List<Manifold>();
            foreach (var line in pol.lines)
            {
                float dst = DistanceFromPointToLineSegment(circle.Position, pol.Position + line.start, pol.Position + line.end);
                if (dst <= circle.Radius)
                {
                    list.Add( new Manifold() { A = circle, B = pol, Normal = line.GetNormal(), Penetration = (circle.Radius - dst) });
                }
            }

            return list.ToArray();
        }

        private static float DistanceFromPointToLineSegment(Vector2 point, Vector2 anchor, Vector2 end)
        {
            Vector2 d = end - anchor;
            float length = d.Length();
            if (d == Vector2.Zero) return (point - anchor).Length();
            d.Normalize();
            float intersect = Vector2.Dot((point - anchor), d);
            if (intersect < 0) return (point - anchor).Length();
            if (intersect > length) return (point - end).Length();
            return (point - (anchor + d * intersect)).Length();
        }

        //Polygon - Polygon collision, returns the Line that collides on the target Plygon
        public static Manifold[] Collide(FixturePolygon pol, FixturePolygon target)
        {
            List<Manifold> list = new List<Manifold>();
            foreach (var line1 in target.lines)
            {
                foreach (var line2 in pol.lines)
                {
                    if (LineIntersectsLine(target.Position + line1.start, target.Position + line1.end, pol.Position + line2.start, pol.Position + line2.end))
                        list.Add( new Manifold()
                        {
                            A = pol,
                            B = target,
                            Normal = line1.GetNormal(),
                            Penetration = Math.Min(DistanceFromPointToLineSegment(line2.end, line1.start, line1.end), DistanceFromPointToLineSegment(line2.start, line1.start, line1.end)) 
                        });
                }
            }
            return list.ToArray();
        }

        private static bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);
            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
    }
}
