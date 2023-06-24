using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Briques : Sprites
    {
        public Texture2D texture;
        public float scale { get; set; }
        public int nbHits { get; set; }
        public int Points { get; set; }
        public bool Scalling;
        public bool isScalling
        {
            get
            {
                return Scalling;
            }
        }
        public bool isBreakable;

        public enum State
        {
            Idle,
            Hit,
            Broken,
        }
        public State currentState { get; set; }
        public Briques(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
        }
        public override void Update()
        {
            if (Scalling)
            {
                scale -= 0.1f;
                if (scale <= 0)
                {
                    scale = 0f;
                    Scalling = false;
                }
            }
          base.Update();
        } 
    }
}
