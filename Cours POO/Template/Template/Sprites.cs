using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
    public class Sprites : IActor
    {
        // IACTOR
        public Vector2 Position{get; set;}

        public Rectangle BoundingBox { get; set; }

        public float vx; 
        public float vy;

        // SPRITE
        public Texture2D Texture { get;}

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

        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(Texture, Position, Color.White);
        }

        
    }
}
