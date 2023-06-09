using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Sprites
    {
        public Vector2 Position { get; set; }
        public Vector2 Vitesse { get; set; }
        private Texture2D texture;

        // propriété automatique pour récupérer la hauteur de la texture
        public int Height
        {
            get
            { 
                return texture.Height; 
            }
        }
        public int Width
        {
            get
            {
                return texture.Width;
            }
        }


        public Sprites(Texture2D pTexture) 
        {
            texture = pTexture;
        
        }

        public void SetPosition(Vector2 pPosition)
        {
            Position = pPosition;
        }

       

        // ou sinon on peut directement passer X Y 
        public void SetPosition(float pX,  float pY)
        {
            Position = new Vector2(pX, pY);
        }
        public virtual void Update()
        {
           // Position = new Vector2 (Position.X + Vitesse.X, 
                                    //Position.Y + Vitesse.Y);
            Position += Vitesse; 
            
             
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {

            }

        }

        public virtual void Draw(SpriteBatch pBatch)
        {
            pBatch.Draw(texture, Position, Color.White); 
        }

    }
}
