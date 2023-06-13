using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{

    class SceneMenu : Scenes
    {
        SpriteFont FontMenu;

        public SceneMenu(Game pGame) : base(pGame)
        {
            FontMenu = game.Content.Load<SpriteFont>("PixelMaster");
        }

        public override void Update()
        {

        }

        public override void DrawScene(SpriteBatch pBatch)
        {
            pBatch.DrawString(FontMenu,
                    "Ceci est le menu",
                    new Vector2(1, 1),
                    Color.White);
        }

    }
}
