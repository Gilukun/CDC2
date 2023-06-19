using Microsoft.Xna.Framework;
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
        Texture2D background;
      
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();

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

        AssetsManager Font =   ServiceLocator.GetService<AssetsManager>();

        public Menu(CasseBriques pGame) : base (pGame) 
        {
            background = pGame.Content.Load<Texture2D>("BckMenu");
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
            BoutonEnter.SetPosition(ResolutionEcran.CenterWidth, ResolutionEcran.CenterHeight);
            BoutonEnter.onClick = OnClick;
            listeBouttons.Add(BoutonEnter);

            BoutonSettings = new GUI(casseBriques.Content.Load<Texture2D>("Button1"));
            BoutonSettings.SetPosition(ResolutionEcran.CenterWidth, 500);
            BoutonSettings.onClick = OnClick;
            listeBouttons.Add(BoutonSettings);
            oldMState = Mouse.GetState();

            Titre = "FANTASOID";
            DimensionTitre =  AssetsManager.GetSize(Titre,Font.TitleFont) ;
            
            Start = "START";
            DimensionStart = AssetsManager.GetSize(Start, Font.MenuFont);

            Settings = "SETTINGS";
            DimensionSettings = AssetsManager.GetSize(Settings, Font.MenuFont);
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
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
            BoutonEnter.Draw();
            BoutonSettings.Draw();
            pBatch.DrawString(Font.TitleFont, 
                              "FANTASOID", 
                              new Vector2 (ResolutionEcran.CenterWidth- DimensionTitre.X /2, 100 - DimensionTitre.Y/2), 
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
