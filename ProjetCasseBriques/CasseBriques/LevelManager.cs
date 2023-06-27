using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Drawing;
using System.IO;
using System.Net.Security;
//using System.Numerics;
using System.Text.Json;

namespace CasseBriques
{
    public class LevelManager
    {
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        
        public int numero { get; set; } 
        public int[][] Map { get; set; } 
        public int LevelMax;
        
        HUD HUD;
        LevelManager currentLevel;
        Briques SprBriques;
        public Briques bNormal;
        pIce BriqueMan;
        pFire BriqueMan2;
        pIce BriqueMan3;
        pFire BriqueMan4;
       
        public List<Briques> ListeBriques { get; private set; }
        public List<Personnages> lstPerso { get; private set; }
        public List<Personnages> lstPerso2 { get; private set; }
        public List<Briques> lstSolidBricks { get; private set; }

        public LevelManager() 
        {

        } 
        public LevelManager(int pNumero)
        {
            numero = pNumero;
            LevelMax = 4;

        }
        public void RandomLevel()
        {
            int colNb = 10;
            int linNb = 4;
            Random rnd = new Random();
            Map = new int[linNb][]; 
            for (int l = 0; l < linNb; l++)
            {
                Map[l] = new int[colNb];
                for (int c = 0; c < colNb; c++)
                {
                    Map[l][c] = rnd.Next(1, 4);
                }
            }
        }

        public void InitializeLevel()
        {
            LevelMax = 4;
            for (int i = 1; i <= LevelMax; i++)
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
            lstPerso = new List<Personnages>();
            lstPerso2 = new List<Personnages>();
            lstSolidBricks = new List<Briques>();
          
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            string levelData = File.ReadAllText("level" + pLevel + ".json");
            currentLevel = JsonSerializer.Deserialize<LevelManager>(levelData);

            SprBriques = new Briques(_content.Load<Texture2D>("Bricks\\Brique_1"));
            HUD = new HUD(_content.Load<Texture2D>("HUD2"));

            int NiveauHauteur = currentLevel.Map.GetLength(0);
            int NiveauLargeur = currentLevel.Map[1].Length;
            int largeurGrille = NiveauLargeur * SprBriques.LargeurSprite;
            int hauteurGrille = NiveauHauteur  * SprBriques.HauteurSprite;
            int spacing = (ResolutionEcran.Width - largeurGrille) / 2;

            for (int l = 0; l < NiveauHauteur; l++)
            {
                for (int c = 0; c < NiveauLargeur; c++)
                {
                    int typeBriques = currentLevel.Map[l][c];

                    switch (typeBriques)
                    {
                        case 1:
                            Briques bNormal = new BBase(_content.Load<Texture2D>("Bricks\\Brique_" + typeBriques));
                            bNormal.SetPosition(c * bNormal.LargeurSprite + spacing, l * bNormal.HauteurSprite + bNormal.CentreSpriteH + HUD.HauteurSprite);
                            ListeBriques.Add(bNormal);
                            break;
                        case 2:
                            Briques bGlace = new BGlace(_content.Load<Texture2D>("Bricks\\Brique_" + typeBriques));
                            bGlace.SetPosition(c * bGlace.LargeurSprite + spacing, l * bGlace.HauteurSprite + bGlace.CentreSpriteH + HUD.HauteurSprite);
                            ListeBriques.Add(bGlace);
                            break;

                        case 3:
                            Briques bFeu = new BFeu(_content.Load<Texture2D>("Bricks\\Brique_" + typeBriques));
                            bFeu.SetPosition(c * bFeu.LargeurSprite + spacing, l * bFeu.HauteurSprite + bFeu.CentreSpriteH + HUD.HauteurSprite);
                            ListeBriques.Add(bFeu);
                            
                            break;

                        default:
                            break;
                    }
                }
            }

            if (pLevel == 2)
            {
             
                BriqueMan3 = new pIce(_content.Load<Texture2D>("bIce"));
                lstPerso.Add(BriqueMan3);
                BriqueMan4 = new pFire(_content.Load<Texture2D>("bTime"));
                lstPerso.Add(BriqueMan4);

                int spacingX = ResolutionEcran.Width / 2;
                int firstBrickX = ResolutionEcran.Width / 4;
                int spacingGrille = 200;
                for (int i=1; i < 5; i++)
                { 
                    Briques bMetal= new BMetal(_content.Load<Texture2D>("Brique_4"));
                    int brickX = firstBrickX + (i - 1) * spacingX;
                    int brickY = hauteurGrille + spacingGrille;
                    bMetal.SetPosition(brickX, brickY);
                    lstSolidBricks.Add(bMetal);
                }
            }
            else
            {
                BriqueMan = new pIce(_content.Load<Texture2D>("pIce"));
                lstPerso.Add(BriqueMan);
                BriqueMan2 = new pFire(_content.Load<Texture2D>("pFire"));
                lstPerso.Add(BriqueMan2);
            }  
        }
       

        public void DrawLevel()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            AssetsManager font = ServiceLocator.GetService<AssetsManager>();

            float rotation;
            foreach (var Briques in ListeBriques)
            {
                if (Briques is BFeu Fire)
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
               
                //pBatch.DrawRectangle(Briques.BoundingBox, Color.Red);
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
                  //pBatch.DrawRectangle(Perso.BoundingBox, Color.Yellow);
                }
            }
            foreach (var Briques in lstSolidBricks)
            {
           
                    pBatch.Draw(Briques.texture,
                                    Briques.Position,
                                    null,
                                    Color.White,
                                    0,
                                    new Vector2(Briques.CentreSpriteL, Briques.CentreSpriteH),
                                    1.0f,
                                    SpriteEffects.None,
                                    0);
                   // pBatch.DrawRectangle(Briques.BoundingBox, Color.Yellow);
            }
        }
    
    }
}

