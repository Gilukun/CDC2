using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class BFeu : Briques
    {
        public float Rotation { get; set; }
        public bool rotate;

     public BFeu(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            Rotation = 0.0f;
            rotate = false;
            scale = 1.0f;   
            Scalling = false;
            isBreakable = true;
            nbHits = 2;
            Points = 200;
        }

        public override void Update()
        {
            if (rotate)
            {
                Rotation += 10f;
            }
            base.Update();

        }
       
    }
}
