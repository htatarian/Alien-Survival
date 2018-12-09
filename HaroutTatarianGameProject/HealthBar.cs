using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;
using Microsoft.Xna.Framework.Input;

namespace HaroutTatarianGameProject
{
    public class HealthBar
    {
        // This is the player's "health"
        public Rectangle InnerRectangle { get; set; }

        #region private fields
        private readonly SpriteBatch spriteBatch;
        private readonly Rectangle outerRectangle;
        #endregion

        public HealthBar(Game game,SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            // Define outer healthbar Rectangle
            int outerRectangleWidth = game.GraphicsDevice.DisplayMode.Width / 2;
            int outerRectangleHeight = game.GraphicsDevice.DisplayMode.Height / 30;
            int outerRectangleX = game.GraphicsDevice.DisplayMode.Width / 2 - outerRectangleWidth / 2;
            int outerRectangleY = game.GraphicsDevice.DisplayMode.Height - outerRectangleHeight * 2;
            InnerRectangle = new Rectangle(outerRectangleX,outerRectangleY,outerRectangleWidth,outerRectangleHeight);
            outerRectangle = InnerRectangle;
        }

        public void Draw()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // Fill healthbar "cheat"
            if (keyboardState.IsKeyDown(Keys.F10))
            {
                InnerRectangle = new Rectangle(InnerRectangle.X, InnerRectangle.Y, outerRectangle.Width, InnerRectangle.Height);
            }

            // Bottom black layer
            spriteBatch.FillRectangle(outerRectangle, Color.Black);
            // Middle red layer
            spriteBatch.FillRectangle(InnerRectangle, Color.Red, 0.7);
            // Top border layer
            spriteBatch.DrawRectangle(outerRectangle, Color.Black, 5f);
        }

        public bool DescreaseHealth()
        {
            bool hasHealth = false;

            // Decrease inner healthbar rectangle
            InnerRectangle = new Rectangle(InnerRectangle.Location, new Point(InnerRectangle.Width - InnerRectangle.Width / 100, InnerRectangle.Height));
            if (InnerRectangle.Width > 0)
            {
                hasHealth = true;
            }

            return hasHealth;
        }
    }
}
