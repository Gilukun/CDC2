using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ListeImages
{
    public interface ScreenSize // on rajoute une couche d'abstraction à notre ScreenManager.                           
                                // Les intances auront accès qu'a cette info.Elle "expose" que la partie de cette classe 
                                // Interface ne contient que des méthode et pas de propriété (fonction)
    {
       Point GetScreenSize();  // point c'est comme Vector mais avec des int à la place des float
        int ScreenWidth { get; }
        int ScreenHeitgh { get; }

       }
    internal class ScreenManager
    {
        private GraphicsDeviceManager _graphics;
        public int ScreenWidth // propriété qui permet de récuperer des infos du manager sans qu l'utilisateur puisse savoir d'ou ça vient. Et permet de traiter la propriété comme une variable.
        { get { return _graphics.PreferredBackBufferWidth; } }
        public int ScreenHeight{ get { return _graphics.PreferredBackBufferHeight; } }

        public ScreenManager(GraphicsDeviceManager pGraphics) 
        
        { 
         _graphics = pGraphics;
        }
        public Point GetScreenSize()
        {
            return new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }


        public void ChangeResolution(int Width, int Height) // méthode pour changer la taille de l'écran.
        { 
            _graphics.PreferredBackBufferWidth = Width;
            _graphics.PreferredBackBufferHeight = Height;
            _graphics.ApplyChanges();
        
        }


    }
}
