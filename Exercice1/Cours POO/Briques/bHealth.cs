using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Briques
{
    internal class bHealth : Briques
    {
        public override void Nommer()
        {
            nom = "Brique de vie";
            Trace.WriteLine("Je suis une " + nom);
        }
        public override void Tape()
        {
            Trace.WriteLine("Tu m'a touché ! Je donne de la vie");
        }

        public override void AddPoints()
        {
            points += 50;
            Trace.WriteLine("Je rajoute : " + points + "points au Score");
        }
       
        
    }
}
