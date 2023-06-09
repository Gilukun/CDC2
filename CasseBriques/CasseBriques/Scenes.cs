using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
   
    public class Scenes
    {
        // placer un service locator pour taille d'écran / spriteBatch / son ? 
        public Scenes(Game pGame) 
        {
          
        }
        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch pSpritebatch)
        { 
        }
    }
}
