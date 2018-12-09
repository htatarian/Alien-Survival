using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PROG2370CollisionLibrary;
using System.Linq;

namespace HaroutTatarianGameProject
{
    public class ActionScene
    {
        #region private fields
        private readonly Game game;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont gameEndFont;
        private readonly SpriteFont spriteFont;
        private readonly Background background;
        private readonly Enemy enemy;
        private readonly Player player;
        private readonly HealthBar healthBar;
        private readonly InitialInputWindow InitialInputWindow;
        private readonly StarSprite star;
        // End game messages
        private LevelStopWatch levelStopWatch;
        private string endGameMessageTitle = "";
        private string endGameMessageSubtitle = "";
        private string hint = "";
        // Status check fields
        private bool isGodMod = false;
        private bool isInitialEntered = false;
        private bool isWinSoundPlayed = false;
        private bool isThemeSongStopped = false;
        #endregion

        public ActionScene (Game game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            // Set background
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("action_scene_background");
            background = new Background(game, spriteBatch, backgroundTexture);

            // Set fonts
            gameEndFont = Game1.FontsManager.GetFont(Font.CourierNew160);
            spriteFont = Game1.FontsManager.GetFont(Font.CourierNew40);

            // Initilize fields
            player = new Player(game, spriteBatch);
            star = new StarSprite(game, spriteBatch, player);
            enemy = new Enemy(game, spriteBatch, Color.DarkRed, player);
            InitialInputWindow = new InitialInputWindow(game, spriteBatch);
            healthBar = new HealthBar(game, spriteBatch);

            // Stop the music until initials are entered
            Game1.AudioManager.Play(Audio.None);
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] keys = keyboardState.GetPressedKeys();

            if (!isInitialEntered)
            {
                InitialInputWindow.Update();

                // Check if valid initials are entered
                if (keys.Contains(Keys.Enter) && !isInitialEntered && InitialInputWindow.Input.Length == 3)
                {
                    isInitialEntered = true;
                    levelStopWatch = new LevelStopWatch(game, spriteBatch);
                    Game1.AudioManager.Play(Audio.ActionScene);
                }
            }
            else
            {
                if (levelStopWatch.LevelTime.IsRunning && healthBar.InnerRectangle.Width > 0)
                {
                    player.Update();
                    enemy.Update();
                    star.Update();
                    levelStopWatch.Update();

                    // Change inputs after a minute passes. Also, display a hint
                    if (levelStopWatch.LevelTime.Elapsed.Minutes >= 1)
                    {
                        hint = "*Feeling Dizzy*";
                        player.MovmentKeys = new Keys[] { Keys.A, Keys.Left, Keys.D, Keys.Right, Keys.W, Keys.Up, Keys.S, Keys.Down };
                    }

                    #region player enemy collision logic
                    // Enable god mode
                    if (keyboardState.IsKeyDown(Keys.F12))
                    {
                        isGodMod = !isGodMod;
                    }

                    if (!isGodMod)
                    {
                        // Slightly bigger collosion box than the player's for better collosion
                        Rectangle outerPlayerRec = new Rectangle(player.SpriteRectangle.X - 5, player.SpriteRectangle.Y - 5, player.SpriteRectangle.Width + 10, player.SpriteRectangle.Height + 10);
                        Sides collisionSides = outerPlayerRec.CheckCollisions(enemy.SpriteRectangle);

                        if (collisionSides != Sides.None)
                        {
                            healthBar.DescreaseHealth();
                            Game1.AudioManager.Play(Audio.Hurt);
                        }
                    }
                    #endregion
                }
                else
                {
                    // Stop background music
                    if (!isThemeSongStopped)
                    {
                        Game1.AudioManager.Play(Audio.None);
                        isThemeSongStopped = true;
                    }

                    // Check if dead
                    if(healthBar.InnerRectangle.Width <= 0)
                    {
                        endGameMessageTitle = "You Lost";
                        endGameMessageSubtitle = "Press ESC To Go Back To Main Menu";

                        if (!isWinSoundPlayed)
                        {
                            Game1.AudioManager.Play(Audio.Lose);
                            isWinSoundPlayed = true;
                        }
                    }
                    // Check if survived
                    else if(!levelStopWatch.LevelTime.IsRunning)
                    {
                        endGameMessageTitle = "You Survived";
                        endGameMessageSubtitle = "Press ESC To Go Back To Main Menu";
                        if (!isWinSoundPlayed)
                        {
                            Game1.AudioManager.Play(Audio.Win);
                            isWinSoundPlayed = true;
                        }

                        // If qualifies, add to leaderboard
                        Leaderboard leaderBoard = new Leaderboard("leaderboard");
                        leaderBoard.Add(new Score(InitialInputWindow.Input, star.CollectedStarsCount));
                    }
                }
            }
        }

        public void Draw()
        {
            background.Draw();

            if (!isInitialEntered)
            {
                InitialInputWindow.Draw();
            }
            // Simulate the game
            else
            {
                levelStopWatch.Draw();
                healthBar.Draw();
                star.Draw();
                enemy.Draw();
                player.Draw();

                // Draw hint
                spriteBatch.DrawString(spriteFont, hint,
                    new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - spriteFont.MeasureString(hint).X / 2f, 25),
                    Color.Cornsilk);
                
                // If dead or survived
                if (healthBar.InnerRectangle.Width <= 0 || !levelStopWatch.LevelTime.IsRunning)
                {
                    // Game end title
                    spriteBatch.DrawString(gameEndFont, endGameMessageTitle,
                        new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - gameEndFont.MeasureString(endGameMessageTitle).X / 2,
                        game.GraphicsDevice.DisplayMode.Height / 2 - gameEndFont.MeasureString(endGameMessageTitle).Y / 2),
                        Color.Cornsilk);
                    // Game end subtitle
                    spriteBatch.DrawString(spriteFont, endGameMessageSubtitle,
                        new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - spriteFont.MeasureString(endGameMessageSubtitle).X / 2,
                        game.GraphicsDevice.DisplayMode.Height / 2 - spriteFont.MeasureString(endGameMessageSubtitle).Y / 2
                        + gameEndFont.MeasureString(endGameMessageTitle).Y),
                        Color.Cornsilk);
                }
            }
        }
    }
}
