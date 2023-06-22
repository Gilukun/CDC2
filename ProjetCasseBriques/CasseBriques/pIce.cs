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
    public class pIce : Personnages
    {
        public pIce(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            spawnDelay = 0;
            spawnTimer = 3;
            currentState = State.Idle;
            isSpawn = false;
        }

        public override void Tombe()
        {
            if (currentState != State.Moving)
            {
                currentState = State.Falling;
                Vitesse = new Vector2(Vitesse.X, 1);
            }

        }

        public override void SetPosition(float pX, float pY)
        {
            Position = new Vector2(pX, pY);
        }

        public override void Moving()
        {
            Vitesse = new Vector2(1, Vitesse.Y);
            Position += Vitesse;
        }

        public override void TimerON()
        {
            spawnDelay += 0.1f;
            Trace.WriteLine(spawnDelay);
            if (spawnDelay > spawnTimer)
            {
                TimerIsOver = true;
            }
        }

        public override void Update()
        {
            if (currentState == State.Idle)
            {
                if (!TimerIsOver)
                {
                    TimerON();
                }

                if (TimerIsOver && !isSpawn)
                {
                    Random rnd = new Random();
                    SetPosition(200, 400);
                    spawnDelay = 0;
                    isSpawn = true;
                    currentState = State.Spawn;
                }
            }
            else if (currentState == State.Spawn)
            {
                currentState = State.Moving;

            }
            else if (currentState == State.Moving)
            {
                Moving();
            }
            else if (currentState == State.Falling)
            {
                Vitesse = new Vector2(Vitesse.X, Vitesse.Y + 0.5f);
            }


           
            Trace.WriteLine(currentState);
            base.Update();
        }
    }
}
