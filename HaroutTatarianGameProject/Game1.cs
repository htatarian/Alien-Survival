﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HaroutTatarianGameProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static AudioManager audioManager;
        public static FontsManager fontsManager;
        public static float DeltaTime { get; set; }
        GraphicsDeviceManager graphics;

        // Declare all the scenes here
        private StartScene startScene;
        private ActionScene actionScene;
        private CreditScene creditScene;
        private HelpScene helpScene;
        private LeaderboardScene leaderboardScene;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Full screen mode
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            audioManager = new AudioManager(this);
            fontsManager = new FontsManager(this);
            startScene = new StartScene(this, spriteBatch);

            // Start the music
            audioManager.Play(Audio.StartScene);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            DeltaTime = gameTime.ElapsedGameTime.Milliseconds;

            if (actionScene != null) { actionScene.Update(); }
            if (startScene != null) { startScene.Update(); }

            int selectedIndex = 0;

            KeyboardState ks = Keyboard.GetState();

            if (startScene != null)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene = null;
                    actionScene = new ActionScene(this, spriteBatch);
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene = null;
                    leaderboardScene = new LeaderboardScene(this, spriteBatch);
                    audioManager.Play(Audio.LeaderboardScene);
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene = null;
                    creditScene = new CreditScene(this, spriteBatch);
                    audioManager.Play(Audio.CreditScene);
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene = null;
                    helpScene = new HelpScene(this, spriteBatch);
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            else if (actionScene != null || leaderboardScene != null || creditScene != null || helpScene != null)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    audioManager.Play(Audio.StartScene);

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
