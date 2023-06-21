using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Gameplay : ScenesManager
    {
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();

        GameState Status = ServiceLocator.GetService<GameState>();
        private Texture2D background;

        Raquette SprPad;
        Balle SprBalle;
        Briques SprBriques;
        HUD HUD; 

        private Vector2 positionGrille;
        private int[,] Levels;

        public bool Stick;

        public bool isKeyboardPressed;
        KeyboardState OldKbState;
        KeyboardState NewKbState;
        private Point Viser;
        private Vector2 Velocity;
        List<Briques> ListeBriques = new List<Briques>();
        List<Personnages> lstPerso = new List<Personnages>();
        GameState gameState;
        Level currentLevel;
        private int currentLevelNB;
        private int currentBackground;
        private int MaxLevel;
        private int currentBackgroundMAX;
        ContentManager _content = ServiceLocator.GetService<ContentManager>();

        public void LoadBackground()
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            background = _content.Load<Texture2D>("BK_" + currentBackground);

        }

        public void InitializeLevel()
        {
            MaxLevel = 4;
            for (int i = 1; i <= MaxLevel; i++) // le nombre de niveau correspond au nombre max de Background (4) que j'ai. Si je met 4, la boucle 
            {
                Level level = new Level(i);
                level.RandomLevel();
                level.Save();
            }
        }
        public Gameplay()
        {
            currentBackground = 1;
            LoadBackground();

            // texture de ma raquette
            SprPad = new Raquette(_content.Load<Texture2D>("pNormal"));
            SprPad.SetPosition(ResolutionEcran.CenterWidth, ResolutionEcran.Height - SprPad.CentreSpriteH);
            // Texture de ma balle

            SprBalle = new Balle(_content.Load<Texture2D>("bFire"));
            SprBalle.SetPosition(SprPad.Position.X, SprPad.Position.Y - SprPad.CentreSpriteH - SprBalle.CentreSpriteH);

            SprBalle.Vitesse = new Vector2(6, -4);
            Stick = true;

            SprBriques = new Briques(_content.Load<Texture2D>("Brique_1"));
            HUD = new HUD(_content.Load<Texture2D>("HUD2"));

            OldKbState = Keyboard.GetState();
            currentLevelNB = 1;
            
            LoadLevel(currentLevelNB);
        }

        public void LoadLevel(int pLevel)
        {
            InitializeLevel();
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            string levelData = File.ReadAllText("Level" + currentLevelNB + ".json");
            currentLevel = JsonSerializer.Deserialize<Level>(levelData);

            int NiveauHauteur = currentLevel.Map.GetLength(0);
            int NiveauLargeur = currentLevel.Map[1].Length;
            int largeurGrille = NiveauLargeur * SprBriques.LargeurSprite;
            int spacing = (ResolutionEcran.Width - largeurGrille) / 2;

            for (int l = 0; l < NiveauHauteur; l++)
            {
                for (int c = 0; c < NiveauLargeur;  c++)
                {
                    int typeBriques = currentLevel.Map[l][c];

                    switch (typeBriques)
                    {
                        case 1:
                            Briques bNormal = new Briques(_content.Load<Texture2D>("Brique_" + typeBriques));
                            bNormal.SetPosition(c * bNormal.LargeurSprite + spacing, l * bNormal.HauteurSprite + bNormal.CentreSpriteH + HUD.HauteurSprite);
                            ListeBriques.Add(bNormal);
                            break;
                        case 2:
                            Briques bGlace = new bGlace(_content.Load<Texture2D>("Brique_" + typeBriques));
                            bGlace.SetPosition(c * bGlace.LargeurSprite  + spacing, l * bGlace.HauteurSprite + bGlace.CentreSpriteH + HUD.HauteurSprite);
                            ListeBriques.Add(bGlace);
                            break;
                        case 3:
                            Briques bFeu = new bFeu(_content.Load<Texture2D>("Brique_" + typeBriques));
                            bFeu.SetPosition(c * bFeu.LargeurSprite  + spacing, l * bFeu.HauteurSprite + bFeu.CentreSpriteH + HUD.HauteurSprite);
                            ListeBriques.Add(bFeu);
                            break;
                        case 4:
                            Briques bMetal = new bMetal(_content.Load<Texture2D>("Brique_" + typeBriques));
                            bMetal.SetPosition(c * SprBriques.LargeurSprite + spacing, l * SprBriques.HauteurSprite);
                            ListeBriques.Add(bMetal);
                            break;
                        case 5:
                            Personnages Glace = new Personnages(_content.Load<Texture2D>("pIce"));
                            Glace.SetPosition(c * SprBriques.LargeurSprite + SprBriques.CentreSpriteL + spacing, l * SprBriques.HauteurSprite + SprBriques.CentreSpriteH);
                            lstPerso.Add(Glace);
                            break;
                        case 6:
                            Personnages Feu = new Personnages(_content.Load<Texture2D>("pFire"));
                            Feu.SetPosition(c * SprBriques.LargeurSprite + SprBriques.CentreSpriteL + spacing, l * SprBriques.HauteurSprite + SprBriques.CentreSpriteH);
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

            SprBalle.Update();

            if (Stick)
            {
                SprBalle.SetPosition(SprPad.Position.X, SprPad.Position.Y - SprPad.CentreSpriteH - SprBalle.CentreSpriteH);
            }

            if (SprPad.BoundingBox.Intersects(SprBalle.BoundingBox))
            {
                SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                SprBalle.SetPosition(SprBalle.Position.X, SprPad.Position.Y - SprBalle.HauteurSprite);
            }

            if (SprBalle.Position.Y > ResolutionEcran.Height)
            {

                HUD.Vie--;
                Stick = true;
                if (HUD.Vie<= 0) 
                {
                    SprBalle.CurrrentBallState = Balle.BallState.Dead;
                    Status.ChangeScene(GameState.Scenes.GameOver);
                }
               
            }

            for (int b = ListeBriques.Count - 1; b >= 0; b--)
             {
                bool collision = false;
                Briques mesBriques = ListeBriques[b];
                mesBriques.Update();

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
                        SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                        //SprBalle.SetPosition(SprBalle.Position.X, mesBriques.Position.Y - mesBriques.HauteurSprite/2 - SprBalle.HauteurSprite);
                    }

                    if (collision && mesBriques.isBreakable == true)
                    {
                        mesBriques.nbHits--;
                        if (mesBriques.nbHits == 0)
                        {
                            if (mesBriques is bFeu Fire)
                            {
                                Fire.rotate = true;
                                Fire.Scalling = true;
                                HUD.GlobalScore += Fire.Points;
                            }
                            else
                            {
                                mesBriques.Scalling = true;
                                HUD.GlobalScore += mesBriques.Points;
                                Trace.WriteLine(mesBriques.scale);
                            }
                        }
                    }
                    if (mesBriques.scale <= 0)
                    {
                        ListeBriques.Remove(mesBriques);
                        Trace.WriteLine(ListeBriques.Count);
                        if (!ListeBriques.Any(brique => brique.isBreakable)) // comme count mais avec de meilleur performance/ Proposé par VisualStudio
                        {
                            // casseBriques.gameState.ChangeScene(GameState.Scenes.Win);
                            currentLevelNB++;
                            currentBackground++;
                            if (currentBackground > MaxLevel)
                            {
                                Status.ChangeScene(GameState.Scenes.Menu);
                                currentLevelNB = 1;
                                currentBackground = 1;
                            }
                            else
                            {
                                Stick = true;
                                LoadBackground();
                                LoadLevel(currentLevelNB);
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

                }
                if (collision == true)
                {
                    mesPerso.currentState = Personnages.State.Moving;
                    mesPerso.Tombe();
                    Trace.WriteLine(mesPerso.currentState);
                }
            }
            base.Update();
        }
        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
            pBatch.Draw(HUD.texture, new Vector2(0,0), Color.White);
            HUD.DrawScore();
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
                               new Vector2(Briques.CentreSpriteL, Briques.CentreSpriteH),
                               Briques.scale,
                               SpriteEffects.None,
                               0);

               pBatch.DrawRectangle(Briques.BoundingBox, Color.Red);
            }

            foreach (var Perso in lstPerso)
            {
                pBatch.Draw(Perso.texture,
                               Perso.Position,
                               null,
                               Color.White,
                               0,
                               new Vector2(Perso.CentreSpriteL, Perso.CentreSpriteH),
                               1.0f,
                               SpriteEffects.None,
                               0);
                pBatch.DrawRectangle(Perso.BoundingBox, Color.Yellow);

            }
        }
    }
}