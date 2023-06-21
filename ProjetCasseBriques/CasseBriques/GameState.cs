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
                    CurrentScene = new Menu();
                    break;
                case Scenes.Gameplay:
                    CurrentScene = new Gameplay();
                    break;
                case Scenes.Setting:
                    CurrentScene = new Settings();
                    break;
                case Scenes.Win:
                    CurrentScene = new Win();
                    break;
                case Scenes.GameOver:
                    CurrentScene = new GameOver();
                    break;
                default:
                    break;
            }

            CurrentScene.Load();
        }

        public void Update()
        { 
        
        }


    }
}
