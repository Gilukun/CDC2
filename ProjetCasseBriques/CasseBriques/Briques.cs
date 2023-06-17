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
    public class Briques : Sprites
    {
        public Texture2D texture;
        //public int NbCoups { get; set; }
        public float scale { get; set; }
   
        public int nbHits { get; set; }
        public bool Scalling;
        public bool isScalling
        {
            get
            {
                return Scalling;
            }
        }
        public bool isBreakable;
        public Briques(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            scale = 1.0f;
            Scalling = false;
            isBreakable = true;
            nbHits = 1;
        }


        public override void Update()
        {
            if (Scalling)
            {
                scale -= 0.1f;
                if (scale <= 0)
                {
                    scale = 0f;
                }
            }
            base.Update();
        }

        
     
       
    }
}
