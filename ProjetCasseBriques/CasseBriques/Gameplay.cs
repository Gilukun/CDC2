using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Gameplay : ScenesManager
    {
        private Texture2D background;
        Raquette SprPad;
        Balle SprBalle;
        public bool Stick;
        public Gameplay(Game pGame) : base (pGame) 
        {
            ScreenManager ResolutionEcran = ServiceLocator.GetService<ScreenManager>();
            background = pGame.Content.Load<Texture2D>("BckGameplay");
            // texture de ma raquette
            SprPad= new Raquette(pGame.Content.Load<Texture2D>("pNormal"));
            SprPad.SetPosition(ResolutionEcran.Width / 2 - SprPad.LargeurSprite/2, ResolutionEcran.Height - SprPad.HauteurSprite);
            // Texture de ma balle
            SprBalle = new Balle(pGame.Content.Load<Texture2D>("bNormal"));
            SprBalle.SetPosition(SprPad.Position.X + SprPad.LargeurSprite/2 - SprBalle.LargeurSprite/2,  
                                    SprPad.Position.Y - SprBalle.HauteurSprite) ;
            SprBalle.Vitesse = new Vector2(5, -6);
            Stick= true;
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Stick = false;
            }

            if (Stick)
            {
                SprBalle.SetPosition(SprPad.Position.X + SprPad.LargeurSprite / 2 - SprBalle.LargeurSprite / 2,
                                    SprPad.Position.Y - SprBalle.HauteurSprite);
            }

            SprPad.Update();

            

            if (SprBalle.Position.Y > 0)
            {
                Stick = true;
            }

            if (SprPad.BoundingBox.Intersects(SprBalle.BoundingBox))
            {
                SprBalle.Vitesse = new Vector2(SprBalle.Vitesse.X, -SprBalle.Vitesse.Y);
                SprBalle.SetPosition(SprBalle.Position.X, SprBalle.Position.Y - SprBalle.HauteurSprite);
            }
            SprBalle.Update();



        }
        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            pBatch.Draw(background, new Vector2(0, 0), Color.White);
            SprPad.Draw();
            SprBalle.Draw();
        }
    }
}
