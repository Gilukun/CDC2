using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Briques2
{
    
    internal class Positions
    {
        private Random des;
        private int resultat;
     
        public Positions() 
        {
          des = new Random();

        }

        public int getPosition(int pFaces)
         
        {
            resultat = 0;
            switch (pFaces)
            {
                case 6 : 
                        resultat = des.Next(1, 6 + 1); 
                    break;
                case 10:
                    resultat = des.Next(1, 10 + 1);
                    break;
                case 20:
                    resultat = des.Next(1, 20 + 1);
                    break;
                default:
                    Console.WriteLine("Oublie pas le nombre de face");
                    break;
            }
            return
                resultat;
        } 
       

    }
}
