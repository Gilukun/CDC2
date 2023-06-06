using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.Intrinsics.X86;
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
            } while(vy == 0);
        }


    }

    class Bullet : Sprites
    {
       
        public Bullet(Texture2D pTexture) : base(pTexture)
        {
        }

        public override void Shoot(Vector2 pPosition, Vector2 pdirection, float pSpeed)
        {
            Position = pPosition;
            Direction = pdirection;
            Speed = pSpeed;

        }

    }


    internal class SceneGameplay : Scene 
    {
        private KeyboardState oldKbState;
        GamePadState oldGamePadState;
        private Hero Ship;
        private Song musicGp;
        private SoundEffect sndExplode;
        private Texture2D bullets;
        private MouseState newMState;
        private MouseState oldMState;

        public SceneGameplay(MainGame pGame) : base(pGame)
          

        {
            // On passe l'asset pour la musique directement dans le constructeur.
            /*
            musicGp = AssetManager.MusicGameplay;
            MediaPlayer.Play(musicGp);
            MediaPlayer.IsRepeating = true;
            */
        }


        public override void Load()
        {
            oldKbState = Keyboard.GetState();
            Rectangle Screen = mainGame.Window.ClientBounds; // récupérer les dimensions de l'écran

            //METEOR
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
            
            // HERO
            Ship = new Hero(mainGame.Content.Load<Texture2D>("ship"));
            // on donne la position de départ du vaissau
            Ship.Position = new Vector2((Screen.Width/2) , (Screen.Height / 2));
            // on rajoute notre image à la liste d'acteur 
            listActors.Add(Ship);


            //MUSIC & SFX
            // On peut mettre la musique a la main dans chaque scene ou alors créer un asset dans Asset manager et le passer dans le constructeur.
            musicGp = mainGame.Content.Load<Song>("techno");
            MediaPlayer.Play(musicGp);
            MediaPlayer.IsRepeating = true;

            sndExplode = mainGame.Content.Load<SoundEffect>("explode");

            // Balles

            oldMState = Mouse.GetState();
            bullets = mainGame.Content.Load<Texture2D>("Obus");
            
            base.Load();

        }

        public override void Unload()
        {
            MediaPlayer.Stop();
            base.Unload();
        }

       
        public override void Update(GameTime gameTime)
        {
            if (newMState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)
            {
                Trace.WriteLine("Je clique");
                Bullet nB = new Bullet(bullets);
                nB.Position = new Vector2(Ship.Position.X, Ship.Position.Y);
                Vector2 direction = Vector2.Normalize(new Vector2(newMState.X, newMState.Y) - Ship.Position);
                Trace.WriteLine(direction);
                nB.Shoot(nB.Position, direction, 10f);

                listActors.Add(nB);

            }
            oldMState = newMState;
            // Déplacement et collisions
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
                        m.ToRemove = true;
                        sndExplode.Play();
                    }
                    
                }
                else if (Actor is Bullet b)
                {
                    foreach ( IActor OtherActor in listActors)
                        if (OtherActor is Meteor m2 && (Utilitaires.CollideByBox(b, m2)))
                            {
                                m2.TouchBy(b);
                                b.TouchBy(m2);
                                m2.ToRemove = true;
                                b.ToRemove = true;
                                sndExplode.Play();
                            }
                }

            }

            newMState = Mouse.GetState();



            Clean(); // fonction dans la gestion de la scène globale
           
            // ==============================================================================
            KeyboardState NewKbState = Keyboard.GetState();
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One); // vérifier si la manette est branchée.
            GamePadState newGamePadState;
            bool butB = false;

            // Déplacements à la manette
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
            //=============================================================================================
            // Déplacements au clavier
            if (NewKbState.IsKeyDown(Keys.Z)) 
                {Ship.Move(0, -1);}
            if (NewKbState.IsKeyDown(Keys.S))
                { Ship.Move(0, 1);}
            if (NewKbState.IsKeyDown(Keys.D))
            { Ship.Move(1, 0); }
            if (NewKbState.IsKeyDown(Keys.Q))
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
