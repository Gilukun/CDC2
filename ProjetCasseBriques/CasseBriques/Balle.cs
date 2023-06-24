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
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        HUD HUD;
        Texture2D texture;
        Balle bIce;
        private int initSpeed;
        private float BonusSpeed;
        private float bonusSlowdown;

        protected float SpeedBonusDelay;
        protected float SpeedBonusTimer;
        protected bool TimerIsOver;
        public int Impact { get; set; }

        public enum BallState
        {
            Alive,
            Dead,
            SpeedUp,
            SlowDown,
            Ice,
        }
         public BallState CurrentBallState { get; set; }
        
        public Balle(Texture2D pTexture) : base(pTexture)
        {
            
            HUD = new HUD(_content.Load<Texture2D>("HUD2"));
            texture = pTexture;
            CurrentBallState = BallState.Alive;
            initSpeed = 1;
            SpeedBonusDelay  = 0;
            SpeedBonusTimer = 5;
            BonusSpeed = 2;
            bonusSlowdown = 0.5f;
            Impact = 1;

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
            bIce = new Balle(_content.Load<Texture2D>("bIce"));
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
            else if (CurrentBallState == BallState.Ice)
            {
                
            }

            Position += Vitesse * initSpeed;
            Rebounds();
            Trace.WriteLine(Impact);
            base.Update();

        }

        public void DrawBall()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            if (CurrentBallState == BallState.Ice)
            {
                texture = bIce.texture;

            }
            else
            {
                pBatch.Draw(texture,
                            Position,
                            null,
                            Color.White,
                            0,
                            new Vector2(LargeurSprite / 2, HauteurSprite / 2),
                            1f,
                            SpriteEffects.None,
                            0);
            }
        }
    }
}
