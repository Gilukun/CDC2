﻿using Microsoft.Xna.Framework;
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
    public class GameOver : ScenesManager
    {
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        AssetsManager font = ServiceLocator.GetService<AssetsManager>();
        ScreenManager screen = ServiceLocator.GetService<ScreenManager>();
        AssetsManager audio = ServiceLocator.GetService<AssetsManager>();

        Texture2D background;
        private string gOver;
        private string backToMenu;

        Vector2 dimensionGameOver;
        Vector2 dimensionBackToMenu;

        private float fadeSpeed;
        private float currentAlpha;

        private float blinkSpeed;
        private float blinkMax;
        private bool textVisible;
        private float blinkTimer;
       
        public GameOver()
        {
            background = _content.Load<Texture2D>("Backgrounds\\Back_7");
            MediaPlayer.Play(audio.End);
        }

        public override void Load()
        {
            gOver = "GAMEOVER";
            dimensionGameOver = font.GetSize(gOver, font.GameOverFont);
            currentAlpha = 0;
            fadeSpeed = 0.005f;

            backToMenu = "Appuyez sur M pour revenir au Menu";
            dimensionBackToMenu = font.GetSize(backToMenu, font.ContextualFont);
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
            if (blinkTimer >= blinkMax) 
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
            
            Color textColor = new Color(Color.Black, currentAlpha);
            pBatch.DrawString(font.GameOverFont,
                             gOver,
                             new Vector2(screen.HalfScreenWidth - dimensionGameOver.X/2, screen.CenterHeight - dimensionGameOver.Y/2),
                             textColor);

            if (textVisible)
            {
                pBatch.DrawString(font.ContextualFont,
                                 backToMenu,
                                 new Vector2(screen.HalfScreenWidth - dimensionBackToMenu.X / 2, screen.CenterHeight + dimensionGameOver.Y / 2),
                                 Color.DarkCyan);
            }
        }
    }
}
