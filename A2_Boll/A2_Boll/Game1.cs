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
using A2_Boll;

namespace Fysik_Projekt
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private World world;
        private RigidBody ball;
        Controller controller;
        int toPixels = 50;

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

            ball = new RigidBody(world, .2f, 2, 3) { Restitution = .8f, FrictionKinetic = .5f, FrictionStatic = 1f };
            Fixture fix = new FixtureCircle(0, 0, .5f);
            ball.AddFixture(fix);
            ball.SetVelocity(0, 5);
            world.AddBody(ball);

            float width = graphics.PreferredBackBufferWidth / toPixels - 1;
            float height = graphics.PreferredBackBufferHeight / toPixels - 1;

            // line
            StaticBody body = new StaticBody(world, 0, height + 1) { Restitution = .05f, FrictionKinetic = .0f, FrictionStatic = .3f };
            //fix = new FixturePolygon(1, -5, 6, -2.0f,  6.125f, -1.95f,  6.25f, -1.9f,   6.5f, -1.85f,   6.75f, -1.9f,    7, -2, 11, -5);
            fix = new FixturePolygon(width, 0, width, -height, 1, -height, 1, 0);
            
            body.AddFixture(fix);
            world.AddBody(body);

            var floorbody = new StaticBody(world, 0, height + 1) { Restitution = .4f, FrictionKinetic = .2f};
            FixturePolygon floor = new FixturePolygon(1.1f, 0, width -.1f, 0);
            floorbody.AddFixture(floor);
            world.AddBody(floorbody);

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
                ball.ApplyImpulse(speed * dir * ball.Mass, new Vector2(0,0f));//ball.Velocity = speed * dir;
               // ball.ApplyForce(Forces.CLICK, force * dir);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ball.Position = new Vector2(2, 3);
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
        float speed = 12;

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
