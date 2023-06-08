using Liste_Image;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Reflection;
using System.Diagnostics;

namespace ListeImages
{
    internal class bRebond : Bubble
    {
        public bRebond() : base()
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

            _spriteBatch.Draw(image, position, Color.MediumPurple);
        }

        public override void Move()
        {
            //speedMax = 10f;
            speed = 5f;
            position += direction * speed;
            //if (Math.Abs(direction.X) > speedMax)
            //{
            //    direction = new Vector2((direction.X < 0 ? 0 - speedMax : speedMax), direction.Y);  // création d'un if in line (a deux sorties) Estce que Velocity < 0 alors 0 - speedmax. Sinon : speedMax
            //}

            //if (Math.Abs(direction.Y) > speedMax)
            //{
            //    direction = new Vector2(direction.X, (direction.Y < 0 ? 0 - speedMax : speedMax));  // création d'un if in line (a deux sorties) Estce que Velocity < 0 alors 0 - speedmax. Sinon : speedMax
            //}

            //int hauteur = pGraphics.GraphicsDevice.Viewport.Height;
            //int largeur = pGraphics.GraphicsDevice.Viewport.Width;

            ScreenManager screenManager = ServiceLocator.GetService<ScreenManager>();
            Point screenSize = screenManager.GetScreenSize();
            

            if (position.X > screenSize.X - width)
            {
                position = new Vector2(screenSize.X - width, position.Y);
                direction.X = -direction.X; 
            }

            if (position.X < 0)
            {
                position = new Vector2(0, position.Y);
                direction = new Vector2(-direction.X, direction.Y);
            }

            if (position.Y > screenSize.Y - height)
            {
                position = new Vector2(position.X, screenSize.Y - height);
                direction.Y = -direction.Y;
            }

            if (position.Y < 0)
            {
                position = new Vector2(position.X, 0);
                direction.Y = - direction.Y;
            }
        }

    }
       

}
