using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class AssetsManager
    {
        public  SpriteFont TitleFont {get; set;}
        public  SpriteFont MenuFont { get; set; }

        public void Load()
        {
            ContentManager pContent = ServiceLocator.GetService<ContentManager>();
            TitleFont = pContent.Load<SpriteFont>("TitleFont");
            MenuFont = pContent.Load<SpriteFont>("MenuFont");
        }

        public static Vector2 GetSize(string pText, SpriteFont pFont)
        {
            Vector2 textsize = pFont.MeasureString(pText);
            return textsize;
        }

    }
}
