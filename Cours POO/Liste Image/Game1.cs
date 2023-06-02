﻿using ListeImages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net.Http.Headers;

namespace Liste_Image
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D img;

        //private Bubble image; 
        private BubbleList mesBulles; 
        private Vector2 position;
        private int lScreen;
        public int hScreen;
        private Bubble Bouge; 
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
           
            img = Content.Load<Texture2D>("Bulle");
            //lScreen = GraphicsDevice.Viewport.Width; 
            //hScreen = GraphicsDevice.Viewport.Height;
            //image = new Bubble(Content, 100f,200f); 

            mesBulles = new BubbleList(Content, _graphics) ;
            //position = new Vector2(0, 0);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mesBulles.Move(_graphics);
            mesBulles.Collisions(_graphics);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            mesBulles.Affiche(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}