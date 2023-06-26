using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;

namespace CasseBriques
{
    public class Gameplay : ScenesManager
    {
        ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        GameState Status = ServiceLocator.GetService<GameState>();
        AssetsManager font = ServiceLocator.GetService<AssetsManager>();

        AssetsManager audio = ServiceLocator.GetService<AssetsManager>();
        HUD hud = ServiceLocator.GetService<HUD>();

        SoundEffectInstance padImpact;
        SoundEffectInstance catchSound;

        private Texture2D background;
        Raquette SprPad;
        Balle SprBalle;
        PopUp pop;
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

        private float Delay;
        private float Timer;
        private bool TimerIsOver; 
        public void TimerON(float pIncrement)
        {
            Delay += pIncrement;
            if (Delay > Timer)
            {
                TimerIsOver = true;
            }
        }

        public void LoadBackground()
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            background = _content.Load<Texture2D>("Backgrounds\\Back_" + currentBackground);
        }

        public Gameplay()
        {
            currentLevelNB = 1;
            currentBackground = currentLevelNB;
            LoadBackground();

            //Soundtracks
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(audio.InGame);
            
            // texture de ma raquette
            SprPad = new Raquette(_content.Load<Texture2D>("Pad_1"));
            SprPad.SetPosition(ResolutionEcran.CenterWidth, ResolutionEcran.Height - SprPad.CentreSpriteH);
            // Texture de ma balle

            SprBalle = new Balle(_content.Load<Texture2D>("bFire"));
            SprBalle.SetPosition(SprPad.Position.X, SprPad.Position.Y - SprPad.CentreSpriteH - SprBalle.CentreSpriteH);

            SprBalle.Vitesse = new Vector2(6, -4);
            Stick = true;

            hud.texture = _content.Load<Texture2D>("HUD2");
            

            OldKbState = Keyboard.GetState();

            niveau.LoadLevel(currentLevelNB);
            Delay = 0;
            Timer = 5;
        }

        public void Rebounds()
        { 
        
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

            // La balle reste collée à la raquette
            if (Stick)
            {
                SprBalle.SetPosition(SprPad.Position.X, SprPad.Position.Y - SprPad.CentreSpriteH - SprBalle.CentreSpriteH);
            }

            // Rebond de la balle sur la raquette
            if (SprPad.BoundingBox.Intersects(SprBalle.BoundingBox))
            {
                AssetsManager.PlaySFX(audio.PadRebound);
                SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                SprBalle.SetPosition(SprBalle.Position.X, SprPad.Position.Y - SprBalle.HauteurSprite);   
            }

            // Si la balle sort de l'écran
            if (SprBalle.Position.Y > ResolutionEcran.Height)
            {
                hud.Vie--;
                Stick = true;
                if (hud.Vie <= 0)
                {
                    SprBalle.CurrentBallState = Balle.BallState.Dead;
                    Status.ChangeScene(GameState.Scenes.GameOver);
                }
            }

            // Collissions avec les briques "incassables"
            foreach (var Briques in niveau.lstSolidBricks)
            {
                bool collision = false;
                Briques.Update();
                if (Briques.BoundingBox.Intersects(SprBalle.NextPositionX()))
                {
                    CamShake = 30;
                    collision = true;
                    AssetsManager.PlaySFX(audio.hitBricks);
                    SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                   // SprBalle.SetPosition(Briques.Position.X + Briques.LargeurSprite/2, SprBalle.Position.Y);
                }
                if (Briques.BoundingBox.Intersects(SprBalle.NextPositionY()))
                {
                    CamShake = 30;
                    collision = true;
                    AssetsManager.PlaySFX(audio.hitBricks);
                    SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                    //SprBalle.SetPosition(SprBalle.Position.X, Briques.Position.Y - Briques.HauteurSprite/2 - SprBalle.HauteurSprite);
                }
            }
            
            // Collision avec les briques 
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
                        AssetsManager.PlaySFX(audio.hitBricks);
                        SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                        //SprBalle.SetPosition(mesBriques.Position.X + mesBriques.LargeurSprite/2, SprBalle.Position.Y);
                    }

                    if (mesBriques.BoundingBox.Intersects(SprBalle.NextPositionY()))
                    {
                        collision = true;
                        AssetsManager.PlaySFX(audio.hitBricks);
                        SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, - SprBalle.Vitesse.Y);
                        //SprBalle.SetPosition(SprBalle.Position.X, mesBriques.Position.Y - mesBriques.HauteurSprite/2 - SprBalle.HauteurSprite);
                    }
                    if (collision && mesBriques.isBreakable == true)
                    {
                        mesBriques.nbHits -= SprBalle.Impact;
                        
                        if (mesBriques.nbHits <= 0)
                        {
                            if (mesBriques is BFeu Fire)
                            {
                                Fire.rotate = true;
                                Fire.Scalling = true;
                                hud.GlobalScore += Fire.Points;
                                Fire.currentState = Briques.State.Broken;

                                Vie = new BonusVie(_content.Load<Texture2D>("bTime"));
                                Vie.SetPositionBonus(Fire.Position.X, Fire.Position.Y);
                                Vie.currentState = Bonus.BonusState.Free;

                                pop = new PopUp();
                                pop.SetPosition(Fire.Position.X, Fire.Position.Y);

                                listeBonus.Add(Vie);
                            }
                            if (mesBriques is BGlace Glace)
                            { 
                                Glace.Scalling = true;
                                hud.GlobalScore += Glace.Points;
                                Glace.currentState = Briques.State.Broken;

                                Ice = new BonusImpact(_content.Load<Texture2D>("bIce"));
                                Ice.SetPositionBonus(Glace.Position.X, Glace.Position.Y);
                                Ice.currentState = Bonus.BonusState.Free;

                                pop = new PopUp();
                                pop.SetPosition(Glace.Position.X, Glace.Position.Y);

                                listeBonus.Add(Ice);
                            }
                            else
                            {
                                mesBriques.Scalling = true;
                                pop = new PopUp();
                                pop.SetPosition(mesBriques.Position.X, mesBriques.Position.Y);
                                hud.GlobalScore += mesBriques.Points;
                            } 
                        }
                    }
                    if (mesBriques.scale <= 0)
                    {
                        niveau.ListeBriques.Remove(mesBriques);

                    }
                }       
            }

            // Chargement du niveau suivant
            if (!niveau.ListeBriques.Any(brique => brique.isBreakable)) // comme count mais avec de meilleur performance/ Proposé par VisualStudio
            {
                niveau.lstPerso.Clear();
                SprBalle.CurrentBallState = Balle.BallState.Reset;
                listeBonus.Clear();
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
                }
            }

            // Collision et récupération des bonus
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
                        hud.Vie += mesitems.AddBonus;
                        ResolutionEcran.currentState = ScreenManager.State.Narrow;
                        AssetsManager.PlaySFX(audio.CatchLife);
                    }
                    else if (mesitems is BonusImpact Ice)
                    {
                        mesitems.currentState = Bonus.BonusState.Catch;
                        SprBalle.CurrentBallState = Balle.BallState.Ice;
                        AssetsManager.PlaySFX(audio.CatchLife);
                        ResolutionEcran.currentState = ScreenManager.State.Wide;
                        listeBonus.RemoveAt(p); 
                    }
                }
            }

            // Collision avec les personnages
            for (int p = niveau.lstPerso.Count - 1; p >= 0; p--)
            {
                bool collision = false;
                Personnages mesPerso = niveau.lstPerso[p];
                mesPerso.Update();

                if (mesPerso.BoundingBox.Intersects(SprBalle.NextPositionX()))
                {
                    collision = true;
                    AssetsManager.PlaySFX(audio.hitBricks);
                    SprBalle.Vitesse = new Vector2(-SprBalle.Vitesse.X, SprBalle.Vitesse.Y);
                    //SprBalle.SetPosition(mesPerso.Position.X + mesPerso.LargeurSprite/2 + SprBalle.LargeurSprite, SprBalle.Position.Y);
                }

                if (mesPerso.BoundingBox.Intersects(SprBalle.NextPositionY()))
                {
                    collision = true;
                    AssetsManager.PlaySFX(audio.hitBricks);
                    SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                    //SprBalle.SetPosition(SprBalle.Position.X, mesPerso.Position.Y - mesPerso.HauteurSprite/2 - SprBalle.HauteurSprite);
                }

                if (SprPad.BoundingBox.Intersects(mesPerso.NextPositionY()))
                {
                    if (mesPerso is pFire pFire)
                    {
                        pFire.currentState = Personnages.State.Catch;
                        SprBalle.CurrentBallState = Balle.BallState.SpeedUp;
                        AssetsManager.PlaySFX(audio.CatchLife);
                        ResolutionEcran.currentState = ScreenManager.State.Narrow;
                        niveau.lstPerso.Remove(pFire);
                        
                    }
                    else if (mesPerso is pIce pIce)
                    {
                        pIce.currentState = Personnages.State.Catch;
                        SprBalle.CurrentBallState = Balle.BallState.SlowDown;
                        AssetsManager.PlaySFX(audio.CatchLife);
                        ResolutionEcran.currentState = ScreenManager.State.Wide;
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
            pBatch.Draw(hud.texture, new Vector2(0, 0), Color.White);
            hud.DrawScore();
            SprPad.Draw();
            SprBalle.DrawBall();

            niveau.DrawLevel();
            foreach (var Briques in niveau.ListeBriques)
            {
                if (Briques.nbHits <= 0)
                { 
                    string points = "+" + Briques.Points.ToString();
                    pop.DrawPopUp(points);
                }   
            }
            
        }
    }
}