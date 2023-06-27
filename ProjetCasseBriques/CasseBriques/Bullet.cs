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
        KeyboardState oldKbState;
        public KeyboardState newKbState;
        public bool hasWeapon;
        public Bullet(Texture2D ptexture) : base(ptexture)
        {
            texture = ptexture;
            ListeBalles = new List<Bullet>();
        } 

        public void CreateBullet(string pNom, float pX, float pY, int pSpeed)
        {

            Bullet bullet = new Bullet(_content.Load<Texture2D>("Balls\\bFire"));
            bullet.SetPosition(pX,pY);
            bullet.Vitesse = new Vector2(0, -1);
            bullet.Speed = pSpeed;
            ListeBalles.Add(bullet);
        }

        public override void Update()
        {
            foreach (var item in ListeBalles) 
            {
                item.Position += item.Vitesse * item.Speed;
            }

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
