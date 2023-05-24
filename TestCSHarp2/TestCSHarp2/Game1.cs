using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestCSHarp2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

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
        nEnergie --;
        System.Console.WriteLine(nEnergie);
        nEnergie ++;
        System.Console.WriteLine(nEnergie);
        nEnergie += 10; /* marche avec (+ - * /) */
        System.Console.WriteLine(nEnergie);

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

