﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class bFeu : Briques
    {
        public float Rotation { get; set; }
        public bool rotate;

   
     public bFeu(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            Rotation = 0.0f;
            rotate = false;
            scale = 1.0f;   
            Scalling = false;
            isBreakable = true;
            nbHits = 2;
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
