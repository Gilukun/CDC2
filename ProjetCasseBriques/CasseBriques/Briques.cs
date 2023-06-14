using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Briques : Sprites
    {
        public int NbCoups { get; set; }
        //private bool Scale;
        //public bool isScalling
        //{ get
        //    {
        //        return Scale;
        //    }
        //}
        //public int type;
        //public float nbScale;
        //public float scaling
        //{
        //    get
        //    {
        //        return nbScale;

        //    }
        //}

        public int nbHits { get;set; }

        public Briques(Texture2D pTexture) : base(pTexture)
        {
            //Scale = false;
        }

        public void Scalling()
        {
            //Scale = true;
            //nbScale = 1;

        }
        public override void Update()
        {
            //if (Scale)
            //{
            //    nbScale -= 0.1f;
            //}
            base.Update();
        }

    }
}
