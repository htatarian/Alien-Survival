using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace HaroutTatarianGameProject
{
    public class LevelStopWatch
    {
        public Stopwatch LevelTime { get; private set; } = new Stopwatch();

        #region private fields
        // Minutes to end the game at
        private const int maxGameMinutes = 2;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont = Game1.FontsManager.GetFont(Font.CourierNew40);
        private string countedownTime = "";
        #endregion

        public LevelStopWatch(Game game, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            LevelTime.Start();
        }

        public void Draw()
        {
            // Draw the time at top left corner
            spriteBatch.DrawString(spriteFont, countedownTime, new Vector2(10, 10), Color.Chartreuse);
        }

        public void Update()
        {
            // Covert stop watch time to countedown time based on the max game minutes
            int minutesLeft = 60 - LevelTime.Elapsed.Seconds == 60 ? maxGameMinutes - LevelTime.Elapsed.Minutes : maxGameMinutes - LevelTime.Elapsed.Minutes - 1;
            int secondsLeft = 60 - LevelTime.Elapsed.Seconds == 60 ? 0 : 60 - LevelTime.Elapsed.Seconds;

            countedownTime = minutesLeft.ToString() + ":" + secondsLeft.ToString();

            // If max time reached stop
            if (LevelTime.Elapsed.Minutes >= maxGameMinutes || LevelTime.Elapsed.Hours >= 1)
            {
                LevelTime.Stop();
            }
        }
    }
}