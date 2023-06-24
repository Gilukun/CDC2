using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    internal class BBase : Briques
    {
        public BBase(Texture2D pTexture) : base(pTexture)
        {
            scale = 1.0f;
            Scalling = false;
            isBreakable = true;
            nbHits = 1;
            Points = 100;
        }
    }
}
