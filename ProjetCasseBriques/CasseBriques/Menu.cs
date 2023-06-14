using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
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
       
        public Menu(CasseBriques pGame) : base (pGame) 
        {
            
        }

        public void OnClick(GUI pSender)
        {
            if (pSender == BoutonEnter)
            {
                casseBriques.gameState.ChangeScene(GameState.Scenes.Gameplay);
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
            BoutonEnter.SetPosition(ResolutionEcran.Width / 2 - BoutonEnter.LargeurSprite / 2, 200);
            BoutonEnter.onClick = OnClick;
            listeBouttons.Add(BoutonEnter);


            BoutonSettings = new GUI(casseBriques.Content.Load<Texture2D>("Button1"));
            BoutonSettings.SetPosition(ResolutionEcran.Width / 2 - BoutonSettings.LargeurSprite / 2, 500);
            BoutonSettings.onClick = OnClick;
            listeBouttons.Add(BoutonSettings);

            oldMState = Mouse.GetState();
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
           
        }
    }
}
