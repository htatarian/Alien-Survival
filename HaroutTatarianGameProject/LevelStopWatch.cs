using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace HaroutTatarianGameProject
{
    public class LevelStopWatch
    {
        public Stopwatch LevelTime { get; }
        private const int maxGameMinutes = 2;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont;
        private string timer = "";

        public LevelStopWatch(Game game, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            spriteFont = Game1.fontsManager.GetFont(Font.CourierNew40);
            LevelTime = new Stopwatch();
            LevelTime.Start();
        }

        public void Draw()
        {
            spriteBatch.DrawString(spriteFont, timer, new Vector2(10, 10), Color.Chartreuse);
        }

        public void Update()
        {
            int minutesLeft = 60 - LevelTime.Elapsed.Seconds == 60 ? maxGameMinutes - LevelTime.Elapsed.Minutes : maxGameMinutes - LevelTime.Elapsed.Minutes - 1;
            int secondsLeft = 60 - LevelTime.Elapsed.Seconds == 60 ? 0 : 60 - LevelTime.Elapsed.Seconds;

            timer = minutesLeft.ToString() + ":" + secondsLeft.ToString();

            if (LevelTime.Elapsed.Minutes >= maxGameMinutes || LevelTime.Elapsed.Hours >= 1)
            {
                LevelTime.Stop();
            }
        }
    }
}