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
            DePointsDeVie ="1D8";
            Console.WriteLine("Je créer un Sorcier");
        }
    }
}
