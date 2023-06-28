using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CasseBriques
{
    public class Balle : Sprites
    {
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        HUD hud = ServiceLocator.GetService<HUD>();
        AssetsManager audio = ServiceLocator.GetService<AssetsManager>();   
       
        Texture2D Big;
        private int initSpeed;
        private float bonusSpeed;
        private float bonusSlowdown;

        protected float Delay;
        protected float Timer;
        protected bool TimerIsOver;
        public int Impact { get; private set; }
        public bool collision;

        public enum BallState
        {
            Alive,
            Dead,
            SpeedUp,
            SlowDown,
            Big,
        }
         public BallState CurrentBallState { get; set; }
        
        public Balle(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            CurrentBallState = BallState.Alive;
            initSpeed = 1;
            Delay  = 0;
            Timer = 5;
            bonusSpeed = 2;
            bonusSlowdown = 0.5f;
            Impact = 1;
            Big = _content.Load<Texture2D>("bMenu");
            collision = false;
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
            Position += Vitesse * bonusSpeed ; 
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
                audio.PlaySFX(audio.hitWalls);
                SetPosition(0, Position.Y);
            }
              if (Position.X + SpriteWidth > ResolutionEcran.Width)
            {
                Vitesse = new Vector2(-Vitesse.X, Vitesse.Y);
                audio.PlaySFX(audio.hitWalls);
                SetPosition(ResolutionEcran.Width - SpriteWidth, Position.Y);
            }
            if (Position.Y < hud.Hudhauteur)
            {
                Vitesse = new Vector2(Vitesse.X, -Vitesse.Y);
                audio.PlaySFX(audio.hitWalls);
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
            else if (CurrentBallState == BallState.Big)
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
            Position += Vitesse * initSpeed;
            Rebounds();
            base.Update();
        }

        public void DrawBall()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            Texture2D currentTexture = (CurrentBallState == BallState.Big) ? Big : texture;

            pBatch.Draw(currentTexture,
                        Position,
                        null,
                        Color.White,
                        0,
                        new Vector2(SpriteWidth / 2, SpriteHeight / 2),
                        1f,
                        SpriteEffects.None,
                        0);
        }
    }
}
