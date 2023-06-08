using ListeImages;
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

        private static int n; // membre static, il est partagé avec toutes les instances/classes de bulles
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

        public Bubble() // pour ne pas avoir à indiquer les images a chaque fois on utilise le constructeur
        {
            ContentManager Content = ServiceLocator.GetService<ContentManager>();   
            image = Content.Load<Texture2D>("Bulle");
            n++; // il va compter le nombre total de bulles créées quelque soit sa classe/instance
    
        }
        public virtual void SetPosition(float pX, float pY)
        {
            position = new Vector2(pX, pY);
        }
        public virtual void SetPosition(float pX, float pY, float pDirectionX, float pDirectionY) // polymorphisme
        {
            position = new Vector2(pX, pY);
        }
        public virtual void Affiche() // injection de dépendance. On ne doit pas utiliser de _spriteBacth.Draw dans les class. On utilise le spritebatch du Game !
        {
            SpriteBatch _spriteBatch = ServiceLocator.GetService<SpriteBatch>();

            _spriteBatch.Draw(image, position, Color.White);
        }

        public virtual void Move()
        {
        }

        public virtual void Collisions()
        {
        }
    }
}
