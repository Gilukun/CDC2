using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage
{
    internal class Barbare : Personnages
    {
     
        public Barbare(string pNom) : base (pNom)
            {
            Console.WriteLine("Je créer un nouveau barbare");

            Des monDe;
            monDe = new Des();
            int point2vie = 0;
            point2vie = monDe.LanceDe("1D6");
            TotalDePointsDeVie = point2vie;
            Console.WriteLine("Mon barbare a : " + TotalDePointsDeVie + " point de vie");
        }

    }
}
