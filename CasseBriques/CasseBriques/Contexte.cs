using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public static class Contexte // classe qui garde en mémoire toutes les infos. pratique pour les score/ level etc...
    {
        // -> personne ne peut accéder a ces info il faut faire des propriétés
        // private static int nbVie;
      
        /*
        par exemple si on veut que les score donnent des vies à un certain score on fati
        public static int Score
        { get
            {
                return score;
            }
            set
            {
                score = value;
                if (score > value)
                { nbVie = 1;}
            }
        }
        */

        // Si on veut faire une propriété automatique en mode "propre"
        public static int Score { get; private set; } // le contexte gère le score et les scene ne peuvent pas le modifier
        public static void AddScore(int points)
        {
            Score += points;
        }

        // En mode facile avec une variable public

        public static int nbVie; 






        static Contexte()
        {
            Score = 0; 
            nbVie = 0;
        }
    }
}
