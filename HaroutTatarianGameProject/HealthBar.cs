using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;
using Microsoft.Xna.Framework.Input;

namespace HaroutTatarianGameProject
{
    public class HealthBar
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Game game;
        private readonly Rectangle outerRectangle;
        private readonly int innerRectangleWidth;
        int innerRectangleHeight;
        int innerRectangleX;
        int innerRectangleY;

        public Rectangle InnerRectangle { get; private set; }

        public HealthBar(Game game,SpriteBatch spriteBatch)
        {
            this.game = game;

            this.spriteBatch = spriteBatch;

            // Define outer healthbar Rectangle
            int outerRectangleWidth = game.GraphicsDevice.DisplayMode.Width / 2;
            int outerRectangleHeight = game.GraphicsDevice.DisplayMode.Height / 30;
            int outerRectangleX = game.GraphicsDevice.DisplayMode.Width / 2 - outerRectangleWidth / 2;
            int outerRectangleY = game.GraphicsDevice.DisplayMode.Height - outerRectangleHeight * 2;
            outerRectangle = new Rectangle(outerRectangleX,outerRectangleY,outerRectangleWidth,outerRectangleHeight);

            // Define inner healthbar Rectangle
            innerRectangleWidth = outerRectangleWidth;
            innerRectangleHeight = outerRectangleHeight;
            innerRectangleX = outerRectangleX;
            innerRectangleY = outerRectangleY;
            InnerRectangle = new Rectangle(innerRectangleX, innerRectangleY, innerRectangleWidth, innerRectangleHeight);
        }

        public void Draw()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.F10))
            {
                InnerRectangle = new Rectangle(innerRectangleX, innerRectangleY, innerRectangleWidth, innerRectangleHeight);
            }

            spriteBatch.FillRectangle(InnerRectangle, Color.Red, 0.5);
            spriteBatch.DrawRectangle(outerRectangle, Color.Black, 3f);
        }

        public bool DescreaseHealth()
        {
            bool hasHealth = false;

            // Decrease inner healthbar rectangle
            InnerRectangle = new Rectangle(InnerRectangle.Location, new Point(InnerRectangle.Width - innerRectangleWidth / 100, InnerRectangle.Height));
            if (InnerRectangle.Width > 0)
            {
                hasHealth = true;
            }

            return hasHealth;
        }
    }
}
