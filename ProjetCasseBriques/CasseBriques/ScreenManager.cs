using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    internal class ScreenManager
    {
        private GraphicsDeviceManager _graphics;
        public int Width
        {
            get
            {
                return _graphics.PreferredBackBufferWidth;
            }
        }
        public int Height
        {
            get
            {
                return _graphics.PreferredBackBufferHeight;
            }
        }

        public int CenterWidth
        {
            get
            {
                return _graphics.PreferredBackBufferWidth/2;
            }
        }
        public int CenterHeight
        {
            get
            {
                return _graphics.PreferredBackBufferHeight / 2;
            }
        }
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
