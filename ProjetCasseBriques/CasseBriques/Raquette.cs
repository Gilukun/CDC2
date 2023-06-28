﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Raquette : Sprites
    {
        ScreenManager screen = ServiceLocator.GetService<ScreenManager>();
        private int speed;
        public Raquette(Texture2D pTexture) : base(pTexture)
        {
            speed = 30;
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q)) 
            {
                Position = new Vector2 (Position.X - speed, Position.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position = new Vector2(Position.X + speed, Position.Y);
            }
            if (Position.X + SpriteWidth/2  > screen.Width)
            {
                SetPosition(screen.Width - SpriteWidth/2, (int)Position.Y);
            }
            if (Position.X - SpriteWidth /2 < 0)
            {
                SetPosition(SpriteWidth / 2, (int)Position.Y);
            }
            base.Update();
        }

    }
}
