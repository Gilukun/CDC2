using Microsoft.Xna.Framework;
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
        // Constructeur 
        public ScenesManager()
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            GraphicsDeviceManager ResolutionEcran = ServiceLocator.GetService<GraphicsDeviceManager>();
            int LargeurEcran = ResolutionEcran.PreferredBackBufferWidth;
            int HauteurEcran = ResolutionEcran.PreferredBackBufferHeight;
            DimensionEcran = new Rectangle(0, 0, LargeurEcran, HauteurEcran);
            background = _content.Load<Texture2D>("background");
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
