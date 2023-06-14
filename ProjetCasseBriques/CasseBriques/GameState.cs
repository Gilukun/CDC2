using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class GameState
    {
        public ScenesManager CurrentScene { get; set; }
        protected CasseBriques casseBriques; 
        public GameState(CasseBriques pGame) 
        {
            casseBriques = pGame;
        }

        public enum Scenes
        {
            Menu,
            Setting,
            Pause,
            Gameplay,
            Win,
            GameOver,
            
        }

        public void ChangeScene(Scenes pScene)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }
            switch (pScene)
            {
                case Scenes.Menu:
                    CurrentScene = new Menu(casseBriques);
                    break;
                case Scenes.Gameplay:
                    CurrentScene = new Gameplay(casseBriques);
                    break;
                case Scenes.Setting:
                    CurrentScene = new Settings(casseBriques);
                    break;

                default:
                    break;
            }

            CurrentScene.Load();
        }


    }
}
