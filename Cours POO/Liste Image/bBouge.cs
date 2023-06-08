using Liste_Image;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ListeImages
{
    internal class bBouge : Bubble
    {
        public bBouge() : base()
        {
        }

        public override void SetPosition(float pX, float pY, float pDirectionX, float pDirectionY)
        {
            position = new Vector2(pX, pY);
            direction = new Vector2(pDirectionX, pDirectionY);

        }
        public override void Affiche()
        {
            SpriteBatch _spriteBatch = ServiceLocator.GetService<SpriteBatch>();

            _spriteBatch.Draw(image, position, Color.Red);
        }

        public override void Move()
        {
            //speedMax = 1f;
            speed = 0.9f;
            position += direction * speed; 
            //if (Math.Abs(direction.X) > speedMax)
            //{
            //   direction = new Vector2((direction.X < 0 ? 0 - speedMax : speedMax), direction.Y);  // création d'un if in line (a deux sorties) Estce que direction < 0 alors 0 - speedmax. Sinon : speedMax
            //}
        }

        public override void Collisions()
        {
            ScreenManager screenManager = ServiceLocator.GetService<ScreenManager>();
            // Point screenSize = screenManager.GetScreenSize();  Comme on a mis width/height en int {get;} on peut le récupérer et ça devient plus lisible

            //int largeur = pGraphics.GraphicsDevice.Viewport.Width;
            if (position.X > width)
                position = new Vector2(0, position.Y);
            if (position.X < 0)
                position = new Vector2(width, position.Y);
        }
    }
}
