using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace CasseBriques
{
    internal class SceneGameplay : Scenes
    {
        Texture2D TexRaquette;
        Raquette sprRaquette; 
       
        public SceneGameplay(Game pGame) : base(pGame)
        {

            Rectangle Screen = pGame.Window.ClientBounds; 
            //Contexte.nbVie = 5; on peut utiliser la classe static directement dans nos méthodes
            // = love.graphics.newImage
            // TexRaquette = pGame.Content.Load<Texture2D>("raquette");
            sprRaquette = new Raquette(pGame.Content.Load<Texture2D>("raquette"));
            //sprRaquette.SetPosition (new Vector2(10,10));
            sprRaquette.SetPosition((Screen.Width/2) - (sprRaquette.Width/2), Screen.Height - sprRaquette.Height);
            //sprRaquette.Vitesse = new Vector2(1, 0);
        }

        public override void Update()
        {
            sprRaquette.Update();
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            sprRaquette.Draw(pBatch); 
        }

    }
}
