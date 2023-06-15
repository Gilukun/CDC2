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
        public int NbCoups { get; set; }

        public float scale { get; set; }
        public float rotation { get; set; }
        public bool rotate; 

        public int nbHits { get; set; }
        public bool Scalling;

        public bool IsScalling
        {
            get
            {
                return Scalling;
            }
        }
        public bool RotationOn
        {
            get
            {
                return rotate;
            }
        }

        public Briques(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            scale = 1.0f;
            Scalling = false;
            rotation = 0.0f;
            rotate = false;
        }


       public void JeScale()
        {
         Scalling = true;
        }

        public void JeTourne()
        {
            rotate = true;
        }

        public override void Update()
        {
            if (Scalling)
            {
                scale -= 0.01f;
                if (scale <= 0)
                {
                    scale = 0f;
                }
            }

            if (RotationOn) 
            {
                rotation += 0.4f;
            
            }
            base.Update();
        }

        
     
       
    }
}
