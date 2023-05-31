using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briques2
{
    internal abstract class Briques
    {
        public int x;
        public int y;
        private int vitesse;
        protected int vie;

        public Briques()
        {
 
        }
        public void setPosition( int pX, int pY)
        {
            x = pX;
            y = pY;
            Console.WriteLine("Ma brique est positionnée à x :" + x + " y : " + y);
        }

        public abstract void maClasse();

        public abstract void briqueClass();


    }
}
