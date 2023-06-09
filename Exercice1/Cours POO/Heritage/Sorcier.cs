using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage
{
    internal class Sorcier : Personnages
    {
        public void LanceSort(string pNomSort)
        {
            Console.WriteLine("Je suis un sorcier et je lance un sort " + pNomSort);
        }
        public Sorcier(string pNom) : base(pNom)
        {
            Console.WriteLine("Je créer un Sorcier");
            DePointsDeVie ="1D6";

            Des deSorcier;
            deSorcier = new Des();
            int point2vie = 0;
            point2vie = deSorcier.LanceDe("1D6");
            TotalDePointsDeVie = point2vie;
            Console.WriteLine("Mon Sorcier a : " + TotalDePointsDeVie + " point de vie");
            
        }
    }
}
