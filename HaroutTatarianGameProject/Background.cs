using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HaroutTatarianGameProject
{
    public class Background
    {
        #region private readonly fields
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D backgroundTexture;
        private readonly Game game;
        #endregion

        public Background(Game game, SpriteBatch spriteBatch, Texture2D backgroundTexture)
        {
            // Set fields
            this.spriteBatch = spriteBatch;
            this.game = game;
            this.backgroundTexture = backgroundTexture;
        }

        public void Draw()
        {
            // Draw full screen background
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, game.GraphicsDevice.DisplayMode.Width, game.GraphicsDevice.DisplayMode.Height), Color.White);
        }
    }
}
