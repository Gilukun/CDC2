using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briques3
{
    internal class Briques
    {
        private int x;
        private int y;
        public string Nom;
        private int vitesse;
        private int vie;

        public Briques(string pNom, int pX, int pY,int pVitesse, int pVie)
        {
            Nom = pNom;
            x = pX;
            y = pY;
            vitesse = pVitesse;
            vie = pVie;

            Trace.WriteLine("Je suis une brique de base");
            Trace.WriteLine("Je suis à la position x : " + x + "et y : " + y);
            Trace.WriteLine("J'ai " + vitesse + " de vitesse");
            Trace.WriteLine("J'ai " + vie + " points de vie");

        }

    }
}
