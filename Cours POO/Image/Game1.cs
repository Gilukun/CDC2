using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation.DirectX;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using Color = Microsoft.Xna.Framework.Color;

namespace Image
{

    public class Jelly
    { 
        public Vector2 position;
        public int rotation;
        public int vitesseX;
        public int vitesseY;
        public float scale;
        public float scaleVitesse;
        public float coef;
        public int oldVitesseX;
        public int oldVitesseY;
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D img;
        Texture2D slime;
        
        List<Jelly> lstJelly;
        Random rnd;

        MouseState ancienEtatSouris; // garder l'état d'origine de la souris pour que ça clique qu'une fois par deltatime

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            lstJelly = new List<Jelly>();
            rnd = new Random();
          
            //scale = 1.0f;
            //scaleVitesse =- 0.01f;  
        }

        protected override void Initialize()
        {
            // Ici on itinialize tout ce qui est non graphique. (position etc..) 
           // position = new Vector2(100, 100); // Pour aficher l'image il faut d'abord créer un veteur des positions X et Y. 
          
 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice); // gère le contexte graphique et affiche que les textures s'affichent dans le bon order et gère les performances

            // Ici on charge le contenu du jeu (graphique principalement)

           img= this.Content.Load<Texture2D>("personnage"); // this = notre jeu.Vient de la ligne 16.  Content.load + le type de texture <> et enfin le nom de l'image après qu'elle soit généré dans Content
           slime = this.Content.Load<Texture2D>("metalPanel");
  


            for (int i= 1; i <=10 ; i++) 
            {
                Jelly myJelly = new Jelly();
                int y = rnd.Next(0, GraphicsDevice.Viewport.Height - slime.Height);
                int x = rnd.Next(0, GraphicsDevice.Viewport.Width - slime.Width);
                myJelly.position = new Vector2(x, y);
                myJelly.vitesseX = rnd.Next(1, 5);
                myJelly.vitesseY = rnd.Next(-1, 10);
                myJelly.scale = 1.0f;
                myJelly.scaleVitesse = 0.01f;
                myJelly.coef = 0.5f;
                myJelly.oldVitesseX = 0;
                myJelly.oldVitesseY = 0;
                lstJelly.Add(myJelly);
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update du jeu
            /*position.X = position.X + vitesseX * (float)gameTime.ElapsedGameTime.TotalSeconds; // le deltatime s'écrit (float)gameTime.ElapsedGameTime.TotalSeconds
            position.Y = position.Y + vitesseY * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.X + slime.Width > GraphicsDevice.Viewport.Width)
            { 
                vitesseX = -vitesseX; 
            }
            if (position.X < 0)
            {
                vitesseX = -vitesseX;
            }

            if (position.Y + slime.Height > GraphicsDevice.Viewport.Height)
            {
                vitesseY = -vitesseY;
            }
            if (position.Y < 0)
            {
                vitesseY = -vitesseY;
            }
            */

            // Detection de la souris

            MouseState etatSouris = Mouse.GetState(); // on accède aux états de la souris
            bool bClic = false;
            if (etatSouris.LeftButton == ButtonState.Pressed && ancienEtatSouris.LeftButton == ButtonState.Released )
            {
                Trace.WriteLine("je clique la souris");
                bClic = true;
            }
            ancienEtatSouris = etatSouris; // ne pas oublier de dire que l'état change sinon on reste toujours dans l'état pas cliqué 

            bool selectionOK = false;
            
            for( int i = lstJelly.Count-1; i >= 0; i-- )
            {
                Jelly item = lstJelly[i];
                item.position.X += item.vitesseX;
                item.position.Y += item.vitesseY;

                if (item.position.X < 0)
                {
                    item.vitesseX = - item.vitesseX;
                }
                if (item.position.X > GraphicsDevice.Viewport.Width - slime.Width)
                {
                    item.vitesseX = -item.vitesseX;
                }

                if (item.position.Y + slime.Height > GraphicsDevice.Viewport.Height)
                {
                    item.vitesseY = -item.vitesseY;
                }
                if (item.position.Y < 0)
                {
                    item.vitesseY = - item.vitesseY;
                }

                

                if (bClic && selectionOK== false)
                {
                    if (etatSouris.X >= item.position.X &&
                        etatSouris.Y >= item.position.Y &&
                        etatSouris.X <= item.position.X + slime.Width &&
                        etatSouris.Y <= item.position.Y + slime.Height) 
                    {
                        Trace.WriteLine("J'ai cliqué une image");
                        selectionOK = true;
                        item.oldVitesseX = item.vitesseX; 
                        item.oldVitesseY = item.vitesseY;
                        item.vitesseX = 0;
                        item.vitesseY = 0;
                    }
                    else
                    {
                  
                        item.vitesseX = item.oldVitesseX; 
                        item.vitesseY = item.oldVitesseX; 

                    }
                }

                item.scale += item.scaleVitesse;
                if (item.scale <= 0.5f)
                {
                    item.scale = 0.5f;
                    item.scaleVitesse = -item.scaleVitesse;


                }
                if (item.scale > 1)

                {
                    item.scale = 1.0f;
                    item.scaleVitesse = - item.scaleVitesse;
                }

                // test si item est cliqué

                
            }

            
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Le jeu tourne à 60img/s et donc affiche tout 60i/s

           /* _spriteBatch.Begin(); // on démmare le contexte d'affichage
             // Pour aficher l'image il faut d'abord créer un veteur des positions X et Y. 

            _spriteBatch.Draw(img,position, Color.White);

            _spriteBatch.End(); // on termine notre contexte d'affichage*/

            _spriteBatch.Begin(); // on démmare le contexte d'affichage

            //if (vitesseY > 0)
            //effet = SpriteEffects.FlipVertically;
            SpriteEffects effet;
            

            for (int i= lstJelly.Count-1; i>=0; i--)
            {
                Jelly item = new Jelly();
                effet = SpriteEffects.None;
                if (item.vitesseX > 0)
                    effet = SpriteEffects.FlipHorizontally;
                _spriteBatch.Draw(slime,
                                   item.position, // position x et y
                                   null, // dessiner un rectangle.. on n'en a pas besoin donc null
                                   Color.White * item.coef, // teinte de l'image. Blanc = normal
                                   0, // rotation
                                   new Vector2(0, 0), // origine de l'image soit new Vector2(0,0)
                                   new Vector2(1.0f, item.scale), // déformation, normal = 1
                                   effet, // effet de la texture ici symmétrie
                                   0 // profondeur du calque. 0 par défaut.
                                   );
                if (selectionOK)
                {
                    _spriteBatch.Draw(slime,
                                   item.position, // position x et y
                                   null, // dessiner un rectangle.. on n'en a pas besoin donc null
                                   Color.Red * item.coef, // teinte de l'image. Blanc = normal
                                   0, // rotation
                                   new Vector2(0, 0), // origine de l'image soit new Vector2(0,0)
                                   new Vector2(1.0f, item.scale), // déformation, normal = 1
                                   effet, // effet de la texture ici symmétrie
                                   0 // profondeur du calque. 0 par défaut.
                                   );
                }
            }
            _spriteBatch.End(); // on termine notre contexte d'affichage

            base.Draw(gameTime);
        }
    }
}