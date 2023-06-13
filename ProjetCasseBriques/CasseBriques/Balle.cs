using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Balle : Sprites
    {
        public Balle(Texture2D pTexture) : base(pTexture)
        {
        }

        public override void Update()
        {
            Position += Vitesse;
            if (Position.X < 0)
            { 
                Vitesse = new Vector2(-Vitesse.X, Vitesse.Y);
                SetPosition(0, Position.Y);
            }
            if (Position.X + LargeurSprite> largeurEcran )
            {
                Vitesse = new Vector2(-Vitesse.X, Vitesse.Y);
                SetPosition(largeurEcran - LargeurSprite, Position.Y);
            }

            if (Position.Y < 0)
            {
                Vitesse = new Vector2(Vitesse.X, - Vitesse.Y);
                SetPosition(Position.X, 0);
            }

            base.Update();

        }
    }
}
