using System;
namespace Heritage
{
    public class Des
    {
        public int resulatTirages;
        private Random des;

        public Des()
        {
            des = new Random();
        }

        public int LanceDe(string pFaces)
        {
            resulatTirages = 0;

            switch (pFaces)
            {
                case "1D6":
                    resulatTirages = des.Next(1, 6 + 1);
                    break;
                case "1D10":
                    resulatTirages = des.Next(1, 10 + 1);
                    break;
                case "1D20":
                    resulatTirages = des.Next(1, 20 + 1);
                    break;
                default:
                    Console.WriteLine("N'noublie pas le paramètre du dé");
                    break;
            }
            return resulatTirages; 
        }
        
    }
}

