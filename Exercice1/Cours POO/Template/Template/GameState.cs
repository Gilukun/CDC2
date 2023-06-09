using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
    public class GameState
    {
        public enum SceneType // liste des scènes disponibles pour notre jeu
        {
            Menu,
            Gameplay,
            Gameover

        }

        protected MainGame mainGame;
        public Scene CurrentScene { get; set; }


        public GameState(MainGame pGame)
        
        {
            mainGame = pGame;
                }

        public void ChangeScene(SceneType psceneType)

        {
            if( CurrentScene != null ) 
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }

            switch( psceneType ) 
            {
                case SceneType.Menu:
                    CurrentScene = new SceneMenu(mainGame);
                     break; 
                case SceneType.Gameplay:
                    CurrentScene = new SceneGameplay(mainGame);
                    break   ; 
                case SceneType.Gameover:
                    CurrentScene = new SceneGameOver(mainGame);
                    break ;
                default:
                    break;
            }

            CurrentScene.Load();
        }
    }
}
