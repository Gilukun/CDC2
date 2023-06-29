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
        public enum State
        {
            noHit,
            Hit,
        }
        public State Bulletstate;
        public Bullet(Texture2D ptexture) : base(ptexture)
        {
            texture = ptexture;
            impact = 1;
            ListeBalles = new List<Bullet>();
            Bulletstate = State.noHit;
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
