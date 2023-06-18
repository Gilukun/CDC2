using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    internal class Personnages : Sprites
    {
        public Texture2D texture;
        public enum State
            { Idle,
            Moving,
            Collision,
            Catch
            }
        public State currentState;
        public Personnages(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            currentState = State.Idle;
        }
        public void Tombe()
        {
            if (currentState != State.Moving)
            { 
            currentState = State.Moving;
            Vitesse = new Vector2(Vitesse.X, 1);
             }
    
        }

        public override void Update()
        { 
            if (currentState == State.Moving)
            {
                Vitesse = new Vector2(Vitesse.X, Vitesse.Y + 0.5f);
            }
            base.Update();
        }
    }
}
