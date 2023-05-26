using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Barbare monBarbare = new Barbare("Conan");
            Console.WriteLine("Mon barbare s'appelle : " + monBarbare.Nom);
            
            Sorcier monSorcier = new Sorcier("Panoramix");
            Console.WriteLine("Mon sorcier s'appelle : " + monSorcier.Nom);
            monSorcier.LanceSort("Avadakadabra");

            Console.ReadKey();

        }
    }
}
