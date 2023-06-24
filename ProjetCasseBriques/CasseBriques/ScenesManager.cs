﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class ScenesManager
    {
        private Texture2D background;
        public Rectangle DimensionEcran { get; set; }
        protected CasseBriques casseBriques;
        private KeyboardState NewKbState;
        private KeyboardState OldKbState;
        GameState Status = ServiceLocator.GetService<GameState>();
        GameState GameState;
        private int Sand;
        private float Timer;
        protected int CamShake;
        private Random rnd;
        GameOver GO;

        // Constructeur 
        public ScenesManager()
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            GraphicsDeviceManager ResolutionEcran = ServiceLocator.GetService<GraphicsDeviceManager>();
            int LargeurEcran = ResolutionEcran.PreferredBackBufferWidth;
            int HauteurEcran = ResolutionEcran.PreferredBackBufferHeight;
            DimensionEcran = new Rectangle(0, 0, LargeurEcran, HauteurEcran);
            background = _content.Load<Texture2D>("background");
            rnd = new Random();
        }
         

        public virtual void Load()
        {
            
        }

        public virtual void Unload()
        { }

     
        public virtual void Update()
        {
            NewKbState = Keyboard.GetState();

            if (NewKbState.IsKeyDown(Keys.M) && !OldKbState.IsKeyDown(Keys.M))
            {
                Status.ChangeScene(GameState.Scenes.Menu);
            }
            OldKbState = NewKbState;
  
        }

        public virtual void DrawScene()
        { 
        }
        public virtual void DrawBackground()
        {
        }

        public void Draw()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();

            pBatch.Begin();
            pBatch.Draw(background, new Vector2(0,0), Color.White);
            DrawBackground();
            pBatch.End();

            if (CamShake > 0)
            {
                int offset = rnd.Next(-2, 2); // décallage de la caméra
                pBatch.Begin(SpriteSortMode.Deferred,
                             null,
                             null,
                             null,
                             null,
                             null,
                             Matrix.CreateTranslation(offset, offset, 0f));
                CamShake--;

            }
            else
            pBatch.Begin();
            DrawScene();
           
            pBatch.End();
            
        }

    }

    

}
