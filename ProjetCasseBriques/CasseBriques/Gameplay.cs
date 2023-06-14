using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        Briques mesBriques;
        Texture2D texture;
        Texture2D[] TextureBriques = new Texture2D[6];
        List<Briques> ListeBriques = new List<Briques>();
        private int[,] Level;

        public bool Stick;
        public bool isKeyboardPressed;
        KeyboardState OldKbState;
        KeyboardState NewKbState;
        protected Point positionSouris;
        protected Point 

        
        public Gameplay(CasseBriques pGame) : base(pGame)
        {
            background = pGame.Content.Load<Texture2D>("BckGameplay");

            // texture de ma raquette
            SprPad = new Raquette(pGame.Content.Load<Texture2D>("pNormal"));
            SprPad.SetPosition(ResolutionEcran.Width / 2 - SprPad.LargeurSprite / 2, ResolutionEcran.Height - SprPad.HauteurSprite);
            // Texture de ma balle

            SprBalle = new Balle(pGame.Content.Load<Texture2D>("bNormal"));
            SprBalle.SetPosition(SprPad.Position.X + SprPad.LargeurSprite / 2 - SprBalle.LargeurSprite / 2,
                                    SprPad.Position.Y - SprBalle.HauteurSprite);

            positionSouris = Mouse.GetState().Position;
            SprBalle.Vitesse = new Vector2(1,1);

            Stick = true;

            SprBriques = new Briques(pGame.Content.Load<Texture2D>("Brique_1"));
            OldKbState = Keyboard.GetState();

            for (int t = 1; t < 6; t++)
            {
                TextureBriques[t] = pGame.Content.Load<Texture2D>("Brique_"+t);
            }

            Level = new int[,]
            {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,3,3,3,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,4,4,4,4,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,2,5,2,1,1,1},
                {1,1,1,1,1,1,1,1,2,5,2,1,1,1},
                {1,1,1,1,1,1,1,1,1,2,2,1,1,1},
              
            };
            int NbColonne = (int)Math.Floor((double)ResolutionEcran.Width / SprBriques.LargeurSprite +1);
            int Nbligne = ResolutionEcran.Height / SprBriques.HauteurSprite;

            for (int l = 0; l < Level.GetLength(0); l++)
            {
                for (int c=0 ; c < Level.GetLength(1); c++)
                {
                    int typeBriques = Level[l,c]; ;
                    if (typeBriques != 0)
                    {
                        texture = TextureBriques[typeBriques];
                        mesBriques = new Briques(texture);

                        switch (typeBriques)
                        {
                            case 1:
                                mesBriques.nbHits = 1;
                                break;
                            case 2:
                                mesBriques.nbHits = 2;
                                break;
                            case 3:
                                mesBriques.nbHits = 3;
                                break;
                            case 4:
                                break;

                                default: break;
                        }
                        mesBriques.SetPosition(c * SprBriques.LargeurSprite , l * SprBriques.HauteurSprite);
                        ListeBriques.Add(mesBriques);
                    } 
                }
            }
        }

        public override void Update()
        {
            NewKbState = Keyboard.GetState();
     

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

            if (mesBriques.BoundingBox.Intersects(SprBalle.NextPositionX()))
            {
                collision = true;
                SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
            }

            if (mesBriques.BoundingBox.Intersects(SprBalle.NextPositionY()))
            {
                collision = true;
                SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
            }

            if (collision)
            {
                    mesBriques.nbHits--;
                    if (mesBriques.nbHits == 0)
                    {
                        ListeBriques.Remove(mesBriques);

                    }
            }
            }
            if (SprPad.BoundingBox.Intersects(SprBalle.NextPositionX()))
            {
                SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                SprBalle.SetPosition(SprBalle.Position.X, SprPad.Position.Y - SprBalle.HauteurSprite);
            }

            if (SprPad.BoundingBox.Intersects(SprBalle.NextPositionY()))
            {
                SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                SprBalle.SetPosition(SprBalle.Position.X, SprPad.Position.Y - SprBalle.HauteurSprite);
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
                Briques.Draw();
            }
            
        }
    }
}
