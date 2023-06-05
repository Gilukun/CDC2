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
        public bBougeY(ContentManager pContent) : base(pContent)
        {
        }

        public override void SetPosition(float pX, float pY, float pDirectionX, float pDirectionY)
        {
            position = new Vector2(pX, pY);
            direction = new Vector2(pDirectionX, pDirectionY);

        }
        public override void Affiche(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(image, position, Color.BlueViolet);
        }

        public override void Move(GraphicsDeviceManager pGraphics)
        {

            speedMax = 1.9f;
            speed = 0.03f; ;
            position += new Vector2(0, speed);
            if (Math.Abs(direction.Y) > speedMax)
            {
                direction = new Vector2(direction.X, (direction.Y < 0 ? 0 - speedMax : speedMax));  // création d'un if in line (a deux sorties) Estce que Velocity < 0 alors 0 - speedmax. Sinon : speedMax
            }
            position += direction;

        }

        public override void Collisions(GraphicsDeviceManager pGraphics)
        {
            int hauteur = pGraphics.GraphicsDevice.Viewport.Width;
            if (position.Y > hauteur)
                position = new Vector2(position.X, 0);
        }
    }
}