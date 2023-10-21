using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;

namespace Preditor
{
    public class Host : Game
    {
        private GraphicsDeviceManager _graphics;
        private ImGuiRenderer _imGuiRenderer;
        //private SpriteBatch _spriteBatch;

        // Game Related
        public Starbrite _engine;
        public Editor _editor;

        public Host()
        {
            Log.Debug("Host startup");

            // gfx config
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferMultiSampling = true;
    
            // Preditor components
            _engine = new Starbrite();
            _editor = new Editor(_engine);

            // file content
            Content.RootDirectory = "Content";

            // ??
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 8);
        }

        protected override void Initialize()
        {
            // imgui config
            _imGuiRenderer = new ImGuiRenderer(this);
            _imGuiRenderer.RebuildFontAtlas();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            base.LoadContent();
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
            GraphicsDevice.Clear(Color.Black);

            // imgui
            _imGuiRenderer.BeforeLayout(gameTime);
            _editor.Draw(gameTime);
            _engine.Draw(gameTime);
            _imGuiRenderer.AfterLayout();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}