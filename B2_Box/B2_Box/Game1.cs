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
using C3.XNA;

namespace B2_Box
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private World world;
        private RigidBody ball;
        int toPixels = 50;
        Controller controller;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            world = new World(new Vector2(0, 9.8f), toPixels);

            ball = new RigidBody(world, .2f, 1, 3) { Restitution = .5f, FrictionKinetic = .001f, FrictionStatic = .1f };
            Fixture fix = new FixtureCircle(0, 0, .45f);
            ball.AddFixture(fix);
            ball.SetVelocity(0, 5);
            world.AddBody(ball);

            float width = graphics.PreferredBackBufferWidth / toPixels - 1;
            float height = graphics.PreferredBackBufferHeight / toPixels - 1;

            // line
            StaticBody body = new StaticBody(world, 2, height + 1) { Restitution = .05f, FrictionKinetic = .001f, FrictionStatic = .2f };
            fix = new FixturePolygon(-3,-4, 1, -4, 6, -2.0f,  6.125f, -1.95f,  6.25f, -1.9f,   6.5f, -1.85f,   6.75f, -1.9f,    7, -2, 11, -6.5f);
            body.AddFixture(fix);
            world.AddBody(body);

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

            UpdateInput();

            KeyMouseReader.Update();

            dir = Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(Matrix.CreateScale(toPixels, toPixels, 1f))) - ball.Position;
            dir.Normalize();

            if (KeyMouseReader.KeyPressed(Keys.Space))
                ball.Velocity = speed * dir;
            // ball.ApplyForce(Forces.CLICK, force * dir);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ball.Position = new Vector2(1, 3);
                ball.SetVelocity(0, 0);
            }

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            world.Update(delta);

            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            speed = (float)controller.BarSpeed.Value / 50f;
            controller.labelNewton.Text = String.Format("Speed: {0} m/s", speed);

            if (KeyMouseReader.KeyPressed(Keys.LeftControl))
            {
                ball.Position = Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(Matrix.CreateScale(toPixels, toPixels, 1f)));
                ball.SetVelocity(0, 0);
            }
        }


        Vector2 dir = new Vector2();
        float speed = 7;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            world.Draw(spriteBatch);

            spriteBatch.Begin(SpriteSortMode.Deferred,
                 BlendState.AlphaBlend,
                 SamplerState.LinearWrap,
                 DepthStencilState.None,
                 RasterizerState.CullCounterClockwise,
                 null,
                 Matrix.CreateScale(toPixels, toPixels, 1f));

            //if (Math.Abs(ball.Velocity.Length()) < 2f)
            spriteBatch.DrawLine(ball.Position, ball.Position + (dir * speed / 5f), Color.Red, .05f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
