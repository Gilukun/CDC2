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
        private ScreenManager _Resolution;

        ScenesManager CurrentLevel;
        ScenesManager Menu;
        ScenesManager Gameplay;
        

        public CasseBriques()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _screenManager = new ScreenManager(_graphics);
            ServiceLocator.RegisterService<ScreenManager>(_screenManager);
            _Resolution = ServiceLocator.GetService <ScreenManager>();
            _Resolution.ChangeResolution(1024, 700);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ServiceLocator.RegisterService<SpriteBatch>(_spriteBatch);
            ServiceLocator.RegisterService<ContentManager>(Content);
            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);

            Menu = new Menu(this);
            Gameplay = new Gameplay(this);
            CurrentLevel = Menu;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                CurrentLevel = Gameplay;
           }

            CurrentLevel.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            CurrentLevel.Draw();
            base.Draw(gameTime);
        }
    }
}