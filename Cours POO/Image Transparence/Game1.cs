using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.WIC;
using System;
using System.Collections.Generic;

namespace Image
{
  

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D img;
        Vector2 position;
        Vector2 position2;
        float coef;
   
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
       
        }

        protected override void Initialize()
        {
            position = new Vector2(100, 100);
            position2 = new Vector2(100, 200);
            coef= 0.01f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice); 
           img= this.Content.Load<Texture2D>("personnage"); 
          
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            coef *=1.02f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           _spriteBatch.Begin();
           _spriteBatch.Draw(img, position, Color.White * coef);
           _spriteBatch.Draw(img, position2, new Color(236, 112, 99));
           _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}