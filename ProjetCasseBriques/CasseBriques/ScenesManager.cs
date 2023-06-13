using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public abstract class ScenesManager
    {
        private Texture2D background;
        public Rectangle DimensionEcran { get; set; } 
        // Constructeur 
        public ScenesManager(Game pGame)
        {
            DimensionEcran = pGame.Window.ClientBounds;
            background = pGame.Content.Load<Texture2D>("background");
        }

        public virtual void Update()
        {
        }

        public abstract void DrawScene(); 

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
