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
    public class Settings : ScenesManager
    {
        Texture2D background;
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        public Settings()
        {
            background = _content.Load<Texture2D>("pIce");

        }

        public override void Load()
        {
        }
        public override void Update()
        {
            base.Update();
        }

        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
        }
    }
}
