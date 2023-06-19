﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Menu : ScenesManager
    {
        private GUI BoutonEnter;
        private GUI BoutonSettings;
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        private MouseState oldMState;
        private MouseState newMState;
        private List<GUI> listeBouttons;
       
        private string Titre;
        private Vector2 DimensionTitre;
        private string Start;
        private string Settings;
        private List<string> MenuList;
       

        private Vector2 DimensionStart;
        private Vector2 DimensionSettings;
        GameState gameState;


        public Menu(CasseBriques pGame) : base (pGame) 
        {
            
        }

        public void OnClick(GUI pSender)
        {
            if (pSender == BoutonEnter)
            {
                casseBriques.gameState.ChangeScene(GameState.Scenes.Gameplay);
                //casseBriques.gameState.ChangeScene(GameState.Scenes.Gameplay);
            }
            if (pSender == BoutonSettings)
            {
                casseBriques.gameState.ChangeScene(GameState.Scenes.Setting);
            }

        }

        
        public override void Load()
        {
            listeBouttons = new List<GUI>();
            BoutonEnter = new GUI(casseBriques.Content.Load<Texture2D>("Button1"));
            BoutonEnter.SetPosition(ResolutionEcran.CenterWidth, 200);
            BoutonEnter.onClick = OnClick;
            listeBouttons.Add(BoutonEnter);


            BoutonSettings = new GUI(casseBriques.Content.Load<Texture2D>("Button1"));
            BoutonSettings.SetPosition(ResolutionEcran.CenterWidth, 500);
            BoutonSettings.onClick = OnClick;
            listeBouttons.Add(BoutonSettings);
            oldMState = Mouse.GetState();

            Titre = "FANTASOID";
            DimensionTitre =  AssetsManager.GetSize(Titre,AssetsManager.TitleFont) ;
            
            Start = "START";
            DimensionStart = AssetsManager.GetSize(Start, AssetsManager.MenuFont);
            Settings = "SETTINGS";
            DimensionSettings = AssetsManager.GetSize(Settings, AssetsManager.MenuFont);

        }

        
        public override void Update()
        {
            BoutonEnter.Update();
            BoutonSettings.Update();
            base.Update();
        }
      

        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            BoutonEnter.Draw();
            BoutonSettings.Draw();
            pBatch.DrawString(AssetsManager.TitleFont, 
                              "FANTASOID", 
                              new Vector2 (ResolutionEcran.CenterWidth- DimensionTitre.X /2, 100 - DimensionTitre.Y/2), 
                              Color.WhiteSmoke);

            
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
                    pBatch.DrawString(AssetsManager.MenuFont,
                                             "START",
                                             new Vector2(BoutonEnter.Position.X - DimensionStart.X / 2, BoutonEnter.Position.Y - DimensionStart.Y / 2),
                                             color);
                }
                else if (item == BoutonSettings) 
                {
                    pBatch.DrawString(AssetsManager.MenuFont,
                                     "SETTINGS",
                                     new Vector2(BoutonSettings.Position.X - DimensionSettings.X / 2, BoutonSettings.Position.Y - DimensionSettings.Y / 2),
                                     color);
                }
            
            }
        }
    }
}
