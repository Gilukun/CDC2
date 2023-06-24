using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Personnages : Sprites
    {
        public Texture2D texture;
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        public enum State
        { Idle,
            Spawn,
            Falling,
            Moving,
            Collision,
            Catch,
            Dead
        }
        public State currentState { get; set; }
        protected float spawnDelay;
        protected float spawnTimer;
        protected bool TimerIsOver;
        protected bool isSpawn;
        public Personnages(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            spawnDelay = 0;
            spawnTimer = 3;
            currentState = State.Idle;
            isSpawn = false;
        }
        public virtual void Tombe()
        {
       
        }

        public virtual void Moving()
        {
        }

        public virtual void TimerON()
        {

        }

        public override void Update()
        {
  
            if (Position.X < 0)
            {
                Position = new Vector2(ResolutionEcran.Width, Position.Y);
            }
            if (Position.X > ResolutionEcran.Width)
            {
                Position = new Vector2(0, Position.Y);
            }
            base.Update();
        }

    }
}
