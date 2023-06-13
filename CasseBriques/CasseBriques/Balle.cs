using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Balle : Sprites
    {
        public Balle(Texture2D pTexture, Rectangle pScreen ) : base(pTexture, pScreen)
        {
     
        }
        public override void Update()
        {
            if (Position.X > Screen.Width- Width) 
            { 
                SetPosition(Screen.Width - Width, Position.Y);
                InverseVitesseX();
            }

            if (Position.X<0)
            {
                SetPosition(0, Position.Y);
                InverseVitesseX();
            }

            if (Position.Y<0)
            {
                SetPosition(Position.X, 0);
                InverseVitesseY();
            }
            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
        }
    }
}
