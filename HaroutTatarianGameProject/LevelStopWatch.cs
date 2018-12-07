using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace HaroutTatarianGameProject
{
    public class LevelStopWatch
    {
        public Stopwatch LevelTime { get; }
        private const int maxGameMinutes = 10;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont;
        private string timer = "";

        public LevelStopWatch(Game game, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            spriteFont = game.Content.Load<SpriteFont>("Courier New");
            LevelTime = new Stopwatch();
            LevelTime.Start();
        }

        public void Draw()
        {
            spriteBatch.DrawString(spriteFont, timer, new Vector2(10, 10), Color.Chartreuse);
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