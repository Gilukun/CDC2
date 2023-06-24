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
using System.IO.Compression;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CasseBriques
{
    public class Gameplay : ScenesManager
    {
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        GameState Status = ServiceLocator.GetService<GameState>();
       
        private Texture2D background;
        Raquette SprPad;
        Balle SprBalle;
        HUD HUD;
        Bonus Vie;
        Bonus Ice;

        List<Bonus> listeBonus = new List<Bonus>();
        LevelManager niveau = new LevelManager();
        public GameState State;

        public bool Stick;

        public bool isKeyboardPressed;
        KeyboardState OldKbState;
        KeyboardState NewKbState;

        private int currentBackground;
        private int currentLevelNB;

        private float winDelay;
        private float winTimer;
        private bool TimerIsOver; 
        public void TimerON()
        {
            winDelay += 0.02f;
            if (winDelay > winTimer)
            {
                TimerIsOver = true;
            }
        }


        public void LoadBackground()
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            background = _content.Load<Texture2D>("BK_" + currentBackground);

        }


        public Gameplay()
        {
            currentLevelNB = 1;
            currentBackground = currentLevelNB;
            LoadBackground();

            // texture de ma raquette
            SprPad = new Raquette(_content.Load<Texture2D>("pNormal"));
            SprPad.SetPosition(ResolutionEcran.CenterWidth, ResolutionEcran.Height - SprPad.CentreSpriteH);
            // Texture de ma balle

            SprBalle = new Balle(_content.Load<Texture2D>("bFire"));
            SprBalle.SetPosition(SprPad.Position.X, SprPad.Position.Y - SprPad.CentreSpriteH - SprBalle.CentreSpriteH);

            SprBalle.Vitesse = new Vector2(6, -4);
            Stick = true;

            HUD = new HUD(_content.Load<Texture2D>("HUD2"));
            OldKbState = Keyboard.GetState();

            niveau.LoadLevel(currentLevelNB);
            winDelay = 0;
            winTimer = 5;
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
                if (HUD.Vie <= 0)
                {
                    SprBalle.CurrentBallState = Balle.BallState.Dead;
                    Status.ChangeScene(GameState.Scenes.GameOver);
                }

            }

            foreach (var Briques in niveau.lstSolidBricks)
            {
                bool collision = false;
                Briques.Update();
                if (Briques.BoundingBox.Intersects(SprBalle.NextPositionX()))
                {
                    CamShake = 30;
                    collision = true;
                    SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                    //SprBalle.SetPosition(mesBriques.Positi on.X + mesBriques.LargeurSprite/2 + SprBalle.LargeurSprite, SprBalle.Position.Y);
                }
                if (Briques.BoundingBox.Intersects(SprBalle.NextPositionY()))
                {
                    CamShake = 30;
                    collision = true;
                    SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                    //SprBalle.SetPosition(SprBalle.Position.X, mesBriques.Position.Y - mesBriques.HauteurSprite/2 - SprBalle.HauteurSprite);
                }
            }

            for (int b = niveau.ListeBriques.Count - 1; b >= 0; b--)
            {
                bool collision = false;
                Briques mesBriques = niveau.ListeBriques[b];
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
                        mesBriques.nbHits --;
                        Trace.WriteLine(mesBriques.nbHits);
                        if (mesBriques.nbHits == 0)
                        {
                            if (mesBriques is BFeu Fire)
                            {
                                Fire.rotate = true;
                                Fire.Scalling = true;
                                HUD.GlobalScore += Fire.Points;
                                Fire.currentState = Briques.State.Broken;

                                Vie = new BonusVie(_content.Load<Texture2D>("bTime"));
                                Vie.SetPositionBonus(Fire.Position.X, Fire.Position.Y);
                                Vie.currentState = Bonus.BonusState.Free;
                                
                                listeBonus.Add(Vie);
                            }
                            if (mesBriques is BGlace Glace)
                            { 
                                Glace.Scalling = true;
                                HUD.GlobalScore += Glace.Points;
                                Glace.currentState = Briques.State.Broken;

                                Ice = new BonusImpact(_content.Load<Texture2D>("bIce"));
                                Ice.SetPositionBonus(Glace.Position.X, Glace.Position.Y);
                                Ice.currentState = Bonus.BonusState.Free;

                                listeBonus.Add(Ice);
                            }
                            else
                            {
                                mesBriques.Scalling = true;
                                HUD.GlobalScore += mesBriques.Points;
                            }
                            
                        }
                    }
                    if (mesBriques.scale <= 0)
                    {
                        niveau.ListeBriques.Remove(mesBriques);

                        if (!niveau.ListeBriques.Any(brique => brique.isBreakable)) // comme count mais avec de meilleur performance/ Proposé par VisualStudio
                        {
                            currentLevelNB++;
                            currentBackground++;
                            if (currentBackground > niveau.LevelMax)
                            {
                                Status.ChangeScene(GameState.Scenes.Menu);
                                currentLevelNB = 1;
                                currentBackground = 1;
                            }
                            else
                            {
                                Stick = true;
                                LoadBackground();
                                niveau.LoadLevel(currentLevelNB);
                                winDelay = 0;
                            }
                        }
                    }
                }       
            }
            for (int p = listeBonus.Count - 1; p >= 0; p--)
            {
                Bonus mesitems = listeBonus[p];
                mesitems.Update();

                if (SprPad.BoundingBox.Intersects(mesitems.NextPositionY()))
                {
                    if (mesitems is BonusVie Vie)
                    {
                        mesitems.currentState = Bonus.BonusState.Catch;
                        listeBonus.RemoveAt(p);
                        HUD.Vie += mesitems.AddBonus;
                    }
                    else if (mesitems is BonusImpact Ice)
                    {
                        mesitems.currentState = Bonus.BonusState.Catch;
                        SprBalle.CurrentBallState = Balle.BallState.Ice;
                        
                        listeBonus.RemoveAt(p);
                        Trace.WriteLine(SprBalle.CurrentBallState);
                    }
                }
            }


                for (int p = niveau.lstPerso.Count - 1; p >= 0; p--)
            {
                bool collision = false;
                Personnages mesPerso = niveau.lstPerso[p];
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
                    if (mesPerso is pFire pFire)
                    {
                        pFire.currentState = Personnages.State.Catch;
                        SprBalle.CurrentBallState = Balle.BallState.SpeedUp;
                        niveau.lstPerso.Remove(pFire);
                    }
                    else if (mesPerso is pIce pIce)
                    {
                        pIce.currentState = Personnages.State.Catch;
                        SprBalle.CurrentBallState = Balle.BallState.SlowDown;
                        niveau.lstPerso.Remove(pIce);
                    }
                }

                if (collision == true)
                {
                    mesPerso.currentState = Personnages.State.Falling;
                    mesPerso.Tombe();
                    if (mesPerso.Position.Y > ResolutionEcran.Height)
                    {
                        niveau.lstPerso.Remove(mesPerso);
                    }
                }


            }
            
            base.Update();
        }

        public override void DrawBackground()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
        }
        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            //pBatch.Draw(background, new Vector2(0, 0), Color.White);
            pBatch.Draw(HUD.texture, new Vector2(0, 0), Color.White);
            HUD.DrawScore();
            SprPad.Draw();
            SprBalle.DrawBall();

            niveau.DrawLevel();

            foreach (var Bonus in listeBonus)
            {
                pBatch.Draw(Bonus.texture,
                            Bonus.Position,
                            null,
                            Color.White,
                            0,
                            new Vector2(Bonus.CentreSpriteL, Bonus.CentreSpriteH),
                            1.0f,
                            SpriteEffects.None,
                            0);
            }

        }
    }
}