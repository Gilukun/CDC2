using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Menu : ScenesManager
    {
        ScreenManager Screen = ServiceLocator.GetService<ScreenManager>();
        GameState status = ServiceLocator.GetService<GameState>();
        AssetsManager font = ServiceLocator.GetService<AssetsManager>();
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        AssetsManager audio = ServiceLocator.GetService<AssetsManager>();

        private GUI boutonEnter;
        private GUI boutonSettings;
        private List<GUI> listeBoutons;
        Texture2D background;
        private string titre;
        private Vector2 dimensionTitre;
        private string start;
        private string settings;
        private Vector2 dimensionStart;
        private Vector2 dimensionSettings;
        private int spacing;

        Balle mouseIcon;

        private float delay;
        private float timer;
        private bool timerIsOn;
        public Menu()
        { 
            background = _content.Load<Texture2D>("BckMenu");
            mouseIcon = new Balle (_content.Load<Texture2D>("bMenu"));
            timer = 2;
            delay = 0;
            audio.PlaySong(audio.Intro);
        }

        public void TimerON(float pIncrement)
        {
            delay += pIncrement;
        }

        public void OnClick(GUI pSender)
        {
            if (pSender == boutonEnter)
            {
                audio.PlaySFX(audio.Select);
                timerIsOn = true;  
            }
            else if (pSender == boutonSettings)
            {
                audio.PlaySFX(audio.Select);
                status.ChangeScene(GameState.Scenes.Setting);
                audio.Stop();
            }

        }

        public override void Load()
        {
            
            listeBoutons = new List<GUI>();
            boutonEnter = new GUI(_content.Load<Texture2D>("Bouton_2"));
            boutonEnter.SetPosition(Screen.HalfScreenWidth, Screen.CenterHeight);

            int LargeurBouton = boutonEnter.SpriteWidth;
            int HauteurBouton = boutonEnter.SpriteHeight;
            spacing = HauteurBouton / 2;

            boutonEnter.onClick = OnClick;
            listeBoutons.Add(boutonEnter);

            boutonSettings = new GUI(_content.Load<Texture2D>("Bouton_2"));
            boutonSettings.SetPosition(Screen.HalfScreenWidth, boutonEnter.Position.Y + HauteurBouton + spacing);
            boutonSettings.onClick = OnClick;
            listeBoutons.Add(boutonSettings);

            titre = "FANTASOID";
            dimensionTitre = font.GetSize(titre, font.TitleFont);

            start = "START";
            dimensionStart = font.GetSize(start, font.HUDFont);

            settings = "SETTINGS";
            dimensionSettings = font.GetSize(settings, font.HUDFont);
        }

        public override void Update()
        {
            mouseIcon.SetPosition((Mouse.GetState().X - mouseIcon.HalfHeitgh), (Mouse.GetState().Y - mouseIcon.HalfHeitgh));
            if (timerIsOn)
            {
                TimerON(0.03f);
            }
            if (delay > timer)
            {
                status.ChangeScene(GameState.Scenes.Gameplay);
                delay = 0;
                timerIsOn = false;
            }

            boutonEnter.Update();
            boutonSettings.Update();
            base.Update();
        }

        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);

            // Dessin des boutons
            boutonEnter.Draw();
            boutonSettings.Draw();

            // Affichage du nom du jeu 
            pBatch.DrawString(font.TitleFont,
                              "FANTASOID",
                              new Vector2(Screen.HalfScreenWidth - dimensionTitre.X / 2, 100 - dimensionTitre.Y / 2),
                              Color.DarkSlateBlue);

            // Affichage des textes au dessus des boutons
            Color color;
            foreach (GUI item in listeBoutons)
            {
                if (item.IsHover)
                {
                    color = Color.Red;
                }
                else
                {
                    color = Color.DarkMagenta;
                }
                if (item == boutonEnter)
                {
                    pBatch.DrawString(font.HUDFont,
                                             "START",
                                             new Vector2(boutonEnter.Position.X - dimensionStart.X / 2, boutonEnter.Position.Y - dimensionStart.Y / 2),
                                             color);
                }
                else if (item == boutonSettings)
                {
                    pBatch.DrawString(font.HUDFont,
                                     "SETTINGS",
                                     new Vector2(boutonSettings.Position.X - dimensionSettings.X / 2, boutonSettings.Position.Y - dimensionSettings.Y / 2),
                                     color);
                }
            }

            // Icone de la souris
            pBatch.Draw(mouseIcon.texture, new Vector2(mouseIcon.Position.X, mouseIcon.Position.Y), Color.White);
        }
    }
}