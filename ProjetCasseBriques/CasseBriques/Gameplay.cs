using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace CasseBriques
{
    public class Gameplay : ScenesManager
    {
        ScreenManager screen = ServiceLocator.GetService<ScreenManager>();
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        GameState status = ServiceLocator.GetService<GameState>();
        AssetsManager audio = ServiceLocator.GetService<AssetsManager>();
        HUD hud = ServiceLocator.GetService<HUD>();
        Bullet bullet = ServiceLocator.GetService<Bullet>();

        Raquette pad;
        Balle ball;
        PopUp pop;
        Bonus life;
        Bonus bigBall;
        LevelManager level = new LevelManager();

        List<Bonus> listeBonus = new List<Bonus>();
        List<PopUp> listePopUp = new List<PopUp>();
        private Texture2D background;
        public bool stick;
        public bool isKeyboardPressed;
        KeyboardState oldKbState;
        KeyboardState newKbState;

        private int currentBackground;

        private float delay;
        private float timer;
        private bool timerOff;

        private Random rnd;

        public int LevelNB
        {
            get { return hud.level; }
            }
        public void TimerON(float pIncrement)
        {
            delay += pIncrement;
            if (delay > timer)
            {
                timerOff = true;
            }
        }

        public void LoadBackground()
        {
            ContentManager _content = ServiceLocator.GetService<ContentManager>();
            background = _content.Load<Texture2D>("Backgrounds\\Back_" + currentBackground);
        }

        public Gameplay()
        {
            currentBackground = 1;
            LoadBackground();

            //Soundtracks
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(audio.InGame);
            
            // texture de ma raquette
            pad = new Raquette(_content.Load<Texture2D>("Pad_2"));
            pad.SetPosition(screen.HalfScreenWidth, screen.Height - pad.HalfHeitgh);
           
            // Texture de ma balle
            ball = new Balle(_content.Load<Texture2D>("Balls\\bNormal"));
            ball.SetPosition(pad.Position.X, pad.Position.Y - pad.HalfHeitgh - ball.HalfHeitgh);

            ball.Vitesse = new Vector2(6, -4);
            stick = true;

            hud.texture = _content.Load<Texture2D>("HUD2");
            pop = new PopUp();

            oldKbState = Keyboard.GetState();

            hud.level = 2;
            level.LoadLevel(hud.level);
            delay = 0;
            timer = 5;
            rnd = new Random(); 

            bullet.hasWeapon= true;
        }

        public override void Update()
        {
            newKbState = Keyboard.GetState();
            pad.Update();
            
            if (newKbState.IsKeyDown(Keys.Space) && !oldKbState.IsKeyDown(Keys.Space))
            {
                stick = false;
                //ball.collision = false;
            }

            ball.Update();

            if (bullet.hasWeapon)
            {
                if (newKbState.IsKeyDown(Keys.P) && !oldKbState.IsKeyDown(Keys.P))
                {
                    bullet.CreateBullet("Hero", pad.Position.X, pad.Position.Y, 5);
                    audio.PlaySFX(audio.shoot);
                }
            }

            for (int bullet = this.bullet.ListeBalles.Count - 1; bullet >= 0; bullet--)
            {
                Bullet bullets = this.bullet.ListeBalles[bullet];
                this.bullet.Update();
                if (bullets.Position.Y <= hud.Hudhauteur)
                {
                    this.bullet.ListeBalles.Remove(bullets);
                }
            }
            oldKbState = newKbState;

           
            // La balle reste collée à la raquette
            if (stick)
            {
                ball.SetPosition(pad.Position.X, pad.Position.Y - pad.HalfHeitgh - ball.HalfHeitgh);
            }

            RebondRaquette();
            CollisionSolidBricks();
            CollisionBricks();
            CollisionPersonnage();
            CollisionBulletsBricks();
            GetBonus();
            LoseLife();
            NextLevel();

            base.Update();
        }

        public void RebondRaquette()
        {
            if (pad.BoundingBox.Intersects(ball.BoundingBox))
            {
                audio.PlaySFX(audio.PadRebound);
                ball.Vitesse = new Vector2(ball.Vitesse.X, -ball.Vitesse.Y);
                ball.SetPosition(ball.Position.X, pad.Position.Y - ball.SpriteHeight);
            }
        }

        public void NextLevel()
        {
            if (!level.listBriques.Any(brique => brique.isBreakable)) // comme count mais avec de meilleur performance/ Proposé par VisualStudio
            {
                level.listPerso.Clear();
                listeBonus.Clear();
                bullet.ListeBalles.Clear();
                currentBackground++;
                hud.level++;

                if (hud.level > level.LevelMax)
                {
                    status.ChangeScene(GameState.Scenes.Win);
                    screen.currentState = ScreenManager.State.Basic;
                    hud.level = 1;
                    currentBackground = 1;
                }
                else
                {
                    stick = true;
                    LoadBackground();
                    level.LoadLevel(hud.level);
                }
            }
        }
        public void GetBonus()
        {
            for (int p = listeBonus.Count - 1; p >= 0; p--)
            {
                Bonus mesitems = listeBonus[p];
                mesitems.Update();

                if (pad.BoundingBox.Intersects(mesitems.NextPositionY()))
                {
                    if (mesitems is BonusVie Vie)
                    {
                        mesitems.currentState = Bonus.BonusState.Catch;
                        hud.Vie += mesitems.addlife;
                        audio.PlaySFX(audio.CatchLife);
                        listeBonus.RemoveAt(p);
                    }
                    else if (mesitems is BonusImpact BigBall)
                    {
                        mesitems.currentState = Bonus.BonusState.Catch;
                        ball.CurrentBallState = Balle.BallState.Big;
                        audio.PlaySFX(audio.CatchLife);
                        listeBonus.RemoveAt(p);
                    }
                }
            }
        }

        public void CollisionPersonnage()
        {
            for (int p = level.listPerso.Count - 1; p >= 0; p--)
            {
                ball.collision = false;
                Personnages mesPerso = level.listPerso[p];
                mesPerso.Update();

                if (mesPerso.BoundingBox.Intersects(ball.NextPositionY()))
                {
                    ball.collision = true;
                    audio.PlaySFX(audio.hitBricks);
                    ball.InverseVitesseY();
                }
                else if (mesPerso.BoundingBox.Intersects(ball.NextPositionX()))
                {
                    ball.collision = true;
                    audio.PlaySFX(audio.hitBricks);
                    ball.InverseVitesseX(); 
                }
               

                if (pad.BoundingBox.Intersects(mesPerso.NextPositionY()))
                {
                    if (mesPerso is PersonnageFire pFire)
                    {
                        pFire.CurrentState = Personnages.State.Catch;
                        ball.CurrentBallState = Balle.BallState.SpeedUp;
                        audio.PlaySFX(audio.CatchLife);
                        screen.currentState = ScreenManager.State.Narrow;
                        level.listPerso.Remove(pFire);
                    }
                    else if (mesPerso is PersonnageIce pIce)
                    {
                        pIce.CurrentState = Personnages.State.Catch;
                        ball.CurrentBallState = Balle.BallState.SlowDown;
                        audio.PlaySFX(audio.CatchLife);
                        screen.currentState = ScreenManager.State.Wide;
                        level.listPerso.Remove(pIce);
                    }
                }

                if (ball.collision)
                {
                    mesPerso.CurrentState = Personnages.State.Falling;
                    mesPerso.Tombe();
                    if (mesPerso.Position.Y > screen.Height)
                    {
                        level.listPerso.Remove(mesPerso);
                    }
                }
            }
        }


        public void CollisionBulletsBricks()
        {
            for (int b = level.listBriques.Count - 1; b >= 0; b--)
            {
                Briques mesBriques = level.listBriques[b];
                mesBriques.Update();

                if (mesBriques.IsScalling == false)
                {
                    for (int bullet = this.bullet.ListeBalles.Count - 1; bullet >= 0; bullet--)
                    {
                        Bullet bullets = this.bullet.ListeBalles[bullet];
                        bullets.Update();
                        if (bullets.BoundingBox.Intersects(mesBriques.BoundingBox))
                        {
                            mesBriques.nbHits -= bullets.impact;
                            this.bullet.ListeBalles.Remove(bullets);
                        }
                    }

                    if (mesBriques.nbHits <= 0)
                    {
                        switch (mesBriques.id)
                        {
                            case Briques.ID.Feu:
                                mesBriques.rotate = true;
                                mesBriques.scalling = true;
                                break;
                            default:
                                break;
                        }
                        mesBriques.scalling = true;
                        hud.GlobalScore += mesBriques.points;
                        pop.SetPosition(mesBriques.Position.X, mesBriques.Position.Y);
                        listePopUp.Add(pop);
                        break;
                    }
                    if (mesBriques.scale <= 0)
                    {
                        level.listBriques.Remove(mesBriques);
                        listePopUp.Remove(pop);
                    }
                }
            }
        }
        public void CollisionBricks()
        {
            for (int b = level.listBriques.Count - 1; b >= 0; b--)
            {
                ball.collision = false;
                Briques mesBriques = level.listBriques[b];
                mesBriques.Update();

                if (mesBriques.IsScalling == false)
                {
                    if (mesBriques.BoundingBox.Intersects(ball.NextPositionY()))
                    {
                        ball.collision = true;
                        audio.PlaySFX(audio.hitBricks);
                        mesBriques.nbHits -= ball.Impact;
                        ball.InverseVitesseY();
                    }
                    if (mesBriques.BoundingBox.Intersects(ball.NextPositionX()))
                    {
                        ball.collision = true;
                        audio.PlaySFX(audio.hitBricks);
                        mesBriques.nbHits -= ball.Impact;
                        ball.InverseVitesseX();
                    }

                    else
                    {
                        ball.collision = false;
                    }
                   
                    if (ball.collision && mesBriques.isBreakable == true)
                    {
                        if (mesBriques.nbHits <= 0)
                        {
                            if (mesBriques is BriqueFeu Fire)
                            {
                                Fire.rotate = true;
                                Fire.scalling = true;
                                hud.GlobalScore += Fire.points;
                                Fire.currentState = Briques.State.Broken;

                                int dice = rnd.Next(1, 11);
                                if (dice >= 1 && dice <= 10)
                                {
                                    life = new BonusVie(_content.Load<Texture2D>("bTime"));
                                    life.SetPositionBonus(Fire.Position.X, Fire.Position.Y);
                                    life.currentState = Bonus.BonusState.Free;
                                    listeBonus.Add(life);
                                }

                                pop.SetPosition(Fire.Position.X, Fire.Position.Y);
                                listePopUp.Add(pop);
                            }
                            if (mesBriques is BriqueGlace Glace)
                            {
                                Glace.scalling = true;
                                hud.GlobalScore += Glace.points;
                                Glace.currentState = Briques.State.Broken;

                                int dice = rnd.Next(1, 11);
                                if (dice >= 1 && dice <= 10)
                                {
                                    bigBall = new BonusImpact(_content.Load<Texture2D>("bIce"));
                                    bigBall.SetPositionBonus(Glace.Position.X, Glace.Position.Y);
                                    bigBall.currentState = Bonus.BonusState.Free;
                                    listeBonus.Add(bigBall);
                                }

                                pop.SetPosition(Glace.Position.X, Glace.Position.Y);
                                listePopUp.Add(pop);
                            }
                            else
                            {
                                mesBriques.scalling = true;
                                hud.GlobalScore += mesBriques.points;
                                pop.SetPosition(mesBriques.Position.X, mesBriques.Position.Y);
                                listePopUp.Add(pop);
                            }
                        }
                    }
                    if (mesBriques.scale <= 0)
                    {
                        level.listBriques.Remove(mesBriques);
                        listePopUp.Remove(pop);
                    }
                }
            }
        }

        public void CollisionSolidBricks()
        {
            for (int bricks = level.listSolidBricks.Count - 1; bricks >= 0; bricks--)
            {
                Briques solidB = level.listSolidBricks[bricks];
                solidB.Update();
                if (solidB.BoundingBox.Intersects(ball.NextPositionY()))
                {
                    ball.collision = true;
                    CamShake = 30;
                    audio.PlaySFX(audio.hitBricks);
                    ball.InverseVitesseY();
                }
                else if (solidB.BoundingBox.Intersects(ball.NextPositionX()))
                {
                    ball.collision = true;
                    CamShake = 30;
                    audio.PlaySFX(audio.hitBricks);
                    ball.InverseVitesseX();
                }

                for (int bullet = this.bullet.ListeBalles.Count - 1; bullet >= 0; bullet--)
                {
                    Bullet bullets = this.bullet.ListeBalles[bullet];
                    if (bullets.BoundingBox.Intersects(solidB.BoundingBox))
                    {
                        hud.GlobalScore += solidB.points;
                        audio.PlaySFX(audio.hitBricks);
                        level.listSolidBricks.Remove(solidB);
                        this.bullet.ListeBalles.Remove(bullets);
                    }
                }
            }          
        }

        public void LoseLife()
        {
            if (ball.Position.Y > screen.Height)
            {
                hud.Vie--;
                //audio.PlaySFX(audio.ballLost);
                stick = true;
                if (hud.Vie <= 0)
                {
                    ball.CurrentBallState = Balle.BallState.Dead;
                    status.ChangeScene(GameState.Scenes.GameOver);
                    screen.currentState = ScreenManager.State.Basic;
                    hud.Vie = 3;
                }
            }
        }
        public override void DrawBackground()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
        }
        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            
            pBatch.Draw(hud.texture, new Vector2(0, 0), Color.White);
            hud.DrawScore();
            pad.Draw();
            ball.DrawBall();

            foreach (var Bonus in listeBonus)
            {
                pBatch.Draw(Bonus.texture,
                            Bonus.Position,
                            null,
                            Color.White,
                            0,
                            new Vector2(Bonus.HalfWidth, Bonus.HalfHeitgh),
                            1.0f,
                            SpriteEffects.None,
                            0);
            }

            level.DrawLevel();
            foreach (var PopUp in listePopUp)
            {
                foreach (var Briques in level.listBriques)
                {
                    if (Briques.nbHits <= 0)
                    {
                        string points = "+" + Briques.points.ToString();
                        PopUp.DrawPopUp(points);
                    }
                }
            }

            bullet.DrawWeapon();
        }
    }
}