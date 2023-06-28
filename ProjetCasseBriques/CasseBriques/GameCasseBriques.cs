using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace CasseBriques
{
    public class CasseBriques : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        ScreenManager _screenManager;
        ScreenManager _Resolution;
        GraphicsDevice GraphDevice;
        public GameTime gameTime = new GameTime();

        

        public GameState State;
        public int maxLevel;

        public CasseBriques()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            State = new GameState(this);
        }

        protected override void Initialize()
        {
            _screenManager = new ScreenManager(_graphics);
            ServiceLocator.RegisterService<ScreenManager>(_screenManager);
            _Resolution = ServiceLocator.GetService<ScreenManager>();
            _Resolution.ChangeResolution(900, 900);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ServiceLocator.RegisterService<SpriteBatch>(_spriteBatch);
            ServiceLocator.RegisterService<ContentManager>(Content);
            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);
            ServiceLocator.RegisterService<GameState>(State);

            HUD hud = new HUD(Content.Load<Texture2D>("HUD2"));
            ServiceLocator.RegisterService<HUD>(hud);

            Bullet bullet = new Bullet(Content.Load<Texture2D>("bFire"));
            ServiceLocator.RegisterService<Bullet>(bullet);

            AssetsManager AssetsManager = new AssetsManager();
            AssetsManager.Load();
            ServiceLocator.RegisterService<AssetsManager>(AssetsManager);

            ScenesManager Menu = new Menu();
            ScenesManager Gameplay = new Gameplay();
            
            State.ChangeScene(GameState.Scenes.Menu);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (State.CurrentScene != null)
            {
                State.CurrentScene.Update();
            }

            _Resolution.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (State.CurrentScene != null)
            {
                State.CurrentScene.Draw();
            }

            base.Draw(gameTime);
        }
    }
}