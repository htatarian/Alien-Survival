﻿using Microsoft.Xna.Framework;
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
        private const float Speed = 0.42f;
        protected override bool IsIdle { get; set; }
        protected override bool WasRunning { get; set; }
        protected override Point Velocity { get; set; }
        private bool isGodMod = false;

        public Player(Game game, SpriteBatch spriteBatch) : base(game,spriteBatch,Color.White,AlienSize.Small)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            spriteFont = Game1.fontsManager.GetFont(Font.CourierNew60);
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
            Keys[] keys = { Keys.D, Keys.Right, Keys.A, Keys.Left, Keys.W, Keys.Up, Keys.S, Keys.Down};

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
                keys = new Keys[] { Keys.A, Keys.Left, Keys.D, Keys.Right, Keys.W, Keys.Up, Keys.S, Keys.Down };
                hint = "*Feeling Dizzy*";
            }

            if (!IsIdle)
            {
                if (keyboardState.IsKeyDown(keys[0]) || keyboardState.IsKeyDown(keys[1]))
                {
                    if (SpriteRectangle.X < game.GraphicsDevice.DisplayMode.Width - SpriteDimensions.X)
                    {
                        Velocity = new Point((int)(Speed * Game1.DeltaTime), Velocity.Y);
                    }
                }
                else if (keyboardState.IsKeyDown(keys[2]) || keyboardState.IsKeyDown(keys[3]))
                {
                    if (SpriteRectangle.X > 0)
                    {
                        Velocity = new Point((int)(-Speed * Game1.DeltaTime), Velocity.Y);
                    }
                }
                else if (keyboardState.IsKeyDown(keys[4]) || keyboardState.IsKeyDown(keys[5]))
                {
                    if (SpriteRectangle.Y > 0)
                    {
                        Velocity = new Point(Velocity.X, (int)(-Speed * Game1.DeltaTime));
                    }
                }
                else if (keyboardState.IsKeyDown(keys[6]) || keyboardState.IsKeyDown(keys[7]))
                {
                    if (SpriteRectangle.Y < game.GraphicsDevice.DisplayMode.Height - SpriteDimensions.Y)
                    {
                        Velocity = new Point(Velocity.X, (int)(Speed * Game1.DeltaTime));
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