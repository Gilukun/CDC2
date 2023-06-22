using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Level
    {
        public int numero { get; set; } // pour que Json fonction il faut sérialiser, donc il faut absolumnt mettre get;set.
        public int[][] Map { get; set; } // on créer une liste avec 2 champs
        public int LevelMax;
        public Level() { } // constructeur par défaut pour pourvoir désérialiser le fichier. Il faut donc qu'il soit vide car il doit faire en background un new sans paramètre
        public Level(int pNumero)
        {
            numero = pNumero;
            LevelMax = 4;
        }
        
        public void RandomLevel() 
        {
            Random rnd = new Random();
            Map = new int[3][]; // on créer en nouveau tableau de 10 
            for (int l = 0; l < 3; l++)
            {
                Map[l] = new int[5]; // on lui dit qu'il a 10 ligne
                for (int c = 0; c < 5; c++)
                {
                    Map[l][c] = rnd.Next(1,2); // dans chaque case on met un rnd 
                }

            }
        }

        public void Save()
        {
            string jsonLevel = JsonSerializer.Serialize(this); // on créer le fichier JSON
            File.WriteAllText("level" + numero + ".json", jsonLevel); // on l'exporte en fichier .Json
        }
    }
}

