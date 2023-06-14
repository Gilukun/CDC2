using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace CasseBriques
{
    public class GameCasseBrique : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Scenes MaSceneCourante;
        Scenes SceneMenu;
        Scenes SceneGameplay;
        Scenes ScenesGameOver;

        public GameCasseBrique()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges(); 



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SceneMenu = new SceneMenu(this);
            SceneGameplay = new SceneGameplay(this);
            MaSceneCourante = SceneMenu;
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                MaSceneCourante = SceneGameplay;
            }

            MaSceneCourante.Update();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
          
            MaSceneCourante.Draw(_spriteBatch);
       
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}