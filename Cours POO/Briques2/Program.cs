using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Briques2
{
    

    internal class Program
    {
        private static List<Briques> listeBriques; // pourquoi il m'oblige à mettre static ? 

        static void Main(string[] args)

        {
            listeBriques = new List<Briques>();

           
          

            Pouvoir mesBriquesPowerUp = new Pouvoir();
            mesBriquesPowerUp.maClasse();
            listeBriques.Add(mesBriquesPowerUp);
            
            Health mesBriquesHealth = new Health();
            mesBriquesHealth.maClasse();
            listeBriques.Add(mesBriquesHealth);

            Balls mesBriquesBalls = new Balls();
            mesBriquesBalls.maClasse();
            listeBriques.Add(mesBriquesBalls);


            Console.WriteLine("Voici les effets des briques présentes dans ma liste de briques ");
            foreach (Briques items in listeBriques)
            {
                items.briqueClass();
            }



            

            foreach (Briques items in listeBriques)
            { 
            }

            Console.Read();
        }
    }
}
