using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Template;

namespace Template
{
   
    internal class SceneMenu : Scene
    {
        KeyboardState oldKbState;
        GamePadState oldGamePadState;
        MouseState newMState;
        private Boutons myButton;
        public SceneMenu(MainGame pGame) : base (pGame)
        {
            Trace.WriteLine("je suis un menu");
        }

        public void OnClickPlay(Boutons pSender)
        {
            mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);
        }

        public override void Load()
        {
            Trace.WriteLine("Je load le menu");
            Rectangle Screen = mainGame.Window.ClientBounds;
            myButton = new Boutons(mainGame.Content.Load<Texture2D>("button"));
            myButton.Position = new Vector2(
                                            (Screen.Width / 2) - myButton.Texture.Width / 2,
                                            (Screen.Height / 2) - myButton.Texture.Height / 2
                                            );
            myButton.onClick = OnClickPlay;

            listActors.Add(myButton);

            oldKbState = Keyboard.GetState();
            oldGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);
            base.Load();

        }

        public override void Unload()
        {
            Trace.WriteLine("J'unload le menu");
            base.Unload();
        }

        public override void Update(GameTime gameTime)

        {
            KeyboardState NewKbState = Keyboard.GetState();
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One); // vérifier si la manette est branchée.
            GamePadState newGamePadState;
            bool butX = false;
            // MANETTE
            if (capabilities.IsConnected)
            {
                newGamePadState=  GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes); // independantaxis pour récupérer les directions des sticks. Et la zone morte permet de pas avoir de drift
                if (newGamePadState.IsButtonDown(Buttons.X) && !(oldGamePadState.IsButtonDown(Buttons.X)))
                {
                    butX = true;
                    mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);
                }
            }

            // SOURIS
            newMState = Mouse.GetState();
            if( newMState.LeftButton == ButtonState.Pressed ) 
            { 
            }

            // CLAVIER
            if (NewKbState.IsKeyDown(Keys.Enter) && 
                !oldKbState.IsKeyDown(Keys.Enter)
                || butX)
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);

            }
            oldKbState = NewKbState;

  

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            
            mainGame._spriteBatch.DrawString(AssetManager.MainFont,
                                             "This is the MENU",
                                             new Vector2(2, 1), 
                                             Color.White);
            base.Draw(gameTime);
        }

    }



}
