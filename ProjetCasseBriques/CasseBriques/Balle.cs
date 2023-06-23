using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static CasseBriques.Personnages;

namespace CasseBriques
{
    public class Balle : Sprites
    {
        HUD HUD;
        private int initSpeed;
        private float BonusSpeed;
        private float bonusSlowdown;

        protected float SpeedBonusDelay;
        protected float SpeedBonusTimer;
        protected bool TimerIsOver;

        public enum BallState
        {
            Alive,
            Dead,
            SpeedUp,
            SlowDown,
        }
         public BallState CurrentBallState { get; set; }
        
        public Balle(Texture2D pTexture) : base(pTexture)
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            HUD = new HUD(_content.Load<Texture2D>("HUD2"));
            CurrentBallState = BallState.Alive;
            initSpeed = 1;
            SpeedBonusDelay  = 0;
            SpeedBonusTimer = 5;
            BonusSpeed = 2;
            bonusSlowdown = 0.5f;

        }

        public  void TimerON()
        {
            SpeedBonusDelay += 0.05f;
           
            if (SpeedBonusDelay > SpeedBonusTimer)
            {
                TimerIsOver = true;
            }
        }

        public override void Load()
        {
            
        }

        public void SpeedUp()
        {
            Position += Vitesse * BonusSpeed ; 
        }

        public void SlowDown()
        {
            Position -= Vitesse * bonusSlowdown ;
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
            if (CurrentBallState == BallState.SpeedUp)
            {
                SpeedUp();
                TimerON();
                if (SpeedBonusDelay > SpeedBonusTimer) 
                {
                    CurrentBallState = BallState.Alive;
                    SpeedBonusDelay = 0; 
                }
            }
            else if  (CurrentBallState == BallState.SlowDown)
            {
                SlowDown();
                TimerON();
                
                if (SpeedBonusDelay > SpeedBonusTimer)
                {
                    CurrentBallState = BallState.Alive;
                    SpeedBonusDelay = 0;  
                }
            }
            Position += Vitesse * initSpeed;
            Rebounds();
          
            base.Update();

        }
    }
}
