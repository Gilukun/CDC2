using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briques2
{
    internal class Pouvoir : Briques
    {
        public override void maClasse()
        {
            Console.WriteLine("Je suis un pouvoir");
        }

        public override void briqueClass()
        {
            Console.WriteLine("je suis une brique qui donne un pouvoir");
            }

        public void changeVitesse()
        { 
            
        }
    }
}
