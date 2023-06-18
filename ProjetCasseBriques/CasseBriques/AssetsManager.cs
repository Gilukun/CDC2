using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class AssetsManager
    {
        public static SpriteFont TitleFont {get; private set;}
        public static void Load()
        {
            ContentManager pContent = ServiceLocator.GetService<ContentManager>();
            TitleFont = pContent.Load<SpriteFont>("TitleFont");
        }

        public static Vector2 GetSize(string pText, SpriteFont pFont) 
        {
            Vector2 textsize = pFont.MeasureString(pText);
            return textsize;
        }
    
    }
}
