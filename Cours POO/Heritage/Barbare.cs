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
            DePointsDeVie = "1D12";
            }
    }
}
