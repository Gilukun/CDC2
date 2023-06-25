using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class AssetsManager
    {
        public SpriteFont TitleFont { get; set; }
        public SpriteFont MenuFont { get; set; }
        public SpriteFont HUDFont { get; set; }
        public SpriteFont GameOverFont { get; set; }
        public SpriteFont ContextualFont { get; set; }

        public Song Intro { get; set; }
        public Song End { get; set; }
        public Song InGame { get; set; }

        public SoundEffect PadRebound { get; set; }
        public SoundEffect CatchLife { get; set; }
        public SoundEffect Select { get; set; }
        public SoundEffect hitBricks { get; set; }



        public void Load()
        {
            ContentManager pContent = ServiceLocator.GetService<ContentManager>();
            TitleFont = pContent.Load<SpriteFont>("TitleFont");
            MenuFont = pContent.Load<SpriteFont>("MenuFont");
            HUDFont = pContent.Load<SpriteFont>("HUD1Font");
            GameOverFont = pContent.Load<SpriteFont>("GameOver");
            ContextualFont = pContent.Load<SpriteFont>("PopUpFont");

            // Soundtracks
            Intro = pContent.Load<Song>("Musics\\Intro");
            InGame = pContent.Load<Song>("Musics\\GamePlay");
            End = pContent.Load<Song>("Musics\\End");

            // SFX
            PadRebound = pContent.Load<SoundEffect>("Musics\\HitMetal");
            CatchLife = pContent.Load<SoundEffect>("Musics\\CatchPersonnage");
            Select = pContent.Load<SoundEffect>("Musics\\Selection");
            hitBricks = pContent.Load<SoundEffect>("Musics\\HitFreeze");


        }

        public static Vector2 GetSize(string pText, SpriteFont pFont)
        {
            Vector2 textsize = pFont.MeasureString(pText);
            return textsize;
        }

        public static Rectangle getBoundingBox(string pText, SpriteFont pFont, Vector2 position)
        {
            Vector2 textsize = pFont.MeasureString(pText);

            Rectangle boundingBox = new Rectangle((int)position.X, (int)position.Y, (int)textsize.X, (int)textsize.Y);
            return boundingBox;
        }

        public void PlaySFX(SoundEffectInstance pInstance, SoundEffect pSound)
        {
            pInstance = pSound.CreateInstance();
            pInstance.Play();
        }
    }
}
