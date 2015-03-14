using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SMUSS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameRoot : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GameRoot Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Camera camera;
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }
        public static ContentManager ContentManager { get; private set; }
        Matrix SpriteScale;

        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Instance = this;
            ContentManager = Content;
            ContentManager.RootDirectory = "Content";
        }



        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = Viewport.Height;
            graphics.PreferredBackBufferWidth = Viewport.Width;

            Level testLevel = new TestLevel();
            LevelManager.StartLevel(testLevel);

            base.Initialize();

            camera = new Camera(Viewport);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LevelManager.LoadContent(ContentManager);

            float screenscale = (float)graphics.GraphicsDevice.Viewport.Width / 800f;
            SpriteScale = Matrix.CreateScale(screenscale, screenscale, 1);

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            GameTime = gameTime;

            Input.Update();
            LevelManager.Update();

            camera.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            LevelManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
