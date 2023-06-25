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
        Texture2D background;

        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        GameState Status = ServiceLocator.GetService<GameState>();

        private GUI BoutonEnter;
        private GUI BoutonSettings;
        private List<GUI> listeBouttons;

        private MouseState oldMState;
        private string Titre;
        private Vector2 DimensionTitre;
        private string Start;
        private string Settings;
        private Vector2 DimensionStart;
        private Vector2 DimensionSettings;
        private int spacing;

        AssetsManager Font = ServiceLocator.GetService<AssetsManager>();
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        AssetsManager Audio = ServiceLocator.GetService<AssetsManager>();
        SoundEffectInstance Selection;

        private float Delay;
        private float Timer;
        private bool TimerIsOn;
        public Menu()
        {
            background = _content.Load<Texture2D>("BckMenu");
            MediaPlayer.Play(Audio.Intro);
            Timer = 2;
            Delay = 0;
        }

        public void TimerON(float pIncrement)
        {
            Delay += pIncrement;
        }

        public void OnClick(GUI pSender)
        {
            if (pSender == BoutonEnter)
            {
                Audio.PlaySFX(Selection, Audio.Select);
                TimerIsOn = true;  
            }
            else if (pSender == BoutonSettings)
            {
                Audio.PlaySFX(Selection, Audio.Select);
                Status.ChangeScene(GameState.Scenes.Setting);
                MediaPlayer.Stop();
            }

        }

        public override void Load()
        {
            
            listeBouttons = new List<GUI>();
            BoutonEnter = new GUI(_content.Load<Texture2D>("Button1"));
            BoutonEnter.SetPosition(ResolutionEcran.CenterWidth, ResolutionEcran.CenterHeight);

            int LargeurBouton = BoutonEnter.LargeurSprite;
            int HauteurBouton = BoutonEnter.HauteurSprite;
            spacing = HauteurBouton / 2;

            BoutonEnter.onClick = OnClick;
            listeBouttons.Add(BoutonEnter);

            BoutonSettings = new GUI(_content.Load<Texture2D>("Button1"));
            BoutonSettings.SetPosition(ResolutionEcran.CenterWidth, BoutonEnter.Position.Y + HauteurBouton + spacing);
            BoutonSettings.onClick = OnClick;

            listeBouttons.Add(BoutonSettings);
            oldMState = Mouse.GetState();

            Titre = "FANTASOID";
            DimensionTitre = AssetsManager.GetSize(Titre, Font.TitleFont);

            Start = "START";
            DimensionStart = AssetsManager.GetSize(Start, Font.MenuFont);

            Settings = "SETTINGS";
            DimensionSettings = AssetsManager.GetSize(Settings, Font.MenuFont);
        }

        public override void Update()
        {
            if (TimerIsOn)
            {
                TimerON(0.03f);
            }
            if (Delay > Timer)
            {
                Status.ChangeScene(GameState.Scenes.Gameplay);
                Delay = 0;
                TimerIsOn = false;
            }

            BoutonEnter.Update();
            BoutonSettings.Update();
            base.Update();
        }

        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);

            BoutonEnter.Draw();

            BoutonSettings.Draw();
            pBatch.DrawString(Font.TitleFont,
                              "FANTASOID",
                              new Vector2(ResolutionEcran.CenterWidth - DimensionTitre.X / 2, 100 - DimensionTitre.Y / 2),
                              Color.DarkSlateBlue);

            Color color;
            foreach (GUI item in listeBouttons)
            {
                if (item.IsHover)
                {
                    color = Color.Red;
                }
                else
                {
                    color = Color.Black;
                }
                if (item == BoutonEnter)
                {
                    pBatch.DrawString(Font.MenuFont,
                                             "START",
                                             new Vector2(BoutonEnter.Position.X - DimensionStart.X / 2, BoutonEnter.Position.Y - DimensionStart.Y / 2),
                                             color);
                }
                else if (item == BoutonSettings)
                {
                    pBatch.DrawString(Font.MenuFont,
                                     "SETTINGS",
                                     new Vector2(BoutonSettings.Position.X - DimensionSettings.X / 2, BoutonSettings.Position.Y - DimensionSettings.Y / 2),
                                     color);
                }

            }
        }
    }
}