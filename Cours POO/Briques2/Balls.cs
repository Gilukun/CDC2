using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briques2
{
    internal class Balls : Briques

    {
        public override void maClasse()
        {
            Console.WriteLine("Je suis une balle");
        }
        public override void briqueClass()
        {
            Console.WriteLine("Je suis une brique qui rajoute une balle supplémentaire");
        }
    }
}
