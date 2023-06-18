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
            Collision
        }
        public State currentState;
        public Personnages(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            currentState = State.Idle;
        }
        public void Tombe()
        {
            currentState = State.Moving;
            Vitesse = new Vector2 (0, 1);
        }

        public override void Update()
        { 
            if (currentState == State.Moving)
            {
                Vitesse += Position;
            }
            
        }
    }
}
