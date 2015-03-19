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

namespace Fysik_Projekt
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private World world;
        private RigidBody ball;
        private Controller controller;

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

            ball = new RigidBody(world, 1f, 1, 8) { Restitution = .2f };
            Fixture fix = new FixtureCircle(0, 0, .5f);
            ball.AddFixture(fix);
            world.AddBody(ball);


            // line
            StaticBody body = new StaticBody(world, 0, 9f);
            fix = new FixturePolygon(0, 0, new Segment(0, 0, graphics.PreferredBackBufferWidth / toPixels, 0));
            body.AddFixture(fix);
            world.AddBody(body);

            // Form
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

            KeyMouseReader.Update();

            if (KeyMouseReader.KeyPressed(Keys.Space))
                ball.ApplyForce(Forces.CLICK, force * dir);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ball.Position = new Vector2(1, 8);
                ball.SetVelocity(0, 0);
            }

            UpdateInput();

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            world.Update(delta);

            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            float angle = controller.BarDegrees.Value;
            controller.labelDegree.Text = String.Format("Change Angle (Degrees) {0}", angle);

            dir = new Vector2((float)Math.Cos(MathHelper.ToRadians(-angle)), (float)Math.Sin(MathHelper.ToRadians(-angle)));
            dir.Normalize();

            force = (float)controller.BarSpeed.Value;
            controller.labelNewton.Text = String.Format("Force Magnitude (N) {0}N", force);

            if (KeyMouseReader.KeyPressed(Keys.LeftControl))
            {
                ball.Position = Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(Matrix.CreateScale(toPixels, toPixels, 1f)));
                ball.SetVelocity(0, 0);
            }
        }

        Vector2 dir = new Vector2(2, -5);
        float force = 80;

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

            if (Math.Abs(ball.Velocity.Length()) < 2f)
                spriteBatch.DrawLine(ball.Position, ball.Position + (dir * force / 100f), Color.Red, .05f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
