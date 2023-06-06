using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
    public class Sprites : IActor
    {
        // IACTOR positions et vector de base
        public Vector2 Position{get; set;}
        public Vector2 Direction { get; set;}

        public Rectangle BoundingBox { get; set; }

        public float vx; 
        public float vy;
        public float Speed;
        public float angle; 

        public MouseState mouseState;

        // SPRITE
        public Texture2D Texture { get;}
        public bool ToRemove { get; set; }        

        // Constructeur d'images (textures)

        public  Sprites(Texture2D pTexture)
        {
            Texture = pTexture;

        }

        // déplacement de l'image
        public void Move(float pX, float pY)
        { 
            Position = new Vector2(Position.X + pX, Position.Y + pY);

        }

        public virtual void Shoot(Vector2 pPosition, Vector2 pdirection, float pSpeed)
        {
            Position = pPosition;
            Direction = pdirection;
            Speed = pSpeed;
            
        }

        public void MoveMouse()
        {
            Position += Direction * Speed;

        }
        public float Angle()
        {
            return angle = (float)Math.Atan2(Direction.Y, Direction.X);
        }


        // Fonction pour savoir qui touche
        public virtual void TouchBy(IActor pBy)
        {
        }
        public virtual void Update(GameTime pGametime)
        {
            Move(vx,vy); // on applique le déplacement directement ici a tous les sprites qui auront des paramètre de vélocité px, py
            // On créer la boundingbox qui contient notre image. Elle sera mise à jour pour toujours correspondre à l'emplacement de la texture
            BoundingBox = new Rectangle((int)Position.X,
                                       (int)Position.Y,
                                        Texture.Width, 
                                        Texture.Height);
            Angle();
            MoveMouse();

        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(Texture,
                               Position,
                               null,
                               Color.White,
                               angle,
                               new Vector2(Texture.Width / 2, Texture.Height / 2),
                               1f,
                               SpriteEffects.None, 
                               0f
                               ) ;
        }
       

        
    }
}
