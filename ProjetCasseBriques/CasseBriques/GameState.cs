using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class GameState
    {
        
        public ScenesManager CurrentScene { get; set; }
        protected CasseBriques casseBriques;
        public float Delay;
        public int Timer;
        public bool timerIsOver;
        public Scenes currentState { get; set; }
        public GameState(CasseBriques pGame)
        {
            casseBriques = pGame;
            Timer = 5;
            Delay = 0;
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

        public void TimerON(float pIncrement)
        {
            Delay += pIncrement;
            if (Delay > Timer)
            {
                timerIsOver = true;
            }
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

        public  void Update()
        {
        }
    }
}

