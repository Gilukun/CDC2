using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_RPG;


/* class :
 1. class à des membres (ex : couleur, taille etc...)
2. class à des méthodes  (ex : avance recule etc..)
*/

namespace TestRPG
{
    
    class Program
    {
        static void Terminal(string pString)
        {
            Random random = new Random();
            Console.WriteLine("");

            foreach (char caractère in pString)
            {
                Console.Write("\b");
                
                Console.Write(caractère);
                Console.Write("/");
                Thread.Sleep(random.Next(10,50)); 

            }
            Console.Write("\b ");
            Console.WriteLine(""); 
        }

        static void Main(string[] args)
        {
            string ligne = new string('=', 30); /* on peut afficher de sligne pour séparer les résultats pour que ce soit plus lisible dans le terminal*/
            /* instanciation d'un lanceur "monLanceu" de la class "LanceurDe" */
            lanceurDe monLanceur;
            monLanceur = new lanceurDe();

            lanceurDe monLanceur2;
            monLanceur2 = new lanceurDe();

            Terminal("------ Création d'un personnage de RPG -------");

            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Je lance mon premier dé 6");
            monLanceur.Lance("1D6");
            Console.WriteLine("Résultat : {0}", monLanceur.dernierResultat); /* j'indique que je veux l'index du résultat {0} (ici qu'un résultat donc 0) pour le résultat de monlanceur*/
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ligne);

            Console.WriteLine("Je lance mon second dé 6");
            monLanceur.Lance("1D6");
            Console.WriteLine("Résultat : {0}", monLanceur.dernierResultat);
            Console.WriteLine(ligne);

            Console.WriteLine("Je lance mon premier dé 20");
            monLanceur2.Lance("1D20");
            Console.WriteLine("Résultat : {0} ", monLanceur2.dernierResultat);
            Console.WriteLine(ligne);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Personnage généré");
            Console.ForegroundColor = ConsoleColor.White;

            int monRésultat = monLanceur2.Lance("1D6"); /* on peut stocker directement le résultat d'un méthode dans une variable */
            Console.WriteLine("En stockant la méthode dans une variable on a {0}", monRésultat);


            Personnage monPerso = new Personnage("Yahoo");
            monPerso.Genere();
            monPerso.Affiche();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(monPerso);
            Console.ForegroundColor = ConsoleColor.White;


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Personnage généré");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Read();
        }

    }
}