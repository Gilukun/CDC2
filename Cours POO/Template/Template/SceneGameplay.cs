using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
    // On créer une nouvelle classe pour utiliser les images du joueur
    class Hero : Sprites
    {
        public int Energy;
        public Hero(Texture2D pTexture) : base(pTexture)
        {
            Energy = 100;

        }

        public override void TouchBy(IActor pBy)
        {
            if (pBy is Meteor)
            {
                Energy -= 10;

            }
        }
    }


    class Meteor : Sprites
    { 
        public Meteor (Texture2D pTexture) : base(pTexture) 
        {
            // tant que vx = 0 on va calculer une vitesse x aléatoire
            do
            {
                vx = (float)Template.Utilitaires.GetInt(-3, 3) / 5; // divise par 5 pour avoir une petite valeur, chiffre aléatoire
            } while(vx == 0);

            do 
            { 
                vy = (float)Template.Utilitaires.GetInt(-3, 3) / 5;
            } while(vx == 0);
        }
    
    }


    internal class SceneGameplay : Scene 
    {
        private KeyboardState oldKbState;
        GamePadState oldGamePadState;
        private Hero Ship; 
        public SceneGameplay(MainGame pGame) : base(pGame)
        {
        }


        public override void Load()
        {
            oldKbState = Keyboard.GetState();
            Rectangle Screen = mainGame.Window.ClientBounds; // récupérer les dimensions de l'écran

            // liste des météors
            for (int i=0; i<=20; i++)
            {
                Meteor m = new Meteor(mainGame.Content.Load<Texture2D>("meteor"));
                m.Position = new Vector2(
                                         Template.Utilitaires.GetInt(1, Screen.Width - m.Texture.Width),
                                         Template.Utilitaires.GetInt(1, Screen.Height - m.Texture.Height)
                                        );
                    
                listActors.Add(m);
            }

            Ship = new Hero(mainGame.Content.Load<Texture2D>("ship"));
            // on donne la position de départ du vaissau
            Ship.Position = new Vector2((Screen.Width/2) - Ship.Texture.Width/2 , (Screen.Height / 2) - Ship.Texture.Height / 2);
            // on rajoute notre image à la liste d'acteur 
            listActors.Add(Ship);

            base.Load();


        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle Screen = mainGame.Window.ClientBounds;
            foreach (IActor Actor in listActors)
            {
                if (Actor is Meteor m) // 
                {
                    // ou sinon "Meteor m = (Meteor)Actor;" // pour accéder au propriété du météor il faut l'instancier et le caster en tant qu'actor (mettre entre parenthèse)
                    if (m.Position.X < 0)
                    {
                        m.Position = new Vector2(0, m.Position.Y);
                        m.vx = -m.vx;}

                    if (m.Position.X + m.BoundingBox.Width > Screen.Width) // on peut aussi travailler sur la taille de la texture à la place de boundinBox
                    {
                        m.Position = new Vector2((Screen.Width - m.Texture.Width), m.Position.Y); 
                        m.vx = -m.vx; 
                    }

                    if (m.Position.Y < 0) 
                    {
                        m.Position = new Vector2(m.Position.X, 0);
                        m.vy = -m.vy;  
                    }

                    if (m.Position.Y + m.Texture.Height > Screen.Height)
                    {
                        m.Position = new Vector2(m.Position.X, (Screen.Height - m.BoundingBox.Height)); 
                        m.vy = -m.vy; 
                    }

                    if (Utilitaires.CollideByBox(m, Ship))
                    {
                        Ship.TouchBy(m);
                        m.TouchBy(Ship);

                    }
                }

            }

            KeyboardState NewKbState = Keyboard.GetState();
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One); // vérifier si la manette est branchée.
            GamePadState newGamePadState;
            bool butB = false;

            if (capabilities.IsConnected)
            {
                newGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes); // independantaxis pour récupérer les directions des sticks. Et la zone morte permet de pas avoir de drift
                if (newGamePadState.IsButtonDown(Buttons.B) && !(oldGamePadState.IsButtonDown(Buttons.B)))
                {
                    butB = true;
                    Trace.WriteLine("ZOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOM");
                }
                oldGamePadState = newGamePadState;

                if (newGamePadState.IsButtonDown(Buttons.LeftThumbstickUp) || newGamePadState.IsButtonDown(Buttons.DPadUp))
                    { Ship.Move(0, -1); }
                if (newGamePadState.IsButtonDown(Buttons.LeftThumbstickDown) || newGamePadState.IsButtonDown(Buttons.DPadDown))
                    { Ship.Move(0, 1); }
                if (newGamePadState.IsButtonDown(Buttons.LeftThumbstickRight) || newGamePadState.IsButtonDown(Buttons.DPadRight))
                    { Ship.Move(1, 0); }
                if (newGamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) || newGamePadState.IsButtonDown(Buttons.DPadLeft))
                    { Ship.Move(-1, 0); }
                 oldGamePadState = newGamePadState;
            }
            

            if (NewKbState.IsKeyDown(Keys.Up)) 
                {Ship.Move(0, -1);}
            if (NewKbState.IsKeyDown(Keys.Down))
                { Ship.Move(0, 1);}
            if (NewKbState.IsKeyDown(Keys.Right))
            { Ship.Move(1, 0); }
            if (NewKbState.IsKeyDown(Keys.Left))
                { Ship.Move(-1, 0); }
            oldKbState = NewKbState;

           


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            mainGame._spriteBatch.DrawString(AssetManager.MainFont,
                                             "This is the GAMEPLAY",
                                             new Vector2(2, 1),
                                             Color.White);
            base.Draw(gameTime); 
        }
    }

}
