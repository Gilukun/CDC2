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

        private AssetsManager TitleFont;
        private AssetsManager MenuFont;
        AssetsManager AssetsManager = new AssetsManager();

        HUD HUD;

      
        public GameState State;

        ScenesManager Menu;
        ScenesManager Gameplay;

        private Level currentLevel;
        public int MaxLevel;

        public CasseBriques()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            State = new GameState(this);

        }

        protected override void Initialize()
        {
            _screenManager = new ScreenManager(_graphics);
            ServiceLocator.RegisterService<ScreenManager>(_screenManager);
            _Resolution = ServiceLocator.GetService<ScreenManager>();
            _Resolution.ChangeResolution(1024, 900);
            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);

            

            MaxLevel = 4;
            for (int i = 1; i <= MaxLevel; i++) // le nombre de niveau correspond au nombre max de Background (4) que j'ai. Si je met 4, la boucle 
            {
                Level level = new Level(i);
                level.RandomLevel();
                level.Save();

            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ServiceLocator.RegisterService<SpriteBatch>(_spriteBatch);
            ServiceLocator.RegisterService<ContentManager>(Content);
            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);
            ServiceLocator.RegisterService<GameState>(State);
            AssetsManager.Load();
            ServiceLocator.RegisterService<AssetsManager>(AssetsManager);
           
            Menu = new Menu();
            Gameplay = new Gameplay();
            
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