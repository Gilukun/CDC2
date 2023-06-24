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

        public Texture2D texture;
        public int HauteurBarre { get; set; }
        public int GlobalScore;
        public int Vie { get; set; }
        private string Score;
        private string Life;
        public Balle balle;
        public HUD(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
            HauteurBarre = texture.Height;
            GlobalScore = 0;
            Vie = 3;
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
            Vector2 DimensionScore = AssetsManager.GetSize(Score, Font.HUDFont);
            Vector2 positiontext = new Vector2(0, texture.Height / 2 - DimensionScore.Y / 2);
            Rectangle boundingBox = AssetsManager.getBoundingBox(Score, Font.HUDFont, positiontext);
           // pBatch.DrawRectangle(boundingBox, Color.Red);
            pBatch.DrawString(Font.HUDFont,   
                            Score,
                            new Vector2 (0, texture.Height/2 - DimensionScore.Y/2),
                            Color.White);

            Life = "VIE" + " " + Vie;
            Vector2 DimensionLife = AssetsManager.GetSize(Life, Font.HUDFont);
            pBatch.DrawString(Font.HUDFont,
                           Life,
                           new Vector2( ResolutionEcran.Width - DimensionLife.X , texture.Height / 2 - DimensionLife.Y / 2),
                           Color.White);


        }

    }
}
