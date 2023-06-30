using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques

{
    public class HUD : GUI
    {
        Vector2 DimensionScore;
        Vector2 dimensionLife;
        Vector2 dimensionNiveau;

        public int Hudhauteur
        { get
            { 
                return texture.Height;
            }
        }

        public int level;

        public int globalScore;
        public int Vie { get; set; }
        private string score;
        private string life;
        private string niveau;

        public Balle balle;
        public Gameplay currentLvlNB;

        public HUD(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            globalScore = 0;
            Vie = 3;
            level = 1; 
        }

        public override void Load()
        {
            
        }
        public override void DrawScore()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            AssetsManager font = ServiceLocator.GetService<AssetsManager>();
            ScreenManager screen = ServiceLocator.GetService<ScreenManager>();

            score = "SCORE" + " " + globalScore;
            DimensionScore = font.GetSize(score, font.HUDFont);
            float posXScore = 10f;
            float posYScore = texture.Height / 2 - DimensionScore.Y / 2;
           // boundingBox = AssetsManager.getBoundingBox(Score, Font.HUDFont, positiontext);
           // pBatch.DrawRectangle(boundingBox, Color.Red);
            pBatch.DrawString(font.HUDFont,   
                            score,
                            new Vector2 (posXScore, posYScore),
                            Color.White);

            life = "VIE" + " " + Vie;
            dimensionLife = font.GetSize(life, font.HUDFont);
            float posX = screen.Width - dimensionLife.X;
            float posY = texture.Height / 2 - dimensionLife.Y / 2;
            pBatch.DrawString(font.HUDFont,
                           life,
                           new Vector2(posX, posY),
                           Color.White);

            niveau = "Lvl : " + " " + level ;
            dimensionNiveau= font.GetSize(niveau, font.HUDFont);
            float posXLevel = screen.HalfScreenWidth;
            float posYLevel = texture.Height / 2 - dimensionNiveau.Y / 2;
            pBatch.DrawString(font.HUDFont,
                            niveau,
                            new Vector2(posXLevel, posYLevel),
                            Color.White);
        }
    }
}
