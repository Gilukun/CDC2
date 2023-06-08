using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    internal class SceneMenu : Scenes
    {

        public override void Update()
        {

        }

        public override void Draw()
        {
            _spriteBatch.Draw(pad, new Vector2(pad_x, pad_y), Color.White);
        }
    }

   
}
