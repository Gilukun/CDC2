using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class GameOver : ScenesManager
    {
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        Texture2D background;
        public GameOver()
        {
            background = _content.Load<Texture2D>("GameOver");
        }
        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
        }
    }
}
