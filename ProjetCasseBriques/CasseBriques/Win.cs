using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    internal class Win : ScenesManager
    {
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        AssetsManager Font = ServiceLocator.GetService<AssetsManager>();
        SpriteBatch _spriteBatch = ServiceLocator.GetService<SpriteBatch>();
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        AssetsManager Audio = ServiceLocator.GetService<AssetsManager>();

        Texture2D background;
        private string win;
        private string BackToMenu;

        Vector2 DimensionWin;
        Vector2 DimensionBackToMenu;

        private float fadeSpeed;
        private float currentAlpha;
        Color textColor = Color.Black;

        private float blinkSpeed;
        private float blinkMax;
        private bool textVisible;
        private float blinkTimer;

        private List<Balle> listeBalles = new List<Balle>();
        public Win()
        {
            background = _content.Load<Texture2D>("Backgrounds\\Back_7");
            MediaPlayer.Play(Audio.End);
        }

        public override void Load()
        {
            win = "YOU WIN";
            DimensionWin = Font.GetSize(win, Font.Victory);
            currentAlpha = 0;
            fadeSpeed = 0.002f;

            BackToMenu = "Appuyez sur M pour revenir au Menu";
            DimensionBackToMenu = Font.GetSize(BackToMenu, Font.ContextualFont);
            blinkSpeed = 0.05f;
            blinkTimer = 0;
            blinkMax = 4;
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

            textColor = new Color(Color.DarkRed, currentAlpha);
            pBatch.DrawString(Font.Victory,
                             win,
                             new Vector2(ResolutionEcran.HalfScreenWidth - DimensionWin.X / 2, ResolutionEcran.CenterHeight - DimensionWin.Y / 2),
                             textColor);


            if (textVisible)
            {
                pBatch.DrawString(Font.ContextualFont,
                                 BackToMenu,
                                 new Vector2(ResolutionEcran.HalfScreenWidth - DimensionBackToMenu.X / 2, ResolutionEcran.CenterHeight + DimensionWin.Y / 2),
                                 Color.DarkCyan);
            }
        }

    }
}
