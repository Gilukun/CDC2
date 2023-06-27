using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
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
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        HUD hud = ServiceLocator.GetService<HUD>();
        AssetsManager audio = ServiceLocator.GetService<AssetsManager>();   
       
        Texture2D bIce;
        private int initSpeed;
        private float BonusSpeed;
        private float bonusSlowdown;

        protected float Delay;
        protected float Timer;
        protected bool TimerIsOver;
        public int Impact { get; set; }

        public enum BallState
        {
            Alive,
            Dead,
            SpeedUp,
            SlowDown,
            Ice,
            Reset,
        }
         public BallState CurrentBallState { get; set; }
        
        public Balle(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            CurrentBallState = BallState.Alive;
            initSpeed = 1;
            Delay  = 0;
            Timer = 5;
            BonusSpeed = 2;
            bonusSlowdown = 0.5f;
            Impact = 1;
            bIce = _content.Load<Texture2D>("bMenu");
        }

        public  void TimerON(float pIncrement)
        {
            Delay += pIncrement;
            if (Delay > Timer)
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
                AssetsManager.PlaySFX(audio.hitWalls);
                SetPosition(0, Position.Y);
            }
              if (Position.X + LargeurSprite > ResolutionEcran.Width)
            {
                Vitesse = new Vector2(-Vitesse.X, Vitesse.Y);
                AssetsManager.PlaySFX(audio.hitWalls);
                SetPosition(ResolutionEcran.Width - LargeurSprite, Position.Y);
            }
            hud = ServiceLocator.GetService<HUD>();
            if (Position.Y < hud.Hudhauteur)
            {
                Vitesse = new Vector2(Vitesse.X, -Vitesse.Y);
                AssetsManager.PlaySFX(audio.hitWalls);
                SetPosition(Position.X, hud.Hudhauteur);
            }
        }
        public override void Update()
        {
            if (CurrentBallState == BallState.SpeedUp)
            {
                SpeedUp();
                TimerON(0.005f);
                if (Delay > Timer) 
                {
                    CurrentBallState = BallState.Alive;
                    Delay = 0; 
                }
            }
            else if  (CurrentBallState == BallState.SlowDown)
            {
                SlowDown();
                TimerON(0.005f);
                
                if (Delay > Timer)
                {
                    CurrentBallState = BallState.Alive;
                    Delay = 0;  
                }
            }
            else if (CurrentBallState == BallState.Ice)
            {
                Impact = 3;
                TimerON(0.005f);
                
                if (Delay > Timer)
                {
                    CurrentBallState = BallState.Alive;
                    Impact = 1;
                    Delay = 0;
                }
            }
            else if (CurrentBallState == BallState.Reset)
            {
            }

            Position += Vitesse * initSpeed;
            Rebounds();
            
            base.Update();
        }

        public void DrawBall()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            Texture2D currentTexture = (CurrentBallState == BallState.Ice) ? bIce : texture;

            pBatch.Draw(currentTexture,
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
