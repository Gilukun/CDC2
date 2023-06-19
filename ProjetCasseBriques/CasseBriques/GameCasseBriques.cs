using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CasseBriques
{
    public class CasseBriques : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        ScreenManager _screenManager;
        ScreenManager _Resolution;

        private AssetsManager TitleFont;
        private AssetsManager MenuFont;
        AssetsManager AssetsManager = new AssetsManager();


        public GameState gameState;
        ScenesManager Menu;
        ScenesManager Gameplay;
       

        public CasseBriques()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = new GameState(this);

        }

        protected override void Initialize()
        {
            _screenManager = new ScreenManager(_graphics);
            ServiceLocator.RegisterService<ScreenManager>(_screenManager);
            _Resolution = ServiceLocator.GetService<ScreenManager>();
            _Resolution.ChangeResolution(1024, 800);


            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ServiceLocator.RegisterService<SpriteBatch>(_spriteBatch);
            ServiceLocator.RegisterService<ContentManager>(Content);
            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);

            AssetsManager.Load();
            ServiceLocator.RegisterService<AssetsManager>(AssetsManager);
            //AssetsManager.Load();

            Menu = new Menu(this);
            Gameplay = new Gameplay(this);
           
            gameState.ChangeScene(GameState.Scenes.Menu);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (gameState.CurrentScene != null)
            {
                gameState.CurrentScene.Update();
            }
           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (gameState.CurrentScene != null)
            {
                gameState.CurrentScene.Draw();
            }
           
            base.Draw(gameTime);
        }
    }
}