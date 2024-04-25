using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using System.Drawing;

namespace CasseBrique
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public SceneManager SceneMana;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ServiceLocator.RegisterService<ContentManager>(Content);
            ServiceLocator.RegisterService<GameWindow>(Window);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ServiceLocator.RegisterService<SpriteBatch>(_spriteBatch);

            // TODO: use this.Content to load your game content here

            // On charge la scène menu

            SceneMana = new SceneManager();
            ServiceLocator.RegisterService<SceneManager>(SceneMana);

            SceneMana.AddScenes(new SceneMenu());
            SceneMana.AddScenes(new SceneGameplay());
            SceneMana.AddScenes(new SceneGameOver());
            SceneMana.AddScenes(new SceneDeWin());

            SceneMana.ChangeScene(0);
            SceneMana.Load();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            SceneMana.Update();
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            SceneMana.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
