using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
    internal class AssetManager
    {
        public static SpriteFont MainFont { get; private set; } // pour pouvoir utiliser partout on peu utiliser "static". Elle existe en mémoire pour tout le monde. pas la peine de faire "new"
        public static Song MusicGameplay { get; private set; }

        // Différents assets qu'on doit télécharger pour différentes scènes.(music, police etc...) Uniquement les choses qui prennent pas de temps de chargement. Les petits trucs.
        public static void Load(ContentManager pContent )
        {
            MainFont = pContent.Load<SpriteFont>("MainFont");
            MusicGameplay = pContent.Load<Song>("techno");
        }
        public AssetManager() { }
    }
}
