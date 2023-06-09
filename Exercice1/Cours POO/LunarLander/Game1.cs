using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;

namespace LunarLander
{
    public class Lander
    {
        public Vector2 position { get; set; } = Vector2.Zero;
        public Vector2 velocity { get; set; } = Vector2.Zero;
        public float angle { get; set; } = 270;
        public bool engineOn { get; set; } = false;
        public float speed { get; set; } = 0.02f;
        private float speedMax = 2f;

        public Texture2D img { get; set; }
        public Texture2D imgEngine { get; set; }

        public void Update()
        {
            velocity += new Vector2(0, 0.005f);
            if (Math.Abs(velocity.X) > speedMax)
            {
                velocity = new Vector2(
                    (velocity.X < 0 ? 0 - speedMax : speedMax), velocity.Y);  // création d'un if in line (a deux sorties) Estce que Velocity < 0 alors 0 - speedmax. Sinon : speedMax
            }

            if (Math.Abs(velocity.Y) > speedMax)
            { 
                velocity = new Vector2(velocity.X, (velocity.Y < 0 ? 0 - speedMax : speedMax));
            }
            position += velocity; 
        }

    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;

        Lander lander;
     
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            lander = new Lander();
           
            lander.img = Content.Load<Texture2D>("ship");
            lander.imgEngine = Content.Load<Texture2D>("engine");
            lander.position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) 
            {
                lander.angle += 2; 
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                lander.angle -= 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                lander.engineOn = true;
                float angle_radian = MathHelper.ToRadians(lander.angle);
                float forceX = (float)Math.Cos(angle_radian) * lander.speed; 
                float forceY = (float)Math.Sin(angle_radian) * lander.speed;

                lander.velocity += new Vector2(forceX, forceY);
                
            }
            else
            {
                lander.engineOn = false; 
            }

            lander.Update();

            if (lander.position.X < 0)
            {

                lander.position = new Vector2(0, lander.position.Y);
                lander.velocity = new Vector2(-lander.velocity.X, lander.velocity.Y);
            }

            if (lander.position.X > GraphicsDevice.Viewport.Width - lander.img.Width / 2)
            {

                lander.position = new Vector2(GraphicsDevice.Viewport.Width - lander.img.Width / 2, lander.position.Y);
                lander.velocity = new Vector2(-lander.velocity.X, lander.velocity.Y);
            }

            if (lander.position.Y < 0)
            {
                lander.position = new Vector2(lander.position.X, 0);
                lander.velocity = new Vector2(lander.velocity.X, -lander.velocity.Y);
            }

            if (lander.position.Y > GraphicsDevice.Viewport.Height - lander.img.Height / 2)
            {

                lander.position = new Vector2(lander.position.X, GraphicsDevice.Viewport.Height - lander.img.Height / 2);
                lander.velocity = new Vector2(lander.velocity.X, -lander.velocity.Y);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            _spriteBatch.Begin();

            Vector2 origineImg = new Vector2(lander.img.Width / 2, lander.img.Height / 2);

            _spriteBatch.Draw(lander.img,
                                lander.position,
                                null,
                                Color.White,
                                MathHelper.ToRadians(lander.angle), // convertir un angle en radian. 
                                origineImg,
                                new Vector2(1, 1),
                                SpriteEffects.None,
                                0
                            ) ;

            if (lander.engineOn == true)
            {
                Vector2 origineImgEngine = new Vector2(lander.imgEngine.Width / 2, lander.imgEngine.Height / 2);
                _spriteBatch.Draw(lander.imgEngine,
                                    lander.position,
                                    null,
                                    Color.White,
                                    MathHelper.ToRadians(lander.angle), // convertir un angle en radian. 
                                    origineImgEngine,
                                    new Vector2(1, 1),
                                    SpriteEffects.None,
                                    0
                                    );
            }
            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}