﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected Sprites(Texture2D pTexture)
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

            /* affiche uniquement les boundingBox
            RasterizerState state = new RasterizerState();
            state.FillMode = FillMode.WireFrame;
            pBatch.GraphicsDevice.RasterizerState = state;
            */

            pBatch.Draw(texture, Position,Color.White); 

        }

    }
}
