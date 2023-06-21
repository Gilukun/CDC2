using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class bGlace : Briques
    {

        //public int NbCoups { get; set; }

        public bGlace(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            scale = 1.0f;
            isBreakable = true;
            nbHits = 3;
            Points = 300;
        }

        public override void Update()
        {
           
            base.Update();
        }

    }
}
