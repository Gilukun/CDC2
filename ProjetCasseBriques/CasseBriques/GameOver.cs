using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class GameOver : ScenesManager
    {
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        AssetsManager Font = ServiceLocator.GetService<AssetsManager>();
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
       
        
        Texture2D background;
        private string gOver;
        Vector2 DimensionGameOver;
        private float fadeSpeed;
        private float currentAlpha;
        Color textColor = Color.Black;

        public GameOver()
        {
            background = _content.Load<Texture2D>("BkGameOver");
        }

        public override void Load()
        {
            gOver = "GAMEOVER";
            DimensionGameOver = AssetsManager.GetSize(gOver, Font.GameOverFont);
            currentAlpha = 0;
            fadeSpeed = 0.05f;
            base.Load();
        }

       
        public void UpdateTxt()
        {
            if (currentAlpha < 1)
            {
                currentAlpha += fadeSpeed;
                if (currentAlpha >= 1)
                {
                    currentAlpha = 1;
                }
            }
            
            Trace.WriteLine(textColor);
        }

        public override void Update()
        {
            UpdateTxt();
            base.Update();
        }
        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
            textColor = new Color(Color.Black, currentAlpha);
            pBatch.DrawString(Font.GameOverFont,
                             "GAMEOVER",
                             new Vector2(ResolutionEcran.CenterWidth - DimensionGameOver.X/2, ResolutionEcran.CenterHeight - DimensionGameOver.Y/2),
                             textColor);
           
        }
    }
}
