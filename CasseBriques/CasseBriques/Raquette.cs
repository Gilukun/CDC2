using Microsoft.Xna.Framework;
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
        public float Centre
        {
            get {return Position.X + Width / 2; }

        } 
      

        
        public Raquette(Texture2D pTexture, Rectangle pScreen ): base(pTexture, pScreen) 
        {
        }

        public override void Update()
        {
            SetPosition( Mouse.GetState().X - Width/2, Position.Y );
            if (Position.X < 0)
            {
                SetPosition(0, Position.Y);
            }
            if (Position.X > Screen.Width - Width)
            {
                SetPosition(Screen.Width - Width, Position.Y);
            }

            base.Update();

        }
        public override void DrawSprite(SpriteBatch spriteBatch)
        {
        }

    }
}
