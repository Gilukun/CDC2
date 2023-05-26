using System;
using System.Diagnostics;

namespace Test_RPG
{
    class lanceurDe
    {
        /* membres 
         1. donner visibilité : public (visible en dehors de l'objet / private(visible que par les méthodes de l'objet)
        2. créer une instance dans le program*/

        private Random random;
        public int dernierResultat;

        /* constructeur de random pour que le dé créer lui même sa randomisation a chaque fois qu'on appel notre class 
        1. Public / private
        2. Il ne renvoie rien, même pas void
        3. Il prend le nom de la class pour lequel il construit
        4. il a des paramètres si nécessaire
        --> a chaque fois qu'on instanci LanceurDe, le constructeur va créer un random avec un chiffre au hasard*/

        public lanceurDe()
        {
            random = new Random();
        }

        public int Lance(string pType)
        {
            dernierResultat = 0;

            switch (pType)
            {
                case "1D6":
                    dernierResultat = random.Next(1, 6 + 1); /* ne pas oublier l'exclusion donc 6+1*/
                    break;
                case "1D20":
                    dernierResultat = random.Next(1, 20 + 1);
                    break;
                default:
                    Debug.Fail("Mauvaise utilisation de Lance, Type inconnu"); /* pramaètre pour nous dire si jamais on a oublié un paramètre */
                    break;
            }

            return dernierResultat;
        } 

        public int Lance(int pLancer, string pType, bool pDropLowest = false)
        {
            int total = 0;
            int lowest = 0; 

            for (int i = 0; i < pLancer; i++)
            {
                Lance(pType);
                if (lowest == 0 || dernierResultat < lowest )
                {
                    lowest = dernierResultat; 
                }
                total += dernierResultat;

                Console.WriteLine("Lance dé numéro {0}, resultat {1} total est maintenant {2}", i, dernierResultat, total);
            }
            if (pDropLowest == true)
            {
                Console.WriteLine("Droplowest ({0})", lowest); 
                total = total - lowest;
                Console.WriteLine("New total ({0})", total); 
            }

            dernierResultat = total;
            return total;
        }

    }
}

