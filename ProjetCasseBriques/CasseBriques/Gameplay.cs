using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
       
       
        private Vector2 positionGrille;
        private int[,] Level;
      

        public bool Stick;
        public bool isKeyboardPressed;
        KeyboardState OldKbState;
        KeyboardState NewKbState;
        private Point Viser;
        private Vector2 Velocity;
        List<Briques> ListeBriques = new List<Briques>();

        public Gameplay(CasseBriques pGame) : base(pGame)
        {
            background = pGame.Content.Load<Texture2D>("Bk_1");

            // texture de ma raquette
            SprPad = new Raquette(pGame.Content.Load<Texture2D>("pNormal"));
            SprPad.SetPosition(ResolutionEcran.Width / 2 - SprPad.LargeurSprite / 2, ResolutionEcran.Height - SprPad.HauteurSprite);
            // Texture de ma balle

            SprBalle = new Balle(pGame.Content.Load<Texture2D>("bFire"));
            SprBalle.SetPosition(SprPad.Position.X + SprPad.LargeurSprite / 2 - SprBalle.LargeurSprite / 2,
                                    SprPad.Position.Y - SprBalle.HauteurSprite);
            SprBalle.Vitesse = new Vector2(6, 7);
            Stick = true;
            
            SprBriques = new Briques(pGame.Content.Load<Texture2D>("Brique_1"));
            OldKbState = Keyboard.GetState();



            Level = new int[,]
            {
                {1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,3,3,3,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,4,4,4,4,0,1,1,1,1,1},
                {1,1,1,2,5,2,1,0,2,2,2,1,1},
                {1,1,1,3,3,3,1,0,2,5,2,1,1},
                {1,1,1,1,1,1,1,0,1,2,2,1,1},


            };
            
            int largeurGrille = Level.GetLength(1) * SprBriques.LargeurSprite;
            int spacing = (ResolutionEcran.Width - largeurGrille)/2;

            Texture2D textBriques;
            Texture2D[] TextureBriques = new Texture2D[6];
            
            for (int t = 1; t < 6; t++)
            {
                TextureBriques[t] = pGame.Content.Load<Texture2D>("Brique_" + t);
            }
           
            for (int l = 0; l < Level.GetLength(0); l++)
            {
                for (int c = 0; c < Level.GetLength(1); c++)
                {
                    int typeBriques = Level[l, c]; ;
                    if (typeBriques != 0)
                    {
                        textBriques = TextureBriques[typeBriques];
                        Briques lesBriques = new Briques(textBriques);
                        
                        switch (typeBriques)
                        {
                            case 1:
                                lesBriques.nbHits = 1;
                                break;
                            case 2:
                                lesBriques.nbHits = 2;
                                break;
                            case 3:
                                lesBriques.nbHits = 3;
                                break;
                            case 4:
                                break;

                            default: break;
                        }
                        lesBriques.SetPosition(c * SprBriques.LargeurSprite + SprBriques.LargeurSprite/2 + spacing, l * SprBriques.HauteurSprite + SprBriques.HauteurSprite/2);
                        ListeBriques.Add(lesBriques);
                       
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

            SprBalle.Update();

            if (Stick)
            {
                SprBalle.SetPosition(SprPad.Position.X + SprPad.LargeurSprite / 2 - SprBalle.LargeurSprite/2, SprPad.Position.Y - SprBalle.HauteurSprite);
            }

            if (SprBalle.Position.Y > ResolutionEcran.Height)
            {
                Stick = true;
            }

            for (int b= ListeBriques.Count - 1; b >= 0; b--) 
            {
                bool collision = false;
                Briques mesBriques = ListeBriques[b];
                mesBriques.Update();

                if (mesBriques.IsScalling == false)
                {
                    if (mesBriques.BoundingBox.Intersects(SprBalle.NextPositionX()))
                    {
                        SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                        //SprBalle.SetPosition(SprBalle.Position.X - SprBalle.LargeurSprite, SprBalle.Position.Y);
                        collision = true;
                    }

                    if (mesBriques.BoundingBox.Intersects(SprBalle.NextPositionY()))
                    {
                        SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                        //SprBalle.SetPosition(SprBalle.Position.X, SprBalle.Position.Y - SprBalle.HauteurSprite);
                        collision = true;
                    }

                    if (collision)
                    {
                        mesBriques.nbHits--;
                        if (mesBriques.nbHits == 0)
                        {
                            mesBriques.JeScale();
                            mesBriques.JeTourne();
                            if (mesBriques.scale <= 0)
                                ListeBriques.RemoveAt(b);

                        }
                    }
                    
                }
            }
            if (SprPad.BoundingBox.Intersects(SprBalle.NextPositionX()))
            {
                SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                //SprBalle.SetPosition(SprBalle.Position.X - SprBalle.LargeurSprite, SprBalle.Position.Y);
            }

            if (SprPad.BoundingBox.Intersects(SprBalle.NextPositionY()))
            {
                SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                //SprBalle.SetPosition(SprBalle.Position.X, SprBalle.Position.Y - SprBalle.HauteurSprite);
            }

            base.Update();
        }
        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
            SprPad.Draw();
            SprBalle.Draw();

            foreach (var Briques in ListeBriques)
            {
                pBatch.Draw(Briques.texture,
                            Briques.Position,
                            null,
                            Color.White,
                            Briques.rotation,
                            new Vector2(Briques.LargeurSprite/2,Briques.HauteurSprite/2),
                            Briques.scale,
                            SpriteEffects.None,
                            0);
            }

        }
    }
}
