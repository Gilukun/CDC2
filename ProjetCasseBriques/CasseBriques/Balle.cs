using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Balle : Sprites
    {
        HUD HUD;

                public enum BallState
        { Alive, 
          Dead
        }
        public BallState CurrrentBallState;
        public Balle(Texture2D pTexture) : base(pTexture)
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            HUD = new HUD(_content.Load<Texture2D>("HUD2"));
            CurrrentBallState = BallState.Alive;
        }

        public override void Load()
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

            if (Position.Y < HUD.HauteurBarre)
            {
                Vitesse = new Vector2(Vitesse.X, - Vitesse.Y);
                SetPosition(Position.X, HUD.HauteurSprite);
            }

            base.Update();

        }
    }
}
