using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace HaroutTatarianGameProject
{
    public class PopUp
    {
        #region protected readonly fields
        protected readonly Game game;
        protected readonly SpriteBatch spriteBatch;
        protected readonly SpriteFont subTitleFont = Game1.FontsManager.GetFont(Font.CourierNew40);
        protected readonly SpriteFont inputFont = Game1.FontsManager.GetFont(Font.CourierNew60);
        protected readonly Rectangle rectangle;
        #endregion

        public PopUp(Game game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            // Rectangle to pop up
            int width = game.GraphicsDevice.DisplayMode.Width / 2;
            int height = game.GraphicsDevice.DisplayMode.Height / 4;
            int x = (game.GraphicsDevice.DisplayMode.Width - width) / 2;
            int y = (game.GraphicsDevice.DisplayMode.Height - height) / 2;
            rectangle = new Rectangle(x, y, width, height);
        }

        public virtual void Draw()
        {
            // Display rectangle with a border
            spriteBatch.FillRectangle(rectangle, Color.Black, 0.5);
            spriteBatch.DrawRectangle(rectangle, Color.Black,3f);
        }
    }
}
