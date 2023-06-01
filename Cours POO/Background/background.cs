using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Background
{
    internal class background
    {
        private Vector2 position; // ici le membre est privé, personne ne peut y accéder. On doit créer un accesseur.
        private Texture2D image;
        private float speed; 
        public Vector2 Position // on créer un accesseur Position, qui récupère (get>return) la position sans donner accès à toute la classe.La condition est modifiable que par la classe.
        {
            get 
            { 
                return position;
            }
        }
        public Texture2D Image
        {  get
            {
                return image; 
            }
        }

        public background(float pSpeed, Texture2D pTexture)
        {
            speed = pSpeed;
            image = pTexture;
            position = new Vector2(0,0);
        }

        public void Update()
        {
            position.X += speed;
            if (position.X < 0 - image.Width)
            {
                position.X = 0; 
                    }
        }
    }
}
