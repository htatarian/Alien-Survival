using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HaroutTatarianGameProject
{
    public class LeaderboardScene
    {
        private const string title = "Leaderboard";
        private readonly int textX;
        private readonly Game game;
        private readonly SpriteBatch spriteBatch;
        private readonly Leaderboard leaderboard;
        private readonly SpriteFont spriteFont;
        private readonly SpriteFont titleFont;
        private readonly Background background;
        private readonly int startingPositionY;
        private readonly int centreX;

        public LeaderboardScene(Game game, SpriteBatch spriteBatch)
        {
            // Initilize background
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("leaderboard_scene_background");
            background = new Background(game, spriteBatch, backgroundTexture);

            // Load fonts
            spriteFont = Game1.fontsManager.GetFont(Font.CourierNew40);
            titleFont = Game1.fontsManager.GetFont(Font.CourierNew120);

            // Initilize fields
            this.game = game;
            this.spriteBatch = spriteBatch;
            leaderboard = new Leaderboard("leaderboard");

            // Initilize screen centre
            startingPositionY = (int) titleFont.MeasureString(title).Y * 2;
            centreX = game.GraphicsDevice.DisplayMode.Width / 2;
            textX = (int) spriteFont.MeasureString("X. XXX   X").X;
        }

        public void Draw()
        {
            background.Draw();
            for(int i = 0; i < leaderboard.ScoreList.Count; i++)
            {
                string text = (i+1) + ". " + leaderboard.ScoreList[i].Name + "   " + leaderboard.ScoreList[i].Points;
                spriteBatch.DrawString(spriteFont, text, 
                    new Vector2(centreX - textX/2,
                    startingPositionY + spriteFont.LineSpacing * i),
                    Color.GreenYellow);
            }

            spriteBatch.DrawString(titleFont, title,
                 new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - titleFont.MeasureString(title).X / 2f, 25), Color.Cornsilk);
        }
    }
}
