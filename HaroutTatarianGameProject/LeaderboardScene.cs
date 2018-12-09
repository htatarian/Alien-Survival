using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HaroutTatarianGameProject
{
    public class LeaderboardScene
    {
        #region private fields
        private const string title = "Leaderboard";
        private readonly Game game;
        private readonly SpriteBatch spriteBatch;
        private readonly Leaderboard leaderboard = new Leaderboard("leaderboard");
        private readonly SpriteFont spriteFont = Game1.FontsManager.GetFont(Font.CourierNew40);
        private readonly SpriteFont titleFont = Game1.FontsManager.GetFont(Font.CourierNew120);
        private readonly Background background;
        private readonly int startingPositionY;
        private readonly int centreX;
        private readonly int maxTextWidth;
        #endregion

        public LeaderboardScene(Game game, SpriteBatch spriteBatch)
        {
            // Initilize and set background
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("leaderboard_scene_background");
            background = new Background(game, spriteBatch, backgroundTexture);

            // Initilize fields
            this.game = game;
            this.spriteBatch = spriteBatch;

            // Initilize screen centre
            startingPositionY = (int) titleFont.MeasureString(title).Y * 2;
            centreX = game.GraphicsDevice.DisplayMode.Width / 2;
            maxTextWidth = (int) spriteFont.MeasureString("X. XXX   X").X;
        }

        public void Draw()
        {
            // Draw background
            background.Draw();

            // Display each score on a line
            for(int i = 0; i < leaderboard.ScoreList.Count; i++)
            {
                string text = (i+1) + ". " + leaderboard.ScoreList[i].Name + "   " + leaderboard.ScoreList[i].Points;
                spriteBatch.DrawString(spriteFont, text, 
                    new Vector2(centreX - maxTextWidth/2,
                    startingPositionY + spriteFont.LineSpacing * i),
                    Color.GreenYellow);
            }

            spriteBatch.DrawString(titleFont, title,
                 new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - titleFont.MeasureString(title).X / 2f, 25), Color.Cornsilk);
        }
    }
}
