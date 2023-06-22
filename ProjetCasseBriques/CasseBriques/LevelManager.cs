using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CasseBriques
{
    public class LevelManager
    {
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        public int numero { get; set; } // pour que Json fonction il faut sérialiser, donc il faut absolumnt mettre get;set.
        public int[][] Map { get; set; } // on créer une liste avec 2 champs
        public int LevelMax;
        public LevelManager() { } // constructeur par défaut pour pourvoir désérialiser le fichier. Il faut donc qu'il soit vide car il doit faire en background un new sans paramètre
        public LevelManager(int pNumero)
        {
            numero = pNumero;
            LevelMax = 4;
        }

        HUD HUD;
        GameState CurrentScene;
        LevelManager currentLevel;
        Briques SprBriques;
        pIce BriqueMan;
        pFire BriqueMan2;
        public List<Briques> ListeBriques { get; private set; }
        public List<Personnages> lstPerso { get; private set; }

        private int currentLevelNB;


        public void RandomLevel()
        {
            Random rnd = new Random();
            Map = new int[3][]; // on créer en nouveau tableau de 10 
            for (int l = 0; l < 3; l++)
            {
                Map[l] = new int[5]; // on lui dit qu'il a 10 ligne
                for (int c = 0; c < 5; c++)
                {
                    Map[l][c] = rnd.Next(1, 2); // dans chaque case on met un rnd 
                }
            }
        }


        public void InitializeLevel()
        {
            LevelMax = 4;
            for (int i = 1; i <= LevelMax; i++) // le nombre de niveau correspond au nombre max de Background (4) que j'ai. Si je met 4, la boucle 
            {
                LevelManager level = new LevelManager(i);
                level.RandomLevel();
                level.Save();
            }
        }


        public void Save()
        {
            string jsonLevel = JsonSerializer.Serialize(this); // on créer le fichier JSON
            File.WriteAllText("level" + numero + ".json", jsonLevel); // on l'exporte en fichier .Json
        }
        public void LoadLevel(int pLevel)
        {
            InitializeLevel();
            ListeBriques = new List<Briques>();
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            string levelData = File.ReadAllText("level" + pLevel + ".json");
            currentLevel = JsonSerializer.Deserialize<LevelManager>(levelData);

            SprBriques = new Briques(_content.Load<Texture2D>("Brique_1"));
            HUD = new HUD(_content.Load<Texture2D>("HUD2"));

            int NiveauHauteur = currentLevel.Map.GetLength(0);
            int NiveauLargeur = currentLevel.Map[1].Length;
            int largeurGrille = NiveauLargeur * SprBriques.LargeurSprite;
            int spacing = (ResolutionEcran.Width - largeurGrille) / 2;

            for (int l = 0; l < NiveauHauteur; l++)
            {
                for (int c = 0; c < NiveauLargeur; c++)
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
                            bGlace.SetPosition(c * bGlace.LargeurSprite + spacing, l * bGlace.HauteurSprite + bGlace.CentreSpriteH + HUD.HauteurSprite);
                            ListeBriques.Add(bGlace);
                            break;
                        case 3:
                            Briques bFeu = new bFeu(_content.Load<Texture2D>("Brique_" + typeBriques));
                            bFeu.SetPosition(c * bFeu.LargeurSprite + spacing, l * bFeu.HauteurSprite + bFeu.CentreSpriteH + HUD.HauteurSprite);
                            ListeBriques.Add(bFeu);
                            break;
                        case 4:
                            Briques bMetal = new bMetal(_content.Load<Texture2D>("Brique_" + typeBriques));
                            bMetal.SetPosition(c * SprBriques.LargeurSprite + spacing, l * SprBriques.HauteurSprite);
                            ListeBriques.Add(bMetal);
                            break;
                        default:
                            break;
                    }
                }
            }
            lstPerso = new List<Personnages>();
            BriqueMan = new pIce(_content.Load<Texture2D>("pIce"));
            lstPerso.Add(BriqueMan);
            BriqueMan2 = new pFire(_content.Load<Texture2D>("pFire"));
            lstPerso.Add(BriqueMan2);
        }

        public void DrawLevel()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();

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
                if (Perso.currentState != Personnages.State.Idle)
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
                    // pBatch.DrawRectangle(Perso.BoundingBox, Color.Yellow);
                }

            }
        }
    }
}

