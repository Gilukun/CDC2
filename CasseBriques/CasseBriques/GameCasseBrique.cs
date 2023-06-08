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

        private Texture2D pad; 
        private int pad_x; 
        private int pad_y;

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
            pad_x = 10;
            pad_y = 20;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pad = Content.Load<Texture2D>("raquette");
            SceneMenu = new SceneMenu();
            SceneGameplay = new SceneGameplay();
            MaSceneCourante = SceneMenu;
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                pad_x++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                pad_x--;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            MaSceneCourante.Draw();
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}