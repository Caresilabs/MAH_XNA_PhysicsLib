using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace FysikLib.Fixtures
{
    public class FixtureRectangle : FixtureCircle
    {
        public FixtureRectangle(float offsetX, float offsetY, float radius)
            : base(offsetX, offsetY, radius)
        {
        }

        public override void Draw(SpriteBatch batch)
        {
            var tl = new Vector2(-1, -1);
            var tr = new Vector2(1, -1);
            var br = new Vector2(1, 1);
            var bl = new Vector2(-1, 1);

            tl = Vector2.Transform(tl, Matrix.CreateRotationZ(MathHelper.ToRadians(Body.Rotation)));
            tr = Vector2.Transform(tr, Matrix.CreateRotationZ(MathHelper.ToRadians(Body.Rotation)));
            br = Vector2.Transform(br, Matrix.CreateRotationZ(MathHelper.ToRadians(Body.Rotation)));
            bl = Vector2.Transform(bl, Matrix.CreateRotationZ(MathHelper.ToRadians(Body.Rotation)));

            batch.DrawLine(Position + tl, Position + tr, Color);
            batch.DrawLine(Position + tr, Position + br, Color);
            batch.DrawLine(Position + br, Position + bl, Color);
            batch.DrawLine(Position + bl, Position + tl, Color);
            //batch.DrawRectangle(new Rectangle(tl.X, tl.Y, (bl-tl).X, (bl)), Color, .2f);//(Position, Radius, 32, Color.Red, Radius);
        }

        public override void OnRotate(float angle)
        {
        }
    }
}
