using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* Utilisation de librairie */
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestCSHarp2;


public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Random monDe = new Random(System.DateTime.Now.Millisecond); /* Random vient de la librairie system. On créer un nouvel objet Random qui utilise toujours un seed différent System.Datetime.Now.Millisecond*/
    /* 
    FONCTIONS 
    fonction lancer un dé
       1. visibilité de la fonction : public --> tout le monde y accède
       2. type de retour de la fonction :
            - void --> pas de résultat attendu
            - int --> renvoie un entier
       3. nom de la fonction
        4. Type de paramètre et nom du paramètre
    */

    public int lanceD( int nombreDeFace)
    {
        int resultatDe = monDe.Next(1, nombreDeFace + 1); /* dans notre variable resultaDE on stock notre random de 1 à nombre limite exclus Donc on rajoute + 1*/
        return resultatDe;
    }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        System.Console.WriteLine("Bonjour");
        /* types principaux de variable*/

        /*TYPE NUMERIQUE ENTIER = int
         PlaySoldier : dire a C# d'afficher un variable à un endroit (...{0}.. ; variable)*/
        int nNombreDeVies = 5;
        int nEnergie = 100;
        System.Console.WriteLine("J'ai {0} vie et {1} energie", nNombreDeVies, nEnergie);
        /* il faut numéroter les variables dans l'ordre quand on utilise le Playsoldier"

        /* TYPE NUMERIQUE FLOTANT*/
        float nPosition = 1.5f;
        System.Console.WriteLine("Ma poisition est {0}", nPosition);

        /* TYPE NUMERIQUE BOOLEAN*/
        bool bPorteFermee;
        bPorteFermee = false;
        System.Console.WriteLine("Ma porte est elle fermée : " + bPorteFermee);
        bPorteFermee = true;
        System.Console.WriteLine("Ma porte est elle fermée : " + bPorteFermee);

        /* TYPE NUMERIQUE DOUBLE : valeur numérique énorme*/
        /* TYPE CARACTERE : caractère seul*/
        char cInitale = 'c';
        System.Console.WriteLine("Mon caractère {0} est un charactere unique", cInitale);

        /* CHAINE DE CARACTERES : STRING*/
        string sMessageJoueur = "";
        sMessageJoueur = "il te reste";
        /* autre solution : sMessageJoueur = "il te reste" + nEnergie " points d'énergie"; */
        System.Console.WriteLine(sMessageJoueur +" "+ nEnergie +" "+ "energie");
        System.Console.WriteLine(sMessageJoueur + " " + nEnergie + " " + "energie");

  
        System.Console.WriteLine("la chaine fait " + sMessageJoueur.Length + " caractères");


        /*  découper une chaine de caractère Substring(debut, fin) */
        string sBoutDeChaine = sMessageJoueur.Substring(3,6);
        System.Console.WriteLine(sBoutDeChaine);


        /* EXPRESSIONS : calcul pour avoir une nouvelle valeur avec des variables (+ - *) */

        int nDegats = 10;
        nEnergie = nEnergie - nDegats;
        System.Console.WriteLine(nEnergie);


        /* DECREMENTATION / INCREMENTATION */
        nEnergie --; /* moins 1*/
        System.Console.WriteLine(nEnergie);
        nEnergie ++;/* plus 1*/
        System.Console.WriteLine(nEnergie);
        nEnergie += 10; /* marche avec (+ - * /) */
        System.Console.WriteLine(nEnergie);

      
        /* Appel de la fonction lancer un dé : 
         1. déclarer une variable qui va contenir le resultat de la fonction
        2. appeler la fonction */
        int Resultat; 
        Resultat = lanceD(9);
        Console.WriteLine("Le resulat D 9 est " + Resultat);
        Resultat = lanceD(20);
        Console.WriteLine("Le resulat D 20 est " + Resultat);

        /* BOUCLE FOR
         1. 3 paramètres entre parenthèses
            - initialisation : int variable i (iterator, entier)
            - condition de fin : valeur maximal de i
            - increment : pas du décompte (plus 1, plus 2 etc... +1 --> i++)
        Attention : POINT VIRGULE ENTRE LES PARAMETRES
        */

        for (int i = 1; i <= 10; i++)
        {
            int monDe = lanceD(100);
            Console.WriteLine("Au lancer " +i+ " j'ai obtenu " + monDe);
        }

        /* BOUCLE WHILE
        1. Créer la variable en dehors des paramètres
        2. Un seul paramètre pour la boucle
        3. Toujours penser à incrémenter j !!! Sinon tout bloque ! 
       */

        int j = 1;

        while(j<=10)
        {
            Console.WriteLine("Ma variable est " + j);
            j++;

        }

        /* deuxième méthode while 
         1. DO la chose à executer WHILE condition*/
        j = 1;
        do
        {
            Console.WriteLine("Ma seconde variable j est " + j);
            j++;

        } while (j <= 10);


        /* TABLEAU 
        1. Utilise quand on sait le nombre d'élément
        2. une fois créé on ne peut pas rajouter des éléments une fois créé
        3. type de variable et []
        4. On declare le nombre d'éléments de notre tableau
        5. Entre crochet on mes les valeur de notre tableau
        6. Tableau est indexé à partir de 0
        int --> pour déclarer pas besoin de faire "new"
        string --> pour déclarer il faut faire "new". On créer un nouvel objet. Pas obliger de mettre le chiffre entre crochet.

        */

        int[] monTableau;
        monTableau = new int[5] { 1, 3, 5, 2, 9 };
        Console.WriteLine("Mon premier élément est " + monTableau[4]);



       
        /* ou 
         race[0] = "Humain"
        race[1] = "Gnome" etc... mais moins pratique*/

        /* for (int i = 0; i < races.Length; i++)
        {
            Console.WriteLine("Les races possibles sont " + races[i]);
        }

        foreach (string laRAce in races) pour tous les élémént dans la variable de type string "la race" on stock les éléments de races 
        {
            Console.WriteLine(laRAce);
        }
         */
        /* TABLEAU A PLUSIEURS DIMENSIONS 
        On rajoute une vigule dans les crochets pour indiquer qu'il y a deux dimensions
        Ne pas oublier que l'index commence à 0 !!!! */

        int[,] map = new int[,]
        {
            {0,0,1,0,9,200 },
            {0,3,0,3,5,0 },
            {0,6,4,0,5,0 },
            {0,9,10,0,111,0 },
            {0,0,40,0,30,0 },
        };

        Console.WriteLine("La valeur de le ligne 1 et de la colonne 6 est : " + map[0, 5]);

        /* LISTES 
         1. il faut appeler une librairie avant d'utiliser les liste = using System.Collections.Generic;
        2. Déclarer la liste et son type
        3. Creer une nouvelle liste = new list<int>() --> c'est une fonction donc il faut les parenthèse !
        4. lister les éléments
        5. On peut rajouter des éléments avec Add()
        6. Nombre d'élément de la liste count
        7. Supprimer un élément remove(élément)
            - RemoveAt(index) supprime à l'index
        8. insérer un élément Insert(index, élément)
        9. Supprimer toue la liste RemoveAll() / Clear()
         
         */

        List<int> listeChiffres;

        listeChiffres = new List<int>() {1,2,3,4,5 };
        Console.WriteLine("Le premier élément de ma liste est " + listeChiffres[0]);

        Console.WriteLine("Le nombre d'élément de la liste est  " + listeChiffres.Count);

        listeChiffres.Add(100);
        Console.WriteLine("Le dernier élément de ma liste est " + listeChiffres[5]);

        Console.WriteLine("Le nombre d'élément de la liste est  " + listeChiffres.Count);
        foreach (int chiffre in listeChiffres)
        {
            Console.WriteLine("Tableau original : " + chiffre);

        }

        listeChiffres.Insert(1, 300);
        listeChiffres.Remove(4);
        listeChiffres.RemoveAt(5);

        foreach (int chiffre in listeChiffres)
        {
            Console.WriteLine("Nouveau tableau " + chiffre);

        }


        List<string>  classesJoueur;
        classesJoueur = new List<string>()
        {"Humain",
        "Barbare",
        "Gnome",
        "Chien",

        };
        Console.WriteLine("Mes classes sont ");
        for (int i = 0; i< classesJoueur.Count; i++)
        {
            Console.WriteLine(classesJoueur.IndexOf(classesJoueur[i]) + " " + " " +  classesJoueur[i]);
        }


        /* STRUCTURE DE CONTROLE 
         1. Condition entre parenthèse
         2. Si condition == true le code entre {} est exécuté
        3. ET = &&
        4. OU ||
        5. else et else if ()  --> Il faut faire des {} à chaque fois et else if prend une condition
        6. SWITCH (condition) {case X : y break;} = selon la condition à tester dasn le cas ou c'est x je fait y
        On peut tester plusieurs cas en même temps.
        Ne pas oublier de mettre un valeur defaut pour voir que le test fonctionne bien
        */


        string[] classes;
        classes = new string[] { "Barbare", "Barde", "Ensorceleur", "Guerrier", "Mage" };
        string[] races = new string[] { "Humain", "Elfe", "Nain", "Gnome" };

        foreach (string laRace in races)
            {
            Console.WriteLine("Race : " + laRace);
        }

        Random rnd = new Random();

        int rndClasse = rnd.Next(0, classes.Length);
        Console.WriteLine("la classe sélectionneé est : "+classes[rndClasse]);

        if (rndClasse == 0 || rndClasse == 2)
        {
            Console.WriteLine("On est dans les 3 premières classes");
        }
        else if (rndClasse == 3)
        {
            Console.WriteLine(" 33333");
        }


        switch (rndClasse)
        {
            case 0:
                Console.WriteLine(" 111");
                break;
            case 1:
                Console.WriteLine(" 222");
                break;
            case 3:
            case 4:
                Console.WriteLine(" 33333");
                break;
          
            default:
                Console.WriteLine(" Autre cas");
                break;
        }

        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}

