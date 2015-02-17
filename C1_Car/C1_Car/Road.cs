using FysikLib;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C3.XNA;
using Microsoft.Xna.Framework;
using FysikLib.Fixtures;

namespace C1_Car
{
    public class Road
    {
        float radius = 5f, roadStraightLengt = 10, size = 1.5f;
        Vector2 centrum;
        Segment up, down;
        World world;
        public Road(World world)
        {
            this.world = world;
            UpdateRoad();
        }

        public void Update(float delta)
        {

        }

        public void Draw(SpriteBatch batch)
        {
            batch.DrawLine(up.start - new Vector2(0, size / 2), up.end - new Vector2(0, size / 2), Color.Gray, size);
            batch.DrawLine(down.start + new Vector2(0, size / 2), down.end + new Vector2(0, size / 2), Color.Gray, size);
            batch.DrawHalfCircle(centrum, radius + size/2, 16, Color.Gray, size);
        }

        public void UpdateRoad()
        {
            centrum = new Vector2(Game1.screenWidth / (2 * world.pixelMeterRatio), Game1.screenHeight / (2 * world.pixelMeterRatio));
            up = new Segment(new Vector2(centrum.X - roadStraightLengt, centrum.Y - radius), new Vector2(centrum.X, centrum.Y - radius));
            down = new Segment(new Vector2(centrum.X - roadStraightLengt, centrum.Y + radius-size), new Vector2(centrum.X, centrum.Y + radius-size));
        }

        public Vector2 GetStartPosition()
        {
            return up.start;
        }

        public Vector2 GetLowerRoadPosition()
        {
            return down.start;
        }

        public Vector2 GetCentrum()
        {
            return centrum;
        }

        public void SetRadius(int radius)
        {
            this.radius = radius;
            UpdateRoad();
        }

        public float GetRadius()
        {
            return radius;
        }
    }
}
