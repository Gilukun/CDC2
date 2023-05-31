using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Briques2
{
    internal class Health : Briques

    {
        public override void maClasse()
        {
            Console.WriteLine("Je suis une vie");
        }
        public override void briqueClass()
        {
            Console.WriteLine("Je suis une brique qui donne de la vie supplémentaire");
        }
    }
}
