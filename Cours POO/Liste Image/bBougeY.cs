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

        public override void SetPosition(float pX, float pY)
        {
            position = new Vector2(pX, pY);

        }
        public override void Affiche(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(image, position, Color.BlueViolet);
        }

        public override void Move(GraphicsDeviceManager pGraphics)
        {

            speedMax = 1.9f;
            speed = 0.005f; ;
            velocity += new Vector2(0, speed);
            if (Math.Abs(velocity.Y) > speedMax)
            {
                velocity = new Vector2(velocity.X, (velocity.Y < 0 ? 0 - speedMax : speedMax));  // création d'un if in line (a deux sorties) Estce que Velocity < 0 alors 0 - speedmax. Sinon : speedMax
            }
            position += velocity;

        }

        public override void Collisions(GraphicsDeviceManager pGraphics)
        {
            int hauteur = pGraphics.GraphicsDevice.Viewport.Width;
            if (position.Y > hauteur)
                position = new Vector2(position.X, 0);
            velocity = new Vector2(velocity.X, velocity.Y);

        }
    }
}