using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Raquette : Sprites
    {
        public Raquette(Texture2D pTexture): base(pTexture) 
        { 

        
        }

        public override void Update()
        {
            SetPosition( Mouse.GetState().X - Width/2, Position.Y );
                
            base.Update();

            /* 
             1. taille de l'écran :
             2.
             */


        }
    }
}
