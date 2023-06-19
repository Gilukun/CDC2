using Microsoft.Xna.Framework;
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

        // Constructeur 
        public ScenesManager(CasseBriques pGame)
        {
            casseBriques = pGame;
            DimensionEcran = casseBriques.Window.ClientBounds;
            background = casseBriques.Content.Load<Texture2D>("background");
        }

        public virtual void Load()
        { }

        public virtual void Unload()
        { }

        public virtual void Update()
        {
            NewKbState = Keyboard.GetState();

            if (NewKbState.IsKeyDown(Keys.M) && !OldKbState.IsKeyDown(Keys.M))
            {
                casseBriques.gameState.ChangeScene(GameState.Scenes.Menu);
            }
            OldKbState = NewKbState;
        }

        public virtual void DrawScene()
        { 
        }

       public void Draw()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();

            pBatch.Begin();
            pBatch.Draw(background, new Vector2(0,0), Color.White);
            pBatch.End();

            pBatch.Begin();
            DrawScene();
            pBatch.End();
            
        }

    }

    

}
