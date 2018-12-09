using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PROG2370CollisionLibrary;
using System;

namespace HaroutTatarianGameProject
{
    public class Enemy : AlienSprite
    {
        #region private fields
        private const float speed = 0.25f;
        private readonly Player player;
        private bool isFrozen = false;
        #endregion

        #region overriden abstract properties
        // The properties are required by base class to animate properly
        protected override bool WasRunning { get; set; } = false;
        protected override Point Velocity { get; set; } = new Point(0);
        protected override bool IsIdle { get; set; } = false;
        #endregion

        public Enemy(Game game, SpriteBatch spriteBatch, Color color, Player player) : base(game, spriteBatch, color, AlienSize.Medium)
        {
            this.player = player;
        }

        public override void Update()
        {
            #region movement logic
            KeyboardState keyboardState = Keyboard.GetState();

            // Freeze movement
            if (keyboardState.IsKeyDown(Keys.F11))
            {
                isFrozen = !isFrozen;
            }

            if (!isFrozen)
            {
                Point tempVelocity = new Point(0);

                // Horizontal movement logic
                if (!NearlyEqual(player.SpriteRectangle.X, SpriteRectangle.X))
                {
                    if (player.SpriteRectangle.X > SpriteRectangle.X)
                    {
                        tempVelocity = new Point((int)(speed * Game1.DeltaTime), tempVelocity.Y);
                    }
                    else if (player.SpriteRectangle.X < SpriteRectangle.X)
                    {
                        tempVelocity = new Point((int)(-speed * Game1.DeltaTime), tempVelocity.Y);
                    }
                }
                // Vertical movemnet logic
                if (!NearlyEqual(player.SpriteRectangle.Y, SpriteRectangle.Y))
                {
                    if (player.SpriteRectangle.Y > SpriteRectangle.Y)
                    {
                        tempVelocity = new Point(tempVelocity.X, (int)(speed * Game1.DeltaTime));
                    }
                    else if (player.SpriteRectangle.Y < SpriteRectangle.Y)
                    {
                        tempVelocity = new Point(tempVelocity.X, (int)(-speed * Game1.DeltaTime));
                    }
                }

                Rectangle nextPosition = new Rectangle(SpriteRectangle.X + tempVelocity.X,
                                                           SpriteRectangle.Y + tempVelocity.Y,
                                                            SpriteDimensions.X,
                                                            SpriteDimensions.Y);

                Sides collisionSides = nextPosition.CheckCollisions(player.SpriteRectangle);

                // If next position does not collide with player then move
                if (collisionSides == Sides.None)
                {
                    Velocity = tempVelocity;
                    SpriteRectangle = new Rectangle(SpriteRectangle.X + Velocity.X, SpriteRectangle.Y + Velocity.Y, SpriteDimensions.X, SpriteDimensions.Y);
                }

                base.Update();
            }
            #endregion
        }

        /// <summary>
        /// Check if a value 
        /// </summary>
        /// <param name="f1">value to be checked</param>
        /// <returns>True if value is nearly one, otherwise false</returns>
        private bool NearlyOne(float f1)
        {
            return (Math.Abs(f1) < 1);
        }

        /// <summary>
        /// Check if values are nearly equal
        /// </summary>
        /// <param name="f1">First value to be compared</param>
        /// <param name="f2">Second value to be compared</param>
        /// <returns>True if the different between the two values is almost one, otherwise false</returns>
        private bool NearlyEqual(float f1, float f2)
        {
            return NearlyOne(f1 - f2);
        }
    }
}
