using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Briques : Sprites
    {
        private bool bTombe; 
        public int NbCoups { get; set; }
        public bool falling
        { get
            { return bTombe; } 
        }

        public Briques(Texture2D pTexture, Rectangle pScreen) : base(pTexture, pScreen)
        {
            bTombe = false;
        }
        public override void Update()
        {
            if (bTombe) 
            { 
             Vitesse = new Vector2(Vitesse.X, Vitesse.Y + 0.5f);
            }
            base.Update();
        }

        public void Tombe()
        {
            bTombe = true; 
            Vitesse = new Vector2(Vitesse.X, 1);
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        { 
            //ici on met ce que les briques doivent dessiner quand elles sont touchées
        }
    }
}
