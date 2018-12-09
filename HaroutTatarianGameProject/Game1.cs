using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HaroutTatarianGameProject
{
    public class Game1 : Game
    {
        #region public static fields
        public static AudioManager AudioManager { get; private set; }
        public static FontsManager FontsManager { get; private set; }
        public static float DeltaTime { get; set; }
        #endregion

        #region private variables
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;
        // Scenes
        private StartScene startScene;
        private ActionScene actionScene;
        private LeaderboardScene leaderboardScene;
        private CreditScene creditScene;
        private HelpScene helpScene;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content = new ResourceContentManager(Services, Resource1.ResourceManager);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initilize spriteBatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initilize managers
            AudioManager = new AudioManager(this);
            FontsManager = new FontsManager(this);

            // Enable full screen
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            // Prepare start scene
            AudioManager.Play(Audio.StartScene);
            startScene = new StartScene(this, spriteBatch);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Get milliseconds passed from the previous frame untill the current one
            // Used to provide same user expierance regardless of frame rate
            DeltaTime = gameTime.ElapsedGameTime.Milliseconds;

            if (actionScene != null) { actionScene.Update(); }
            if (startScene != null) { startScene.Update(); }

            KeyboardState keyboardState = Keyboard.GetState();

            // Selected menu
            int selectedIndex = 0;

            // Manage navigation logic and audio
            if (startScene != null)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    startScene = null;
                    actionScene = new ActionScene(this, spriteBatch);
                }
                else if (selectedIndex == 1 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    startScene = null;
                    leaderboardScene = new LeaderboardScene(this, spriteBatch);
                    AudioManager.Play(Audio.LeaderboardScene);
                }
                else if (selectedIndex == 2 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    startScene = null;
                    creditScene = new CreditScene(this, spriteBatch);
                    AudioManager.Play(Audio.CreditScene);
                }
                else if (selectedIndex == 3 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    startScene = null;
                    helpScene = new HelpScene(this, spriteBatch);
                }
                else if (selectedIndex == 4 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            else if (actionScene != null || leaderboardScene != null || creditScene != null || helpScene != null)
            {
                // Escape goes back to main menu
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    AudioManager.Play(Audio.StartScene);

                    actionScene = null;
                    leaderboardScene = null;
                    creditScene = null;
                    helpScene = null;
                    startScene = new StartScene(this, spriteBatch);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (actionScene != null) { actionScene.Draw(); }
            if (startScene != null) { startScene.Draw(); }
            if (leaderboardScene != null) { leaderboardScene.Draw(); }
            if (creditScene != null) { creditScene.Draw(); }
            if (helpScene != null) { helpScene.Draw(); }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
