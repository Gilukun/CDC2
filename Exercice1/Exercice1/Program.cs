using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice1
{
    class Program
    {
        static void Main(string[] args)
        {
            Personnages monPerso = new Personnages("Mac Gyver");
            monPerso.CréerPerso();
            Console.WriteLine(monPerso);

            Console.WriteLine("Mon perso se prend un impact au hasard");
            monPerso.CréerImpact();
            Console.WriteLine(monPerso);
        }

    }

}