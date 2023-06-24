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
        SpriteBatch _spriteBatch = ServiceLocator.GetService<SpriteBatch>();
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        private GraphicsDevice graphicsDevice;

        Texture2D background;
        private string gOver;
        private string BackToMenu;

        Vector2 DimensionGameOver;
        Vector2 DimensionBackToMenu;

        private float fadeSpeed;
        private float currentAlpha;
        Color textColor = Color.Black;
        private float blinkSpeed;
        private float blinkMax;
        private bool textVisible;
        private float blinkTimer;
        Texture2D TextRect;
        Color rectColor;

        private Rectangle backText;
       
        public GameOver()
        {
            background = _content.Load<Texture2D>("BkGameOver");
            graphicsDevice = _spriteBatch.GraphicsDevice;
        }

        public override void Load()
        {
            gOver = "GAMEOVER";
            DimensionGameOver = AssetsManager.GetSize(gOver, Font.GameOverFont);
            currentAlpha = 0;
            fadeSpeed = 0.005f;

            BackToMenu = "Appuyez sur M pour revenir au Menu";
            DimensionBackToMenu = AssetsManager.GetSize(BackToMenu, Font.ContextualFont);
            blinkSpeed = 0.05f;
            blinkTimer = 0;
            blinkMax = 4;
            TextRect = new Texture2D(graphicsDevice, 1, 1);
            TextRect.SetData(new[] { Color.DarkSlateBlue });

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

            textVisible = true;
            blinkTimer += blinkSpeed;
            if (blinkTimer >= 2)
            {
                textVisible = false;
            }
            if (blinkTimer >= 4) 
            {
                blinkTimer = 0;
            }
            
            Trace.WriteLine(blinkTimer);
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

            backText = new Rectangle(ResolutionEcran.CenterWidth - (int)(DimensionBackToMenu.X / 2), ResolutionEcran.CenterHeight + (int)(DimensionGameOver.Y / 2), 
                                    (int)DimensionBackToMenu.X,  (int)DimensionBackToMenu.Y);
            rectColor = new Color(Color.White, 0.6f);
            pBatch.Draw(TextRect, backText, rectColor);

            if (textVisible)
            {
                pBatch.DrawString(Font.ContextualFont,
                                 BackToMenu,
                                 new Vector2(ResolutionEcran.CenterWidth - DimensionBackToMenu.X / 2, ResolutionEcran.CenterHeight + DimensionGameOver.Y / 2),
                                 Color.DarkCyan);
            }
        }
    }
}
