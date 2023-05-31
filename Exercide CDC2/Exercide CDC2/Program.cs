
/*
1. Imaginer plusieurs types de brique
2. Créer une liste de brique
3. Pour chaque brique appeler la méthode "tape" qui affiche ce qu'il se passe en fonction de la brique. */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Exercide_CDC2
{
    public class Program : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Tank> listeTanks;

        public Program()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            /*listeTanks = new List<Tank>();

            NormalTank monNormalTank;
            monNormalTank = new NormalTank();
            monNormalTank.ChangePosition(10f, 10f);

            listeTanks.Add(monNormalTank);

            SuperTank monSuperTank = new SuperTank();

            listeTanks.Add(monSuperTank);

            foreach (Tank item in listeTanks)
            {
                item.Tire();
            }*/

            Briques briques = new Briques();

            

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