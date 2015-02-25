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

            ball = new RigidBody(world, 1f, 1, 3) { Restitution = 1, FrictionKinetic=.1f, FrictionStatic = .2f };
            Fixture fix = new FixtureCircle(0, 0, .2f);
            ball.AddFixture(fix);
            ball.SetVelocity(0, 5);
            world.AddBody(ball);


            // line
            StaticBody body = new StaticBody(world, 0, 9f) { Restitution = 1, FrictionKinetic = .1f, FrictionStatic = .2f};
            fix = new FixturePolygon(1, -5, 6, -2.0f,  6.125f, -1.95f,  6.25f, -1.9f,   6.5f, -1.85f,   6.75f, -1.9f,    7, -2, 11, -5);
            body.AddFixture(fix);
            world.AddBody(body);

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
                ball.Position = new Vector2(1, 3);
                ball.SetVelocity(0, 0);
            }
            Random rnd = new Random();
            if (rnd.Next(0,100) < 1){
                RigidBody b1 = new RigidBody(world, .4f, rnd.Next(1, 5), 5) { Restitution = .6f };
                Fixture fix = new FixtureCircle(0, 0, .5f);
                b1.AddFixture(fix);
               // world.AddBody(b1);
            }

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            world.Update(delta);

            base.Update(gameTime);
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
