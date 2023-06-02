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

        private float speed = 0.005f; 
        private float speedMax = 1f; 
        public bRebond(ContentManager pContent) : base(pContent)
        {
        }

        public override void SetPosition(float pX, float pY)
        {
            position = new Vector2(pX, pY);
            velocity = new Vector2(1, 1);

        }
        public override void Affiche(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(image, position, Color.GreenYellow);
        }

        public override void Move(GraphicsDeviceManager pGraphics)
        {
            speedMax = 2f;
            speed = 0.05f; ;
            position += velocity * speed;
            if (Math.Abs(velocity.X) > speedMax)
            {
                velocity = new Vector2((velocity.X < 0 ? 0 - speedMax : speedMax), velocity.Y);  // création d'un if in line (a deux sorties) Estce que Velocity < 0 alors 0 - speedmax. Sinon : speedMax
            }
            position += velocity;

            int hauteur = pGraphics.GraphicsDevice.Viewport.Height;
            int largeur = pGraphics.GraphicsDevice.Viewport.Width;

            if (position.X > largeur - width)
            {
                position = new Vector2(largeur - width, position.Y);
                velocity.X = -velocity.X;
                Trace.WriteLine(position);
            }
            if (position.X < 0)
            {
                position = new Vector2(0, position.Y);
                velocity = new Vector2(-velocity.X, velocity.Y);
            }

            if (position.Y > hauteur - height)
            {
                position = new Vector2(position.X, hauteur - height);
                velocity.Y = -velocity.Y;
                Trace.WriteLine(position);
            }
            if (position.Y < 0)
            {
                position = new Vector2(position.X, 0);
                velocity.Y = - velocity.Y;
            }
        }

    }
       

}
