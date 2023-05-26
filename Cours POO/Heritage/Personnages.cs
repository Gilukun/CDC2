using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Heritage
{
    internal class Personnages
    {
        public string Nom;
        public int TotalDePointsDeVie;
        protected string DePointsDeVie;

        public Personnages( string pNom)
        {
            Console.WriteLine("Je créer un personnage");
            Nom = pNom;
            DePointsDeVie = "";
            TotalDePointsDeVie = 0;
           
        }
        

    }
}
