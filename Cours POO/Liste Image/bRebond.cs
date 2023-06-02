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
        public bRebond(ContentManager pContent) : base(pContent)
        {
        }

        public override void SetPosition(float pX, float pY)
        {
            position = new Vector2(pX, pY);

        }
        public override void Affiche(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(image, position, Color.GreenYellow);
        }

        public override void Move(GraphicsDeviceManager pGraphics)
        {

            speedMax = 1.9f;
            speed = 0.005f; ;
            velocity += new Vector2(speed, 0);
            position += velocity;

            int hauteur = pGraphics.GraphicsDevice.Viewport.Height;
            int largeur = pGraphics.GraphicsDevice.Viewport.Width;

            if (position.X > largeur - width)
            {
                position = new Vector2(largeur - width, position.Y);
                velocity = new Vector2(- velocity.X, 0);

                Trace.WriteLine(position);
            }
            if (position.X < 0)
            {
                position = new Vector2(0, position.Y);
                velocity = new Vector2(-velocity.X, velocity.Y);
            }
        }

    }
       

}
