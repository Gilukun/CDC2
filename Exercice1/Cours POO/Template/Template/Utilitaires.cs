﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{ // Classe utilisé pour les propriété et méthode statique comme les randoms...
    public class Utilitaires
    {

        static Random RandomGen = new Random();
        private static Vector2 position;
        private static Vector2 direction;

        public static void SetRandomSeed(int pSeed) // ici on peu forcer le seed du random pour avoir toujours les meme chiffres aléatoire
        {
            RandomGen = new Random(pSeed);
        }
        public static int GetInt(int pMin, int pMax)
        {
            return RandomGen.Next(pMin, pMax + 1);
        }


        public static bool CollideByBox(IActor p1, IActor p2)
        {
            return p1.BoundingBox.Intersects(p2.BoundingBox);  // test de collisions entre les bounding box des deux acteurs avec Intersects
        }

       
    }
}
