using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace HaroutTatarianGameProject
{
    public class PopUp
    {
        protected readonly Game game;
        protected readonly SpriteFont titleFont;
        protected readonly SpriteFont inputFont;
        protected readonly SpriteBatch spriteBatch;
        protected readonly Texture2D backgroundTexture;

        protected readonly int width;
        protected readonly int height;
        protected readonly int x;
        protected readonly int y;

        private readonly int borderWidth;
        private readonly int borderHeight;
        private readonly int borderX;
        private readonly int borderY;

        public PopUp(Game game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            titleFont = game.Content.Load<SpriteFont>("Courier New");
            inputFont = game.Content.Load<SpriteFont>("Courier New Extra Spacing");
            backgroundTexture = game.Content.Load<Texture2D>("input_background");

            width = game.GraphicsDevice.DisplayMode.Width / 2;
            height = game.GraphicsDevice.DisplayMode.Height / 4;
            x = (game.GraphicsDevice.DisplayMode.Width - width) / 2;
            y = (game.GraphicsDevice.DisplayMode.Height - height) / 2;

            borderWidth = width;
            borderHeight = height;
            borderX = (game.GraphicsDevice.DisplayMode.Width - borderWidth) / 2;
            borderY = (game.GraphicsDevice.DisplayMode.Height - borderHeight) / 2;
        }

        public virtual void Draw()
        {
            spriteBatch.FillRectangle(new Rectangle(x, y, width, height), Color.Black, 0.5);
            spriteBatch.DrawRectangle(new Rectangle(borderX, borderY, borderWidth, borderHeight), Color.Black,3f);
        }
    }
}
