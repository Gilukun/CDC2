using ListeImages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net.Http.Headers;

namespace Liste_Image
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D img;

        //private Bubble image; 
        private BubbleList mesBulles; 
        private Vector2 position;
        private int lScreen;
        public int hScreen;
        private Bubble Bouge;
        private ScreenManager _ScreenManager; // on le déclare pour stocker 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _ScreenManager = new ScreenManager(_graphics);
            ServiceLocator.RegisterService<ScreenManager>(_ScreenManager);
           


            Content.RootDirectory = "Content";
            IsMouseVisible = true;
           
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ServiceLocator.RegisterService<SpriteBatch>(_spriteBatch); // ici on demande à notre service locator de récupérer le spriteBatch.
                                                                       // En faisant ça on peut suprimer la necessité de demander le spritebatch du game.
                                                                       // On doit quand même demander à nos instances de GetService pour savoir quel service ils doivent dmander

            ServiceLocator.RegisterService<ContentManager>(Content);
            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);

            img = Content.Load<Texture2D>("Bulle");
            //lScreen = GraphicsDevice.Viewport.Width; 
            //hScreen = GraphicsDevice.Viewport.Height;
            //image = new Bubble(Content, 100f,200f); 

            mesBulles = new BubbleList() ;
            //position = new Vector2(0, 0);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mesBulles.Move();
            mesBulles.Collisions();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            mesBulles.Affiche();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}