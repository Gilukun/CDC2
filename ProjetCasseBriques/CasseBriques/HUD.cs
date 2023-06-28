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
        //Vector2 positiontext;
        //Rectangle boundingBox;
        Vector2 dimensionLife;
        Vector2 dimensionNiveau;

        public int Hudhauteur
        { get
            { return texture.Height; ; }
            }

        public int level;

        public int GlobalScore;
        public int Vie { get; set; }
        private string Score;
        private string Life;
        private string niveau;

        public Balle balle;
        public Gameplay currentLvlNB;

        public HUD(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            GlobalScore = 0;
            Vie = 3;
            level = 1; 
        }

        public override void Load()
        {
            
        }
        public override void DrawScore()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            AssetsManager Font = ServiceLocator.GetService<AssetsManager>();
            ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();

            Score = "SCORE" + " " + GlobalScore;
            DimensionScore = Font.GetSize(Score, Font.HUDFont);
            float posXScore = 10f;
            float posYScore = texture.Height / 2 - DimensionScore.Y / 2;
           // boundingBox = AssetsManager.getBoundingBox(Score, Font.HUDFont, positiontext);
           // pBatch.DrawRectangle(boundingBox, Color.Red);
            pBatch.DrawString(Font.HUDFont,   
                            Score,
                            new Vector2 (posXScore, posYScore),
                            Color.White);

            Life = "VIE" + " " + Vie;
            dimensionLife = Font.GetSize(Life, Font.HUDFont);
            float posX = ResolutionEcran.Width - dimensionLife.X;
            float posY = texture.Height / 2 - dimensionLife.Y / 2;
            pBatch.DrawString(Font.HUDFont,
                           Life,
                           new Vector2(posX, posY),
                           Color.White);

            niveau = "Lvl : " + " " + level ;
            dimensionNiveau= Font.GetSize(niveau, Font.HUDFont);
            float posXLevel = ResolutionEcran.HalfScreenWidth;
            float posYLevel = texture.Height / 2 - dimensionNiveau.Y / 2;
            pBatch.DrawString(Font.HUDFont,
                            niveau,
                            new Vector2(posXLevel, posYLevel),
                            Color.White);
        }
    }
}
