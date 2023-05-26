using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Exercice1
{
	class Des
	{
		private Random rnd;
		public int resultat;

		public Des()
		{
            rnd = new Random();
        }
			

		public int Lancer(string pType)
		{
			resultat = 0;

			switch (pType)
			{
				case "6 faces":
					resultat = rnd.Next(1, 6 + 1);
					break;
                case "10 faces":
                    resultat = rnd.Next(1, 10 + 1);
                    break;
                case "20 faces":
                    resultat = rnd.Next(1, 20 + 1);
                    break;
				default:
					Console.WriteLine("Manque le paramètre type");
					break;
            }

			return resultat;

		}

		public int Dommage(string pType)
		{ 
			int resultatimpact = 0;

			switch (pType)
			{
				case "DesImpact":
					resultatimpact = rnd.Next(1, 6 + 1);
					break;
				default:
					Console.WriteLine("Manque le paramètre type");
					break;
			}
			
            return resultatimpact;
        }
        
    }
}

