using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters;

namespace CasseBriques
{
    internal class SceneGameplay : Scenes
    {
        Texture2D TexRaquette;
        Raquette sprRaquette;
        Balle sprBalle;
        Briques sprBrique;
        Texture2D maBrique;
        bool BalleStick;
        const int NbColonne = 23; // nombre de colonne de mon tableau. const = variable qui ne change jamais.
        const int Nbligne = 10;
        private int[,] Level; // Tableau en deux dimensions
        private List<Briques> LstBriques;

        public SceneGameplay(Game pGame) : base(pGame)
        {
            //Contexte.nbVie = 5; on peut utiliser la classe static directement dans nos méthodes
            // = love.graphics.newImage
            // TexRaquette = pGame.Content.Load<Texture2D>("raquette");


            //sprRaquette.SetPosition (new Vector2(10,10));
            sprRaquette = new Raquette(game.Content.Load<Texture2D>("raquette"), Screen);
            sprRaquette.SetPosition((Screen.Width / 2) - (sprRaquette.Width / 2), Screen.Height - sprRaquette.Height);
            //sprRaquette.Vitesse = new Vector2(1, 0);
            sprBalle = new Balle(pGame.Content.Load<Texture2D>("balle"), Screen);
            sprBalle.SetPosition(sprRaquette.Centre, sprRaquette.Position.Y - sprRaquette.Height);
            sprBalle.Vitesse = new Vector2(5, -3);
            BalleStick = true;

          

            Level = new int[,]
            {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            };

            LstBriques = new List<Briques>();

            Texture2D textBrique; 
            Texture2D[] textBriqueAll = new Texture2D[9];
            for (int t = 1; t < 3; t++)
            {
                textBriqueAll[t] = game.Content.Load<Texture2D>("Perso"+t);
            }


            for (int l = 0; l < Level.GetLength(0); l++) // getlength 0 c'est pous la dimension ligne. Pour la dimension colonne il faut getlength 1
            {
               
                for (int c = 0; c < Level.GetLength(1); c++)
                {
                    int typeDeBrique = Level[l, c];
                    if (typeDeBrique != 0)
                    {
                        textBrique = textBriqueAll[typeDeBrique];
                        Briques maBrique = new Briques(textBrique, Screen);
                        switch (typeDeBrique) 
                        { case 1:
                                maBrique.NbCoups = 2;
                                break;
                        
                        
                        
                        }
                       
                        maBrique.SetPosition(c * textBrique.Width, l * textBrique.Height);
                        LstBriques.Add(maBrique);
                    }

                }
            }

        }

        public override void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                BalleStick = false;
            }

            sprRaquette.Update();

            for (int b = LstBriques.Count - 1; b > 0; b--)
            {
                bool Collision = false;
                Briques mesbriques = LstBriques[b];
                mesbriques.Update();

                if (mesbriques.falling == false)

                {
                    if (mesbriques.BoundingBox.Intersects(sprBalle.NextPositionX()))
                    {
                        sprBalle.InverseVitesseX();
                        Collision = true;
                    }

                    if (mesbriques.BoundingBox.Intersects(sprBalle.NextPositionY()))
                    {
                        sprBalle.InverseVitesseY();
                        Collision = true;
                    }
                    if (Collision)
                    {
                        CamShake = 30;
                        
                        mesbriques.NbCoups--;
                        if (mesbriques.NbCoups==0)
                        {
                            mesbriques.Tombe();

                        }
                    }

                }
                if (mesbriques.Position.Y > Screen.Height)
                {
                    
                    LstBriques.Remove(mesbriques); 
                }
            }

            sprBalle.Update();

            if (BalleStick)
            {
                sprBalle.SetPosition(sprRaquette.Centre - sprBalle.HWidth,
                                     sprRaquette.Position.Y - sprRaquette.Height);

            }

            if (sprRaquette.BoundingBox.Intersects(sprBalle.NextPositionX()))
            {
                sprBalle.InverseVitesseX();
                sprBalle.SetPosition(sprBalle.Position.X, sprBalle.Position.Y - sprBalle.Height);
            }

            if (sprRaquette.BoundingBox.Intersects(sprBalle.NextPositionY()))
            {
                sprBalle.InverseVitesseY();
                sprBalle.SetPosition(sprBalle.Position.X, sprBalle.Position.Y - sprBalle.Height);
            }
            
            if (sprBalle.Position.Y > Screen.Height)
            {
                BalleStick = true;
            }
        }
       

        public  override void DrawScene(SpriteBatch pBatch)
        {
            sprRaquette.Draw(pBatch); 
            sprBalle.Draw(pBatch);
         
            foreach (var Briques in LstBriques)
            {
                Briques.Draw(pBatch);
            }
        }

    }
}
