using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
          Dead,
          SpeedUp,
          SlowDown,    
        }
        public BallState CurrentBallState;
        public Balle(Texture2D pTexture) : base(pTexture)
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            HUD = new HUD(_content.Load<Texture2D>("HUD2"));
            CurrentBallState = BallState.Alive;
        }

        public override void Load()
        {
            
        }

        public void SpeedUp()
        {
            Vitesse = new Vector2(2, -2);
            Position += Vitesse;
        }

        public void Rebounds()
        {
            if (Position.X < 0)
            {
                Vitesse = new Vector2(-Vitesse.X, Vitesse.Y);
                SetPosition(0, Position.Y);
            }
            if (Position.X + LargeurSprite > largeurEcran)
            {
                Vitesse = new Vector2(-Vitesse.X, Vitesse.Y);
                SetPosition(largeurEcran - LargeurSprite, Position.Y);
            }

            if (Position.Y < HUD.HauteurBarre)
            {
                Vitesse = new Vector2(Vitesse.X, -Vitesse.Y);
                SetPosition(Position.X, HUD.HauteurSprite);
            }
        }
        public override void Update()
        {
           

            if( CurrentBallState ==  BallState.Alive ) 
            {
                Position += Vitesse;
                Rebounds();
                Trace.WriteLine(Vitesse);
                
            }
            else if (CurrentBallState == BallState.SpeedUp)
            {
                SpeedUp();
                Rebounds();
                
            }

            base.Update();

        }
    }
}
