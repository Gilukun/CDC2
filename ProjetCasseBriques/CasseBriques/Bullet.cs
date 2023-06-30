using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;



namespace CasseBriques
{
    public class Bullet : Sprites
    {
        ContentManager _content = ServiceLocator.GetService<ContentManager>();
        public List<Bullet> ListeBalles;
        public bool HasBullet;
        public int impact;
        public bool collision;
        private float  Delay = 0;
        private int Timer = 5;
        private bool TimerIsOver;
        public enum State
        {
            NoTActivated,
            Activated,
        }
        public State Bulletstate { get;set; }

        
        public void TimerON(float pIncrement)
        {
            Delay += pIncrement;
            if (Delay >= Timer)
            {
                TimerIsOver = true;
            }
        }
        public Bullet(Texture2D ptexture) : base(ptexture)
        {
            texture = ptexture;
            impact = 1;
            ListeBalles = new List<Bullet>();
            Bulletstate = State.NoTActivated;
            Speed = 1;
        } 

        public void CreateBullet(string pNom, float pX, float pY, float pSpeed)
        {
            Bullet bullet = new Bullet(_content.Load<Texture2D>("Balls\\bFire"));
            bullet.SetPosition(pX,pY);
            bullet.Vitesse = new Vector2(0, -1);
            bullet.Speed = pSpeed;
            ListeBalles.Add(bullet);
        }

        public void BulletMoves()
        {
            Position += Vitesse * Speed;
        }

        public override void Update()
        {
            BulletMoves();
            if (Bulletstate == State.Activated) 
            {
                if (!TimerIsOver)
                {
                    TimerON(0.01f);
                }
                if (Delay >= Timer)
                {
                    Delay = 0;
                    Bulletstate = State.NoTActivated;
                }
            }
            TimerIsOver = false;
            base.Update();
        }

        public void DrawWeapon()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();  
            foreach (var item in ListeBalles)
            {
                pBatch.Draw(item.texture, item.Position, Color.White);
                //pBatch.DrawRectangle(item.BoundingBox, Color.Red);
            }
        }
    }
}
