using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace HaroutTatarianGameProject
{
    public class Player : AlienSprite
    {
        #region public properties
        // Movement keys
        public Keys[] MovmentKeys { private get; set; } = 
            new Keys[] { Keys.D, Keys.Right, Keys.A, Keys.Left, Keys.W, Keys.Up, Keys.S, Keys.Down };
        #endregion

        #region private fields
        private const float speed = 0.42f;
        private readonly Game game;
        private KeyboardState previousKeyboardState;
        #endregion

        #region overriden abstract properties
        // The properties are required by base class to animate properly
        protected override bool IsIdle { get; set; } = true;
        protected override bool WasRunning { get; set; } = false;
        protected override Point Velocity { get; set; } = new Point(0);
        #endregion

        public Player(Game game, SpriteBatch spriteBatch) : base(game,spriteBatch,Color.White,AlienSize.Small)
        {
            this.game = game;
            SpriteRectangle = new Rectangle(game.GraphicsDevice.DisplayMode.Width/2, game.GraphicsDevice.DisplayMode.Height/2,SpriteDimensions.X,SpriteDimensions.Y);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            #region movement logic
            KeyboardState keyboardState = Keyboard.GetState();

            // Stop movement
            Velocity = new Point(0);

            // Check if not moving
            IsIdle = keyboardState.GetPressedKeys().Where(k => k == Keys.A | k == Keys.D | k == Keys.W | k == Keys.S
                        | k == Keys.Right | k == Keys.Left | k == Keys.Down | k == Keys.Up).Count() == 0;

            // Check if was running in the previous frame
            WasRunning = previousKeyboardState.GetPressedKeys().Where(k => k == Keys.A | k == Keys.D | k == Keys.W | k == Keys.S
                        | k == Keys.Right | k == Keys.Left | k == Keys.Down | k == Keys.Up).Count() != 0;

            if (!IsIdle)
            {
                // Move left
                if (keyboardState.IsKeyDown(MovmentKeys[0]) || keyboardState.IsKeyDown(MovmentKeys[1]))
                {
                    if (SpriteRectangle.X < game.GraphicsDevice.DisplayMode.Width - SpriteDimensions.X)
                    {
                        Velocity = new Point((int)(speed * Game1.DeltaTime), Velocity.Y);
                    }
                }
                // Move right
                else if (keyboardState.IsKeyDown(MovmentKeys[2]) || keyboardState.IsKeyDown(MovmentKeys[3]))
                {
                    if (SpriteRectangle.X > 0)
                    {
                        Velocity = new Point((int)(-speed * Game1.DeltaTime), Velocity.Y);
                    }
                }
                // Move Up
                else if (keyboardState.IsKeyDown(MovmentKeys[4]) || keyboardState.IsKeyDown(MovmentKeys[5]))
                {
                    if (SpriteRectangle.Y > 0)
                    {
                        Velocity = new Point(Velocity.X, (int)(-speed * Game1.DeltaTime));
                    }
                }
                // Move Down
                else if (keyboardState.IsKeyDown(MovmentKeys[6]) || keyboardState.IsKeyDown(MovmentKeys[7]))
                {
                    if (SpriteRectangle.Y < game.GraphicsDevice.DisplayMode.Height - SpriteDimensions.Y)
                    {
                        Velocity = new Point(Velocity.X, (int)(speed * Game1.DeltaTime));
                    }
                }
            }

            SpriteRectangle = new Rectangle(SpriteRectangle.X + Velocity.X, SpriteRectangle.Y + Velocity.Y, SpriteDimensions.X, SpriteDimensions.Y);

            previousKeyboardState = keyboardState;
            #endregion

            base.Update();
        }
    }
}