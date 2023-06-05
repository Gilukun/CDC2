using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Template.Template;

namespace Template
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch; // changer en public !
        public GameState gameState;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = new GameState(this);
        }

        protected override void Initialize()
        {
            gameState.ChangeScene(GameState.SceneType.Menu);
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetManager.Load(Content); // on appel directement 
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (gameState.CurrentScene != null)
            {
                gameState.CurrentScene.Update(gameTime);
                    }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (gameState.CurrentScene != null)
            {
                gameState.CurrentScene.Draw(gameTime);
            }

            _spriteBatch.End(); 
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}