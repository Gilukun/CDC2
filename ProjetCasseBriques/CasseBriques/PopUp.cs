using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class PopUp
    {
        AssetsManager font = ServiceLocator.GetService<AssetsManager>();
        Texture2D texture;
        Vector2 Position;
        
        public PopUp()
        {
        }

       public void SetPosition(float pX, float pY)
        {
            Position = new Vector2(pX, pY);
        }

        public void DrawPopUp(string pString)
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>(); 
           
            pBatch.DrawString(font.PopUpFont,
                                pString,    
                                new Vector2(Position.X, Position.Y),
                                Color.DarkSalmon);
        }

    }
}
