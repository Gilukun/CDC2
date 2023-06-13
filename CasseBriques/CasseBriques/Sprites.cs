using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public abstract class Sprites
    {
        public Vector2 Position { get; set; }
        public Vector2 Vitesse { get; set; }
        private Texture2D texture;
        protected Rectangle Screen; 
        public Rectangle BoundingBox{ get; set; }   

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
        public int HWidth
        {
            get
            {
                return texture.Width/2;
            }
        }

        public Sprites(Texture2D pTexture, Rectangle pScreen) 
        {
            texture = pTexture;
            Screen = pScreen;
        
        }

        public void SetPosition(Vector2 pPosition)
        {
            Position = pPosition;
        }

        public void InverseVitesseX() 
        {
            Vitesse = new Vector2(-Vitesse.X, Vitesse.Y); 
        }
        public void InverseVitesseY()
        {
            Vitesse = new Vector2(Vitesse.X, - Vitesse.Y);
        }

        // ou sinon on peut directement passer X Y 
        public void SetPosition(float pX,  float pY)
        {
            Position = new Vector2(pX, pY);
        }

        public Rectangle NextPositionX()
        {
            Rectangle NextPosition = BoundingBox;
            NextPosition.Offset(new Point((int)Vitesse.X, 0));
            return NextPosition;
        }
        public Rectangle NextPositionY()
        {
            Rectangle NextPosition = BoundingBox;
            NextPosition.Offset(new Point(0, (int)Vitesse.Y));
            return NextPosition;
        }


        public virtual void Update()
        {
           // Position = new Vector2 (Position.X + Vitesse.X, 
                                    //Position.Y + Vitesse.Y);
            Position += Vitesse;

            BoundingBox = new Rectangle((int) Position.X, (int) Position.Y, Width, Height); // tous mes sprites ont maintenant une bounding box



            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
            }

        }

        public abstract void DrawSprite(SpriteBatch spriteBatch);
       

        public virtual void Draw(SpriteBatch pBatch)
        {
            pBatch.Draw(texture, Position, Color.White); 
            DrawSprite(pBatch);
        }

    }
}
