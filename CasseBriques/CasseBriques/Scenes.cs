using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
   
    public abstract class Scenes
    {
        // placer un service locator pour taille d'écran / spriteBatch / son ? 
        public Game game;
        public Rectangle Screen {get; set; }
        private Texture2D TextureFond;
        protected int CamShake;
        private Random rnd; 
        public Scenes(Game pGame) 
        {
            game = pGame;
            Screen = game.Window.ClientBounds;
            TextureFond = game.Content.Load<Texture2D>("FondEcran"); 
            rnd = new Random();
        }
        public abstract void Update();

        public abstract void DrawScene(SpriteBatch pBatch);

        public void Draw(SpriteBatch pBatch)
        {
            // on dessine le fond pour toutes les scènes
            pBatch.Begin();
            pBatch.Draw(TextureFond, new Vector2(0, 0), Color.White);
            pBatch.End();

            // on implémante le camshake pour tous les éléments déssiné a part le fond
            if (CamShake >0)
            {
                int offset = rnd.Next(-4,5); // décallage de la caméra
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

            DrawScene(pBatch);

            pBatch.End();   
        }
    }
}
