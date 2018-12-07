using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HaroutTatarianGameProject
{
    public class Background
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D backgroundTexture;
        private readonly Game game;

        public Background(Game game, SpriteBatch spriteBatch, Texture2D backgroundTexture)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            this.backgroundTexture = backgroundTexture;
        }

        public void Draw()
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, game.GraphicsDevice.DisplayMode.Width, game.GraphicsDevice.DisplayMode.Height), Color.White);
        }
    }
}
