using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing.Text;

namespace Background
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D imgBackground0;
        Texture2D imgBackground1;
        Texture2D imgBackground2;
        Texture2D imgBackground3;

        background background0;
        background background1;
        background background2;
        background background3;



        Vector2 bgposition;

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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            imgBackground0 = this.Content.Load<Texture2D>("urban_scrolling0");
            imgBackground1 = this.Content.Load<Texture2D>("urban_scrolling1");
            imgBackground2 = this.Content.Load<Texture2D>("urban_scrolling2");
            imgBackground3 = this.Content.Load<Texture2D>("urban_scrolling3");

            background0 = new background(-2, imgBackground0);
            background1 = new background(-5, imgBackground1); 
            background2 = new background(-8, imgBackground2); 
            background3 = new background(-10, imgBackground3);



            // bgposition = new Vector2(0,0);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            background0.Update();
            background1.Update();
            background2.Update();
            background3.Update();

            // bgposition.X -= 1;
            // if (bgposition.X <= 0 - imgBackground.Width)
            //bgposition.X = 0;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        private void AfficheBackground(background pBackground)
        {
            _spriteBatch.Draw(pBackground.Image, pBackground.Position, Color.White);
            if (pBackground.Position.X <= 0)
                _spriteBatch.Draw(pBackground.Image, new Vector2(pBackground.Position.X + pBackground.Image.Width, 0), Color.White);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //_spriteBatch.Begin();
            //_spriteBatch.Draw(imgBackground, bgposition, Color.White);

            //if (bgposition.X  <0)
            //{
            //    _spriteBatch.Draw(imgBackground, new Vector2 (bgposition.X + GraphicsDevice.Viewport.Width, 0), Color.White);
            //}
            //_spriteBatch.End();



           

            _spriteBatch.Begin();

            AfficheBackground(background0);
            AfficheBackground(background1);
            AfficheBackground(background2);
            AfficheBackground(background3);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}