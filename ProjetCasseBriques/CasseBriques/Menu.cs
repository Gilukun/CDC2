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
        private GUI BoutonEnter;
        private GUI BoutonSettings;
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        private MouseState oldMState;
        private MouseState newMState;
        private List<GUI> listeBouttons;
        private string NomJeu;
        private Vector2 tailleNJeu;
        private float tailleNJeuWidth;
        private float tailleNJeuHeight;
       
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

            NomJeu = "FANTASOID";
            tailleNJeu = AssetsManager.TitleFont.MeasureString(NomJeu);
            tailleNJeuWidth = tailleNJeu.X;
            tailleNJeuHeight = tailleNJeu.Y;    
          
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
                              new Vector2 (ResolutionEcran.Width/2 - tailleNJeuWidth/2, ResolutionEcran.Height/2 - tailleNJeuHeight/2), 
                              Color.WhiteSmoke); 
        }
    }
}
