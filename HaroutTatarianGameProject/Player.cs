using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PROG2370CollisionLibrary;
using System.Linq;

namespace HaroutTatarianGameProject
{
    public class Player : AlienSprite
    {
        public Enemy enemy;

        private readonly Game game;

        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont;
        private string hint = "";

        private KeyboardState previousKeyboardState;
        private const float Speed = 7f;
        protected override bool IsIdle { get; set; }
        protected override bool WasRunning { get; set; }
        protected override Point Velocity { get; set; }
        private bool isGodMod = false;

        public Player(Game game, SpriteBatch spriteBatch) : base(game,spriteBatch,Color.White,AlienSize.Small)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            spriteFont = game.Content.Load<SpriteFont>("Courier New Bold");
            Velocity = new Point(0);
            IsIdle = true;
            WasRunning = false;
            SpriteRectangle = new Rectangle(game.GraphicsDevice.DisplayMode.Width/2, game.GraphicsDevice.DisplayMode.Height/2,SpriteDimensions.X,SpriteDimensions.Y);

            Vector2 tempPos = new Vector2(game.GraphicsDevice.DisplayMode.Width / 10, spriteFont.MeasureString(hint).Y + game.GraphicsDevice.DisplayMode.Height / 8);
        }

        public override void Draw()
        {
            spriteBatch.DrawString(spriteFont, hint,
            new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - spriteFont.MeasureString(hint).X / 2f, 25),
            Color.Cornsilk);
            base.Draw();
        }

        public override void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] keys = { Keys.D, Keys.A, Keys.W, Keys.S };

            if (keyboardState.IsKeyDown(Keys.F12))
            {
                isGodMod = !isGodMod;
            }

                // Stop the player
                Velocity = new Point(0);

            IsIdle = keyboardState.GetPressedKeys().Where(k => k == Keys.A | k == Keys.D | k == Keys.W | k == Keys.S
            | k == Keys.Right | k == Keys.Left | k == Keys.Down | k == Keys.Up).Count() == 0;

            WasRunning = previousKeyboardState.GetPressedKeys().Where(k => k == Keys.A | k == Keys.D | k == Keys.W | k == Keys.S
                        | k == Keys.Right | k == Keys.Left | k == Keys.Down | k == Keys.Up).Count() != 0;
            
            if (ActionScene.levelStopWatch.LevelTime.Elapsed.Minutes >= 1)
            {
                keys = new Keys[] { Keys.A, Keys.D, Keys.W, Keys.S };
                hint = "*Feeling Dizzy*";
            }

            if (!IsIdle)
            {
                if (keyboardState.IsKeyDown(keys[0]))
                {
                    if (SpriteRectangle.X < game.GraphicsDevice.DisplayMode.Width - SpriteDimensions.X)
                    {
                        Velocity = new Point((int)(Speed), Velocity.Y);
                    }
                }
                else if (keyboardState.IsKeyDown(keys[1]))
                {
                    if (SpriteRectangle.X > 0)
                    {
                        Velocity = new Point((int)(-Speed), Velocity.Y);
                    }
                }
                else if (keyboardState.IsKeyDown(keys[2]))
                {
                    if (SpriteRectangle.Y > 0)
                    {
                        Velocity = new Point(Velocity.X, (int)(-Speed));
                    }
                }
                else if (keyboardState.IsKeyDown(keys[3]))
                {
                    if (SpriteRectangle.Y < game.GraphicsDevice.DisplayMode.Height - SpriteDimensions.Y)
                    {
                        Velocity = new Point(Velocity.X, (int)(Speed));
                    }
                }
            }

            if (enemy != null && !isGodMod)
            {
                Rectangle outerPlayerRec = new Rectangle(SpriteRectangle.X - 5, SpriteRectangle.Y - 5, SpriteRectangle.Width + 10, SpriteRectangle.Height + 10);
                Sides collisionSides = outerPlayerRec.CheckCollisions(enemy.SpriteRectangle);

                if (collisionSides != Sides.None)
                {
                    ActionScene.healthBar.DescreaseHealth();
                    Game1.audioManager.Play(Audio.Hurt);
                }
            }

            SpriteRectangle = new Rectangle(SpriteRectangle.X + Velocity.X, SpriteRectangle.Y + Velocity.Y, SpriteDimensions.X, SpriteDimensions.Y);
            previousKeyboardState = keyboardState;
            base.Update();
        }
    }
}