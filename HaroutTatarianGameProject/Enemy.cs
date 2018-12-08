using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PROG2370CollisionLibrary;
using System;

namespace HaroutTatarianGameProject
{
    public class Enemy : AlienSprite
    {
        private KeyboardState PreviousKeyboardState { get; set; }
        private Player player;
        private const float Speed = 4.165f;
        private bool isFrozen = false;
        
        protected override bool WasRunning { get; set; }
        protected override Point Velocity { get; set; }
        protected override bool IsIdle { get; set; }

        public Enemy(Game game, SpriteBatch spriteBatch, Color color, Player player, Vector2 spritePostion) : base(game, spriteBatch, color, AlienSize.Medium)
        {
            this.player = player;
            Velocity = new Point(0);
            IsIdle = false;
            WasRunning = false;
        }

        public override void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.F11))
            {
                isFrozen = !isFrozen;
            }

            if (!isFrozen)
            {
                Point tempVelocity = new Point(0);

                if (!NearlyEqual(player.SpriteRectangle.X, SpriteRectangle.X))
                {
                    // fix enemies not going on x axis
                    if (player.SpriteRectangle.X > SpriteRectangle.X)
                    {
                        tempVelocity = new Point((int)(Speed), tempVelocity.Y);
                    }
                    else if (player.SpriteRectangle.X < SpriteRectangle.X)
                    {
                        tempVelocity = new Point((int)(-Speed), tempVelocity.Y);
                    }
                }
                if (!NearlyEqual(player.SpriteRectangle.Y, SpriteRectangle.Y))
                {
                    if (player.SpriteRectangle.Y > SpriteRectangle.Y)
                    {
                        tempVelocity = new Point(tempVelocity.X, (int)(Speed));
                    }
                    else if (player.SpriteRectangle.Y < SpriteRectangle.Y)
                    {
                        tempVelocity = new Point(tempVelocity.X, (int)(-Speed));
                    }
                }

                Rectangle proposedLocation = new Rectangle(SpriteRectangle.X + tempVelocity.X,
                                                           SpriteRectangle.Y + tempVelocity.Y,
                                                            SpriteDimensions.X,
                                                            SpriteDimensions.Y);

                Sides collisionSides = proposedLocation.CheckCollisions(player.SpriteRectangle);

                if (collisionSides != Sides.None)
                {
                    Velocity = new Point(0);
                    SpriteRectangle = new Rectangle(SpriteRectangle.X + Velocity.X, SpriteRectangle.Y + Velocity.Y, SpriteDimensions.X, SpriteDimensions.Y);
                }
                else
                {
                    Velocity = tempVelocity;
                    SpriteRectangle = new Rectangle(SpriteRectangle.X + Velocity.X, SpriteRectangle.Y + Velocity.Y, SpriteDimensions.X, SpriteDimensions.Y);
                }

                base.Update();
            }
        }

        private bool NearlyOne(float f1)
        {
            return (Math.Abs(f1) < 1);
        }
        private bool NearlyEqual(float f1, float f2)
        {
            return NearlyOne(f1 - f2);
        }
    }
}
