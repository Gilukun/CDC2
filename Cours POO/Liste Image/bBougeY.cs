using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Liste_Image;
using System.Reflection;

namespace ListeImages
{
    internal class bBougeY : Bubble
    {
        public bBougeY() : base()
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

            _spriteBatch.Draw(image, position, Color.Green);
        }

        public override void Move()
        {
            //speedMax = 20f;
            speed = 5f; 
            position += direction * speed;
            //if (Math.Abs(direction.Y) > speedMax)
            //{
               // direction = new Vector2(direction.X, (direction.Y < 0 ? 0 - speedMax : speedMax));  // création d'un if in line (a deux sorties) Estce que Velocity < 0 alors 0 - speedmax. Sinon : speedMax
            //}
        }

        public override void Collisions()
        {
            ScreenManager screenManager = ServiceLocator.GetService<ScreenManager>();
            //Point screenSize = screenManager.GetScreenSize();
            //int hauteur = pGraphics.GraphicsDevice.Viewport.Width;
            if (position.Y > width)
                position = new Vector2(position.X, 0);
        }
    }
}