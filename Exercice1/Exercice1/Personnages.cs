using System;
namespace Exercice1
{
	public class Personnages
	{
		public string Nom;
		public int Vie;
		public int Classe;
		public int Charisme;
		public int Intelligence;
		public int Sagesse;

		public Personnages(string pNom)
		{
			Console.WriteLine("Le nom du personnage est : " + pNom);
			Nom = pNom;
		}

		public void CréerPerso()
		{
			Des LanceDés = new Des();
			Vie = LanceDés.Lancer("6 faces");
            Classe = LanceDés.Lancer("6 faces");
            Charisme = LanceDés.Lancer("10 faces");
            Intelligence = LanceDés.Lancer("6 faces");
            Sagesse = LanceDés.Lancer("20 faces");
        }

        public override string ToString()
		{
            string caract;
            caract = "Personnage " + Nom;
            caract += "\n ############";
            caract += "\n";
            caract += "Vie = " + Vie;
            caract += "\n";
            caract += "Classe = " + Classe;
            caract += "\n";
            caract += "Charisme = " + Charisme;
            caract += "\n";
            caract += "Intelligence = " + Intelligence;
            caract += "\n";
            caract += "Sagesse = " + Sagesse;
            caract += "\n ############";
            caract += "\n";
            return caract;
        }

        public void CréerImpact()
        {
            Des Impact = new Des();
            int damage = Impact.Dommage("DesImpact");
            Console.WriteLine("Impact de " +  damage);
            Vie = Vie - damage;
        }
	}

}

