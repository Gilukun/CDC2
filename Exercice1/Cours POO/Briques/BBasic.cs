using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briques
{
    internal class BBasic : Briques
    {
        public override void Nommer()
        {
            nom = "Brique de base";
            Trace.WriteLine("Je suis une " + nom);
        }


        public override void Tape()
        {
            Trace.WriteLine("Tu m'a touché ! Je donne des points"); 
        }
        public override void AddPoints()
            {
            points += 10; 
                Trace.WriteLine("Je rajoute : " + points + "points au Score");
            }
    }
}
