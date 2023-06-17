﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SpriteBatchExtensions // Classe Static qui permet de créer une méthode pour afficher les hitbox
{
    public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
    {
        Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        pixel.SetData(new[] { color });

        spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), color);
        spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, 1), color);
        spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), color);
        spriteBatch.Draw(pixel, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height + 1), color);
    }
}

namespace CasseBriques
{
    public abstract class Sprites
    {
        private Texture2D texture;
        public Vector2 Position { get; set; }
        public Vector2 Vitesse { get; set; }
        protected int hauteurEcran;
        protected int largeurEcran;
        public int LargeurSprite
        {
            get
            { return texture.Width; }
        }
        public int HauteurSprite
        {
            get
            { return texture.Height; }
        }

        public Rectangle BoundingBox;
        

        // Constructeur
        public Sprites(Texture2D pTexture)
         { 
            ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
            largeurEcran = ResolutionEcran.Width;
            hauteurEcran = ResolutionEcran.Height; 
            texture = pTexture;
        }

        // Fonction pour avoir le centre des sprites

        public virtual void SetPosition(float pX, float pY)
        {
            Position = new Vector2(pX, pY);
        }


        public virtual Rectangle NextPositionX()
        {
            Rectangle NextPosition = BoundingBox;
            NextPosition.Offset(new Point((int)Vitesse.X, 0));
            return NextPosition;
        }
        public virtual Rectangle NextPositionY()
        {
            Rectangle NextPosition = BoundingBox;
            NextPosition.Offset(new Point(0, (int)Vitesse.Y));
            return NextPosition;
        }

        public void Load()
        { 
        }
      
        public virtual void Update()
        {
            Position += Vitesse; // Pour les sprites avec de la vitesse on aura automatiquement la mise à jour de la vitesse
            BoundingBox = new Rectangle((int)Position.X,(int)Position.Y, LargeurSprite,  HauteurSprite);
        }
        public virtual void Draw()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.DrawRectangle(BoundingBox, Color.Red); // affichage des boundingBox

            pBatch.Draw(texture,
                        Position,
                        null,
                        Color.White,
                        0,
                        new Vector2(0, 0),
                        1f,
                        SpriteEffects.None,
                        0);
            

            //RasterizerState state = new RasterizerState();
            //state.FillMode = FillMode.Solid;
            //pBatch.GraphicsDevice.RasterizerState = state;

            //Texture2D texture,
            //Vector2 position,
            //Nullable<Rectangle> sourceRectangle,
            //Color color,
            //float rotation,
            //Vector2 origin,
            //float scale,
            //SpriteEffects effects,
            //float layerDepth

        }

    }
}
