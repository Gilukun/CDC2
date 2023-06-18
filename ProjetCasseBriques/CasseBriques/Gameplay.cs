using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Gameplay : ScenesManager
    {
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        private Texture2D background;
        
        Raquette SprPad;
        Balle SprBalle;
        Briques SprBriques;
        Briques SprBFeu;
       
       
        private Vector2 positionGrille;
        private int[,] Level;
      

        public bool Stick;
        public bool isKeyboardPressed;
        KeyboardState OldKbState;
        KeyboardState NewKbState;
        private Point Viser;
        private Vector2 Velocity;
        List<Briques> ListeBriques = new List<Briques>();
        List<Personnages> lstPerso =  new List<Personnages> (); 
        //List<Briques> ListSpriteBriques = new List<Briques>();

        public Gameplay(CasseBriques pGame) : base(pGame)
        {
            background = pGame.Content.Load<Texture2D>("Bk_1");

            // texture de ma raquette
            SprPad = new Raquette(pGame.Content.Load<Texture2D>("pNormal"));
            SprPad.SetPosition(ResolutionEcran.Width / 2 - SprPad.LargeurSprite / 2, ResolutionEcran.Height - SprPad.HauteurSprite);
            // Texture de ma balle

            SprBalle = new Balle(pGame.Content.Load<Texture2D>("bFire"));

            SprBalle.Vitesse = new Vector2(6, -4);
            Stick = true;
            
            SprBriques = new Briques(pGame.Content.Load<Texture2D>("Brique_1"));

            OldKbState = Keyboard.GetState();

            Level = new int[,]
            {
                {0,0,1,1,1,1,1,0,0,},
                {1,1,1,1,1,1,1,1,1,},
                {1,1,1,1,1,1,1,1,1,},
                {1,2,2,2,4,3,3,3,1,},
                {1,2,5,2,4,3,6,3,1,},
                {1,2,2,2,4,3,3,3,1,},
                {0,0,1,1,1,1,1,0,0,},
                {0,5,6,5,6,5,6,6,5,},

            };
            
            int largeurGrille = Level.GetLength(1) * SprBriques.LargeurSprite;
            int spacing = (ResolutionEcran.Width - largeurGrille)/2;
           
            for (int l = 0; l < Level.GetLength(0); l++)
            {
                for (int c = 0; c < Level.GetLength(1); c++)
                {
                    int typeBriques = Level[l, c]; ;
                  
                    switch(typeBriques)
                    {
                        case 1:
                            Briques bNormal = new Briques(pGame.Content.Load<Texture2D>("Brique_" + typeBriques));
                            bNormal.SetPosition(c * SprBriques.LargeurSprite + SprBriques.LargeurSprite / 2 + spacing, l * SprBriques.HauteurSprite + SprBriques.HauteurSprite / 2);
                            ListeBriques.Add(bNormal); 
                            break;
                        case 2:
                            Briques bGlace = new bGlace(pGame.Content.Load<Texture2D>("Brique_" + typeBriques));
                            bGlace.SetPosition(c * SprBriques.LargeurSprite + SprBriques.LargeurSprite / 2 + spacing, l * SprBriques.HauteurSprite + SprBriques.HauteurSprite / 2);
                            ListeBriques.Add(bGlace);
                            break;
                        case 3:
                            Briques bFeu = new bFeu(pGame.Content.Load<Texture2D>("Brique_" + typeBriques));
                            bFeu.SetPosition(c * SprBriques.LargeurSprite + SprBriques.LargeurSprite / 2 + spacing, l * SprBriques.HauteurSprite + SprBriques.HauteurSprite / 2);
                            ListeBriques.Add(bFeu);
                            break;
                        case 4:
                            Briques bMetal = new bMetal(pGame.Content.Load<Texture2D>("Brique_" + typeBriques));
                            bMetal.SetPosition(c * SprBriques.LargeurSprite + SprBriques.LargeurSprite / 2 + spacing, l * SprBriques.HauteurSprite + SprBriques.HauteurSprite / 2);
                            ListeBriques.Add(bMetal);
                            break;
                        case 5:
                            Personnages Glace = new Personnages(pGame.Content.Load<Texture2D>("pIce"));
                            Glace.SetPosition(c * SprBriques.LargeurSprite + SprBriques.LargeurSprite / 2 + spacing, l * SprBriques.HauteurSprite + SprBriques.HauteurSprite / 2);
                            lstPerso.Add(Glace);
                            break;
                        case 6:
                            Personnages Feu = new Personnages(pGame.Content.Load<Texture2D>("pFire"));
                            Feu.SetPosition(c * SprBriques.LargeurSprite + SprBriques.LargeurSprite / 2 + spacing, l * SprBriques.HauteurSprite + SprBriques.HauteurSprite / 2);
                            lstPerso.Add(Feu); 
                            break;   
                        default:
                            break;
                            
                    }
                }
            }
        }
        public override void Update()
        {
            NewKbState = Keyboard.GetState();
            SprPad.Update();

            if (NewKbState.IsKeyDown(Keys.Space) && !OldKbState.IsKeyDown(Keys.Space))
            {
                Stick = false;       
            }
            OldKbState = NewKbState;

            for (int b= ListeBriques.Count - 1; b >= 0; b--) 
            {
                bool collision = false;
                Briques mesBriques = ListeBriques[b];
                mesBriques.Update();
                mesBriques.BoundingBox = new Rectangle((int)(mesBriques.Position.X-mesBriques.LargeurSprite/2), 
                                                       (int)(mesBriques.Position.Y-mesBriques.HauteurSprite/2),
                                                       mesBriques.LargeurSprite,  
                                                       mesBriques.HauteurSprite);

                if (mesBriques.isScalling == false)
                {
                    if (mesBriques.BoundingBox.Intersects(SprBalle.NextPositionX()))
                    {
                        collision = true;
                        SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                        //SprBalle.SetPosition(mesBriques.Positi on.X + mesBriques.LargeurSprite/2 + SprBalle.LargeurSprite, SprBalle.Position.Y);
                    }

                    if (mesBriques.BoundingBox.Intersects(SprBalle.NextPositionY()))
                    {
                        collision = true;
                        SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X,-SprBalle.Vitesse.Y);
                        //SprBalle.SetPosition(SprBalle.Position.X, mesBriques.Position.Y - mesBriques.HauteurSprite/2 - SprBalle.HauteurSprite);
                    }

                    if (collision && mesBriques.isBreakable == true )
                    {
                        mesBriques.nbHits--;
                        if (mesBriques.nbHits == 0)
                        {
                            if (mesBriques is bFeu Fire)
                            {
                                Fire.rotate = true;
                                Fire.Scalling = true;
 
                            }
                            else
                            {
                                mesBriques.Scalling = true;
                            }

                            if (mesBriques.scale <= 0)
                            { 
                                ListeBriques.Remove(mesBriques);
                                
                            }
                        }
                    }
                    
                }
            }

            for (int p = lstPerso.Count - 1; p >= 0; p--)
            {
                bool collision = false;
                Personnages mesPerso = lstPerso[p];
                mesPerso.Update();

                mesPerso.BoundingBox = new Rectangle((int)(mesPerso.Position.X - mesPerso.LargeurSprite / 2),
                                                      (int)(mesPerso.Position.Y - mesPerso.HauteurSprite / 2),
                                                      mesPerso.LargeurSprite,
                                                      mesPerso.HauteurSprite);

                if (mesPerso.BoundingBox.Intersects(SprBalle.NextPositionX()))
                {
                    collision = true;
                    SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                    //SprBalle.SetPosition(mesBriques.Positi on.X + mesBriques.LargeurSprite/2 + SprBalle.LargeurSprite, SprBalle.Position.Y);
                }

                if (mesPerso.BoundingBox.Intersects(SprBalle.NextPositionY()))
                {
                    collision = true;
                    SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                    //SprBalle.SetPosition(SprBalle.Position.X, mesBriques.Position.Y - mesBriques.HauteurSprite/2 - SprBalle.HauteurSprite);
                }
                if (SprPad.BoundingBox.Intersects(mesPerso.NextPositionY()))
                {
                    collision = true;
                    mesPerso.currentState = Personnages.State.Catch;
                    lstPerso.Remove(mesPerso);
                    Trace.WriteLine(mesPerso.currentState);
                    Trace.WriteLine(lstPerso.Count);
                }
                if (collision == true)
                {
                    mesPerso.currentState = Personnages.State.Moving;
                    mesPerso.Tombe();
                    Trace.WriteLine(mesPerso.currentState);
                }
            }
            SprBalle.Update();

            if (Stick)
            {
                SprBalle.SetPosition(SprPad.Position.X + SprPad.LargeurSprite / 2 - SprBalle.LargeurSprite / 2,
                                    SprPad.Position.Y - SprBalle.HauteurSprite);
            }

            if (SprPad.BoundingBox.Intersects(SprBalle.BoundingBox))
            {
                SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                SprBalle.SetPosition(SprBalle.Position.X, SprPad.Position.Y - SprBalle.HauteurSprite);
            }

            if (SprBalle.Position.Y > ResolutionEcran.Height)
            {
                Stick = true;
            }

            base.Update();
        }
        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
            SprPad.Draw();
            SprBalle.Draw();

            float rotation;
            foreach (var Briques in ListeBriques)
            {

                if (Briques is bFeu Fire)
                {
                   rotation = Fire.Rotation;
                }
                else
                {
                    rotation = 0;

                }
                pBatch.Draw(Briques.texture,
                               Briques.Position,
                               null,
                               Color.White,
                               rotation,
                               new Vector2(Briques.LargeurSprite / 2, Briques.HauteurSprite / 2),
                               Briques.scale,
                               SpriteEffects.None,
                               0);

                //pBatch.DrawRectangle(Briques.BoundingBox, Color.Red);
            }

            foreach (var Perso in lstPerso)
            {
                pBatch.Draw(Perso.texture,
                               Perso.Position,
                               null,
                               Color.White,
                               0,
                               new Vector2(Perso.LargeurSprite / 2, Perso.HauteurSprite / 2),
                               1.0f,
                               SpriteEffects.None,
                               0);
                pBatch.DrawRectangle(Perso.BoundingBox, Color.Yellow);

            }
        }
    }
}
