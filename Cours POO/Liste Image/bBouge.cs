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
                public bBouge(ContentManager pContent) : base(pContent)
        {
        }

        public override void SetPosition(float pX, float pY, float pDirectionX, float pDirectionY)
        {
            position = new Vector2(pX, pY);
            direction = new Vector2(pDirectionX, pDirectionY);

        }
        public override void Affiche(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(image, position, Color.Red);
        }

        public override void Move(GraphicsDeviceManager pGraphics)
        {

            speedMax = 10f;
            speed = 0.9f;
            direction += direction * speed; 
            if (Math.Abs(direction.X) > speedMax)
            {
                direction = new Vector2((direction.X < 0 ? 0 - speedMax : speedMax), direction.Y);  // création d'un if in line (a deux sorties) Estce que Velocity < 0 alors 0 - speedmax. Sinon : speedMax
            }
            position += direction;

        }

        public override void Collisions(GraphicsDeviceManager pGraphics)
        {
            int largeur = pGraphics.GraphicsDevice.Viewport.Width;
            if (position.X > largeur)
                position = new Vector2(0, position.Y);
            if (position.X < 0)
                position = new Vector2(largeur, position.Y);
        }
    }
}
