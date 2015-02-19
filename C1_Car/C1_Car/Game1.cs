using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FysikLib;

namespace C1_Car
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World world;
        Road road;
        Controller controller;
        List<Car> cars = new List<Car>();

        public static int screenWidth = 1280, screenHeight = 720;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            controller = new Controller(this);
            controller.Show();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = new World(new Vector2(0, -10), 10);      
            road = new Road(world);
        }

        protected override void UnloadContent()
        {

        }

        public void SpawnCar()
        {
            cars.Add(new Car(world, 2000, controller.GetFriction(), road, controller.GetSpeed()));
        }
        public void RemoveCars()
        {
            world.RemoveAllBodies();
            cars.Clear();
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameTime.ElapsedGameTime.TotalSeconds == 0) return;

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            controller.txtRadius.Text = "Radius of curve (m) [" + controller.GetRadius() + "]";
            controller.txtSpeed.Text = "Speed (m/s) [" + controller.GetSpeed() + "]";
            controller.txtFriction.Text = "Coefficient of friction [" + controller.GetFriction() + "]";

            road.SetRadius(controller.GetRadius());

            //Check if crash
            float forceMagnitude = (float)(Math.Pow(controller.GetSpeed(), 2) / controller.GetRadius());
            if (Math.Abs(world.GetGravity().Y * controller.GetFriction()) < forceMagnitude)
                controller.txtMake.Text = "Won't make it!";
            else
                controller.txtMake.Text = "Will make it";

            road.Update(delta);
            foreach(Car car in cars)
                car.Update(delta);
            world.Update(delta);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.LinearWrap,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise,
                null,
                Matrix.CreateScale(world.pixelMeterRatio, world.pixelMeterRatio, 1f));

            road.Draw(spriteBatch);
            foreach (Car car in cars)
                car.Draw(spriteBatch);
            spriteBatch.End();

            world.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
