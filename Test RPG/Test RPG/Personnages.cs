using System;
using System.Diagnostics;
using TestRPG;

namespace Test_RPG
{
	public class Personnage /*renommer ici la class*/
	{
		public string Nom; /* premier membre, ici le personnage aura un nom*/
		public int Force;
		public int Dexterite;
		public int Constitution;
		public int Intelligence;
		public int Sagesse;
		public int Charisme; 

		public Personnage() /*Constructeur qui va généré les membres. Polymorphisme = On peut avoir plusieurs constructeur mais ils ne doivent pas avoir la même signature (pas même type de paramètre...)*/

		{
			Console.WriteLine("Constructeur sans paramètre"); 	
			Nom ="";
		}

		public Personnage(string pNom)
		{
            Console.WriteLine("Constructeur avec paramètre");
            Nom = pNom;
		}

		public void Genere() /* toujours indiquer ce que renvoi la méthode ici void */
		{
			Console.WriteLine("On genere un personnage aléatoirement");
			lanceurDe lanceur = new lanceurDe();
			
			Force = lanceur.Lance(4, "1D6", true);
			Dexterite = lanceur.Lance(4, "1D6", true);
            Constitution = lanceur.Lance(4, "1D6", true);
            Intelligence = lanceur.Lance(4, "1D6", true);
            Sagesse = lanceur.Lance(4, "1D6", true);
            Charisme = lanceur.Lance(4, "1D6", true); 
        }

		public void Affiche()
		{
            Console.WriteLine("Personnage {0}", Nom);
            Console.WriteLine("Dexterite : " + Dexterite);
            Console.WriteLine("Constitution : " + Constitution);
            Console.WriteLine("Intelligence : " + Intelligence);
            Console.WriteLine("Sagesse : " +  Sagesse);
            Console.WriteLine("Charisme : " +  Charisme);
        }

		public override string ToString() /* la class ToString existe déja et on souhaite utiliser la fonction mais changer ses méthodes. Donc on "override". notre nouveau ToString hérite de la class toString original mais avec l'override qu'on définit*/
			
		{
			string caract;
			caract = "Personnage" + Nom;
			caract += "\n ############";
            caract += "\n";

            caract += "Force = " + Force;
            caract += "\n";
            caract += "Dexterite = " + Dexterite;
            caract += "\n";
            caract += "Constinution = " + Constitution;
            caract += "\n";
            caract += "Intelligence = " + Intelligence;
            caract += "\n";
            caract += "Sagesse = " + Sagesse;
            caract += "\n";
            caract += "Charisme = " + Charisme;
            caract += "\n ############";
            caract += "\n";



            return caract;


		}
    }
}

