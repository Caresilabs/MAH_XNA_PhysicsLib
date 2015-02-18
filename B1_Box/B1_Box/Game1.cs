using System;
using System.Collections.Generic;
using System.Linq;
using FysikLib;
using FysikLib.Fixtures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace B1_Box
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private World world;
        private Controller controller;

        RigidBody slide;
        RigidBody box;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "#BoxHype";
        }


        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // ASSIGN
            float mid = graphics.PreferredBackBufferWidth / 100;
            int toPixels = 50;
            world = new World(new Vector2(0, 9.8f), toPixels);


            // Box
            box = new RigidBody(world, .5f, 2, 1);
            box.Restitution = .5f;
            Fixture fix = new FixtureRectangle(0, 0, 1);
            box.AddFixture(fix);
            box.Rotation = 45;
            world.AddBody(box);


            // Slide
            slide = new StaticBody(world, mid * 1.8f, 7);
            float len = 8;
            fix = new FixturePolygon(0, -5, 0, 0, -mid * 2, 0);
            slide.AddFixture(fix);
            world.AddBody(slide);

            // Init form
            controller = new Controller();
            controller.Show();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameTime.ElapsedGameTime.TotalSeconds == 0) return;

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                box.Position = new Vector2(2, 0);
                box.SetVelocity(5, 0);
            }


            UpdateInput();

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            world.Update(delta);

            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            float degrees = controller.BarDegrees.Value;
            slide.Rotation = degrees;
            box.Rotation = degrees;
            controller.labelDegree.Text = String.Format("Change Angle (Degrees) {0}", degrees);

            float stat = controller.BarStatic.Value / 100f;
            box.FrictionStatic = stat;
            slide.FrictionStatic = stat;
            controller.labelStatic.Text = String.Format("Static Friction (0 - 1.2) {0}", stat);

            float kin = controller.BarKinetic.Value / 100f;
            box.FrictionKinetic = kin;
            slide.FrictionKinetic = kin;
            controller.labelKinetic.Text = String.Format("Kinetic Friction (0 - 1.2) {0}", kin);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            world.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
