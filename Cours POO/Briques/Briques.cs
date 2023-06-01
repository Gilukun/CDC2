using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briques
{
    internal abstract class Briques
    {
        protected int x;
        protected int y;
        protected int vie;
        protected int points;
        protected string nom;

        public void AssignerVie(int pVie)
        {
            vie = pVie;
            Trace.WriteLine("J'ai " + vie + " point de vie");
        }
        public abstract void Nommer(); 
        public abstract void AddPoints();
        public abstract void Tape();
    }


}
