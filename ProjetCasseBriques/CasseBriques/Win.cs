using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    internal class Win : ScenesManager
    {
        Texture2D background;
        public Win(CasseBriques pGame) : base(pGame)
        {
            background = pGame.Content.Load<Texture2D>("pFire");

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
