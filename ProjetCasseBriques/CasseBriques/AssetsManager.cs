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
        public SpriteFont TitleFont { get; private set; }
        public SpriteFont MenuFont { get; private set; }
        public SpriteFont HUDFont { get; private set; }
        public SpriteFont GameOverFont { get; private set; }
        public SpriteFont ContextualFont { get; private set; }
        public SpriteFont PopUpFont { get; private set; }
        public SpriteFont Victory { get; private set; }



        public Song Intro { get; private set; }
        public Song End { get; private set; }
        public Song InGame { get; private set; }

        public SoundEffect PadRebound { get; private set; }
        public SoundEffect CatchLife { get; private set; }
        public SoundEffect Select { get; private set; }
        public SoundEffect hitBricks { get; private set; }
        public SoundEffect hitWalls { get; private set; }
        public SoundEffect shoot { get; private set; }
        public SoundEffect enlarge { get; private set; }
        public SoundEffect hitMonster { get; private set; }
        public SoundEffect bulletHit { get; private set; }
        public SoundEffect ballLost { get; private set; }









        public void Load()
        {
            ContentManager pContent = ServiceLocator.GetService<ContentManager>();
            TitleFont = pContent.Load<SpriteFont>("TitleFont");
            MenuFont = pContent.Load<SpriteFont>("MenuFont");
            HUDFont = pContent.Load<SpriteFont>("HUD1Font");
            GameOverFont = pContent.Load<SpriteFont>("GameOver");
            ContextualFont = pContent.Load<SpriteFont>("PopUpFont");
            PopUpFont = pContent.Load<SpriteFont>("PopUps");
            Victory = pContent.Load<SpriteFont>("Victory");

            // Soundtracks
            Intro = pContent.Load<Song>("Musics\\Intro");
            InGame = pContent.Load<Song>("Musics\\GamePlay");
            End = pContent.Load<Song>("Musics\\End");

            // SFX
            PadRebound = pContent.Load<SoundEffect>("Musics\\HitMetal");
            CatchLife = pContent.Load<SoundEffect>("Musics\\CatchPersonnage");
            Select = pContent.Load<SoundEffect>("Musics\\Selection");
            hitBricks = pContent.Load<SoundEffect>("Musics\\HitFreeze");
            hitWalls = pContent.Load<SoundEffect>("Musics\\hitcadre");
            shoot = pContent.Load<SoundEffect>("Musics\\shoot");
            enlarge = pContent.Load<SoundEffect>("Musics\\enlarge");
            hitMonster = pContent.Load<SoundEffect>("Musics\\hitMonster");
            bulletHit = pContent.Load<SoundEffect>("Musics\\BulletHits");
            ballLost = pContent.Load<SoundEffect>("Musics\\Dead");

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

        public static void PlaySong(Song pSong)
        {
            MediaPlayer.Play(pSong);
        }
        public static SoundEffectInstance PlaySFX(SoundEffect pSound)
        {
            SoundEffectInstance  instance = pSound.CreateInstance();
            instance.Play();
            return instance;
        }
    }
}
