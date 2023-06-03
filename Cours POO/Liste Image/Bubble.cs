using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liste_Image
{
    internal class Bubble
    {
        protected float x; 
        protected float y;
        protected float speed;
        protected float speedMax;
        protected Vector2 position; 
        protected Vector2 direction;

       
        protected Texture2D image;
        public int width
        { get
            { 
                return image.Width;
            }
        }
        public int height
        {
            get
            { 
                return image.Height; 
            }
        }


        public Bubble(ContentManager pContent) // pour ne pas avoir à indiquer les images a chaque fois on utilise le constructeur
        {
            image = pContent.Load<Texture2D>("Bulle");
 
        }

        public virtual void SetPosition(float pX, float pY)
        {
            position = new Vector2(pX, pY);

        }
        public virtual void SetPosition(float pX, float pY, float pDirectionX, float pDirectionY) // polymorphisme
        {
            position = new Vector2(pX, pY);

        }
        public virtual void Affiche(SpriteBatch pSpriteBatch) // injection de dépendance. On ne doit pas utiliser de spriteBacth dans les class. On utilise le spritebatch du Game !
        {
            pSpriteBatch.Draw(image, position, Color.White);
        }

        public virtual void Move(GraphicsDeviceManager pGraphics)
        {
        }

        public virtual void Collisions(GraphicsDeviceManager pGraphics)
        {
        }
    }
}
