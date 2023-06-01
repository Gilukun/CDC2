using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briques
{
    internal class bPower : Briques
    {
        public override void Nommer()
        {
            nom = "Brique de Pouvoir";
            Trace.WriteLine("Je suis une " + nom);
        }

        public override void Tape()
        {
            Trace.WriteLine("tu m'a touché ! je te donne des supers pouvoirs");
        }

        public override void AddPoints()
        {
            points += 20;
            Trace.WriteLine("Je rajoute : " + points + "points au Score");
        }

       
    }
}
