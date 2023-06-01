using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Briques
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Briques maBriqueVie; // pas sûr d'avoir compris pourquoi ici ça doit être privé
        private Briques maBriquePower;
        private Briques maBriqueBasic;

        private List<Briques> listeBriques; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            listeBriques = new List<Briques>(); 

            maBriqueVie = new bHealth();
            //maBriqueVie.AssignerVie(10);
            //maBriqueVie.AddPoints();
            listeBriques.Add(maBriqueVie);

            maBriqueBasic = new BBasic();
            //maBriqueBasic.AssignerVie(20);
            //maBriqueBasic.AddPoints();
            listeBriques.Add(maBriqueBasic);
            maBriquePower = new bPower();
            //maBriquePower.AssignerVie(50);
            //maBriquePower.AddPoints();

            listeBriques.Add(maBriquePower);


            foreach (Briques item in listeBriques)
            {
                item.AssignerVie(20);
                item.Nommer(); 
                item.Tape(); 
                item.AddPoints(); 
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}