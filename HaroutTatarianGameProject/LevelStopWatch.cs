using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace HaroutTatarianGameProject
{
    public class LevelStopWatch
    {
        public Stopwatch LevelTime { get; }
        private const int maxGameMinutes = 10;
        private const string gameEndMessageLineOne = "You Won";
        private const string gameEndMessageLineTwo = "Press SPACE To Continue";
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont;
        //private readonly SpriteFont gameEndFont;
        private string timer = "";

        public LevelStopWatch(Game game, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            spriteFont = game.Content.Load<SpriteFont>("Courier New");
            //gameEndFont = game.Content.Load<SpriteFont>("Courier New Big");
            LevelTime = new Stopwatch();
            LevelTime.Start();
        }

        public void Draw()
        {
            spriteBatch.DrawString(spriteFont, timer, new Vector2(10, 10), Color.Chartreuse);

            //if (!LevelTime.IsRunning)
            //{
            //    spriteBatch.DrawString(gameEndFont, gameEndMessageLineOne,
            //        new Vector2(Game.GraphicsDevice.DisplayMode.Width / 2 - gameEndFont.MeasureString(gameEndMessageLineOne).X / 2,
            //        Game.GraphicsDevice.DisplayMode.Height / 2 - gameEndFont.MeasureString(gameEndMessageLineOne).Y / 2),
            //        Color.Cornsilk);
            //    spriteBatch.DrawString(spriteFont, gameEndMessageLineTwo,
            //        new Vector2(Game.GraphicsDevice.DisplayMode.Width / 2 - spriteFont.MeasureString(gameEndMessageLineTwo).X / 2,
            //        Game.GraphicsDevice.DisplayMode.Height / 2 - spriteFont.MeasureString(gameEndMessageLineTwo).Y / 2 
            //        + gameEndFont.MeasureString(gameEndMessageLineOne).Y),
            //        Color.Cornsilk);
            //}
        }

        public void Update()
        {        
            timer = LevelTime.Elapsed.Minutes.ToString() + ":" + LevelTime.Elapsed.Seconds.ToString();

            if (LevelTime.Elapsed.Seconds >= maxGameMinutes || LevelTime.Elapsed.Hours >= 1)
            {
                LevelTime.Stop();
            }
        }
    }
}