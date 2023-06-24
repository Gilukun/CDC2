﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CasseBriques
{
    public class BMetal : Briques
    {
        public BMetal(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            
            isBreakable = false;
        }
    }
}
