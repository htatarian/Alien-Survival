using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace HaroutTatarianGameProject
{
    public class ActionScene
    {
        private string gameEndMessageLineOne = "";
        private string gameEndMessageLineTwo = "";

        private readonly SpriteFont gameEndFont;
        private readonly SpriteFont spriteFont;

        public static LevelStopWatch levelStopWatch;
        public static StarSprite star;
        public static HealthBar healthBar;

        private readonly Game game;

        private InitialInputWindow InitialInputWindow;
        private SpriteBatch spriteBatch;
        public Enemy enemy;

        bool isInitialEntered = false;
        bool isWinSoundPlayed = false;
        bool isThemeSongStopped = false;

        public Player player;
        Background background;

        public ActionScene (Game game, SpriteBatch spriteBatch)
        {
            // Stop music
            Game1.audioManager.Play(Audio.None);

            this.game = game;
            this.spriteBatch = spriteBatch;

            // Set background
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("action_scene_background");
            background = new Background(game, spriteBatch, backgroundTexture);

            // Set fonts
            gameEndFont = Game1.fontsManager.GetFont(Font.CourierNew160);
            spriteFont = Game1.fontsManager.GetFont(Font.CourierNew40);

            // Initilize fields
            player = new Player(game, spriteBatch);
            star = new StarSprite(game, spriteBatch, player);
            enemy = new Enemy(game, spriteBatch, Color.DarkRed, player, new Vector2(0, 0));
            InitialInputWindow = new InitialInputWindow(game, spriteBatch);
            healthBar = new HealthBar(game, spriteBatch);

            player.enemy = enemy;
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] keys = keyboardState.GetPressedKeys();

            if (!isInitialEntered)
            {
                InitialInputWindow.Update();
                if (keys.Contains(Keys.Enter) && !isInitialEntered && InitialInputWindow.Input.Length == 3)
                {
                    isInitialEntered = true;
                    levelStopWatch = new LevelStopWatch(game, spriteBatch);
                    Game1.audioManager.Play(Audio.ActionScene);
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
                }
                else
                {
                    // Stop background music
                    if (!isThemeSongStopped)
                    {
                        Game1.audioManager.Play(Audio.None);
                        isThemeSongStopped = true;
                    }

                    // Check if dead
                    if(healthBar.InnerRectangle.Width <= 0)
                    {
                        gameEndMessageLineOne = "You Lost";
                        gameEndMessageLineTwo = "Press ESC To Go Back To Main Menu";

                        if (!isWinSoundPlayed)
                        {
                            Game1.audioManager.Play(Audio.Lose);
                            isWinSoundPlayed = true;
                        }
                    }
                    // Check if survived
                    else if(!levelStopWatch.LevelTime.IsRunning)
                    {
                        gameEndMessageLineOne = "You Survived";
                        gameEndMessageLineTwo = "Press ESC To Go Back To Main Menu";
                        if (!isWinSoundPlayed)
                        {
                            Game1.audioManager.Play(Audio.Win);
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
            else
            {
                levelStopWatch.Draw();
                healthBar.Draw();
                star.Draw();
                enemy.Draw();
                player.Draw();

                if (healthBar.InnerRectangle.Width <= 0 || !levelStopWatch.LevelTime.IsRunning)
                {
                    spriteBatch.DrawString(gameEndFont, gameEndMessageLineOne,
                        new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - gameEndFont.MeasureString(gameEndMessageLineOne).X / 2,
                        game.GraphicsDevice.DisplayMode.Height / 2 - gameEndFont.MeasureString(gameEndMessageLineOne).Y / 2),
                        Color.Cornsilk);
                    spriteBatch.DrawString(spriteFont, gameEndMessageLineTwo,
                        new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - spriteFont.MeasureString(gameEndMessageLineTwo).X / 2,
                        game.GraphicsDevice.DisplayMode.Height / 2 - spriteFont.MeasureString(gameEndMessageLineTwo).Y / 2
                        + gameEndFont.MeasureString(gameEndMessageLineOne).Y),
                        Color.Cornsilk);
                }
            }
        }
    }
}
