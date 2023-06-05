using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
    internal class SceneGameOver : Scene
    {
        public SceneGameOver(MainGame pGame) : base(pGame)
        {

        }

        public override void Load()
        {
            base.Load();

        }

        public override void Unload()
        { 
            base.Unload(); 
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.DrawString(AssetManager.MainFont,
                                             "This is the GAMEOVER",
                                             new Vector2(2, 1),
                                             Color.White);

            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
