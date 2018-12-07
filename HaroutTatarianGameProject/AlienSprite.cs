using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HaroutTatarianGameProject
{
    public enum AlienSize
    {
        Large = 2,
        Medium = 3,
        Small = 4
    }

    public abstract class AlienSprite
    {
        #region animation fields
        private const int idleFrameStart = 0;
        private const int idleFrameBlink = 1;
        private const int idleFrameEnd = 2;
        private const int idleFrameMaxDelay = 250;
        private const int runFrameStart = 6;
        private const int runFrameEnd = 11;
        private const int runFrameMaxDelay = 6;

        private int currentFrame = 0;
        private int currentFrameDelayCount = 0;
        private SpriteEffects spriteDirection = SpriteEffects.None;
        #endregion

        // Used to move and draw texture. Also, to determine collosion
        public Rectangle SpriteRectangle { get; set; }
        public Point SpriteDimensions { get; set; }  
        private readonly List<Texture2D> spriteTextures;
        private readonly SpriteBatch spriteBatch;  
        private readonly Color color;

        protected abstract bool IsIdle { get; set; }
        protected abstract bool WasRunning { get; set; }
        protected abstract Point Velocity { get; set; }

        public AlienSprite(Game game, SpriteBatch spriteBatch, Color color, AlienSize alienSize)
        {
            this.spriteBatch = spriteBatch;

            spriteTextures = new List<Texture2D>
            {
                // Load idle position resources
                game.Content.Load<Texture2D>("green__0000_idle_1"),
                game.Content.Load<Texture2D>("green__0001_idle_2"),
                game.Content.Load<Texture2D>("green__0002_idle_3"),

                // Load turning resources
                game.Content.Load<Texture2D>("green__0003_turn_1"),
                game.Content.Load<Texture2D>("green__0004_turn_2"),
                game.Content.Load<Texture2D>("green__0005_turn_3"),

                // Load running resources
                game.Content.Load<Texture2D>("green__0012_run_1"),
                game.Content.Load<Texture2D>("green__0013_run_2"),
                game.Content.Load<Texture2D>("green__0014_run_3"),
                game.Content.Load<Texture2D>("green__0015_run_4"),
                game.Content.Load<Texture2D>("green__0016_run_5"),
                game.Content.Load<Texture2D>("green__0017_run_6")
            };

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.color = color;

            SpriteDimensions = new Point(spriteTextures[0].Width /(int) alienSize, spriteTextures[0].Height /(int) alienSize);
            SpriteRectangle = new Rectangle(0, 0, SpriteDimensions.X, SpriteDimensions.Y);
        }

        public virtual void Draw()
        {
            spriteBatch.Draw(texture: spriteTextures[currentFrame],
                destinationRectangle: SpriteRectangle, 
                sourceRectangle: null, 
                color: color,
                rotation: 0f,
                origin: new Vector2(0,0),
                effects: spriteDirection,
                layerDepth: 0f);
        }

        public virtual void Update()
        {
            #region Idle Position Animation
            if (IsIdle)
            {
                // If was running in the previous frame
                if (WasRunning)
                {
                    currentFrameDelayCount = idleFrameMaxDelay;
                }

                if (currentFrameDelayCount > idleFrameMaxDelay || currentFrame == idleFrameBlink)
                {
                    currentFrameDelayCount = 0;
                    currentFrame = currentFrame >= idleFrameEnd ? idleFrameStart : ++currentFrame;
                }
                else
                {
                    currentFrameDelayCount++;
                }
            }
            #endregion
            #region moving animation
            else
            {
                if (Velocity.X != 0)
                {
                    // Change player direction based on velocity's x axis
                    spriteDirection = Velocity.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                    if (currentFrameDelayCount > runFrameMaxDelay)
                    {
                        currentFrameDelayCount = 0;
                        // If was idle and now running, make a turn animation and then run animation
                        // Otherwise, do the run animation
                        currentFrame = currentFrame != runFrameEnd ? ++currentFrame : runFrameStart;
                    }
                    else
                    {
                        currentFrameDelayCount++;
                    }
                }
                else if (Velocity.Y != 0)
                {
                    currentFrame = idleFrameEnd;
                }
            }
            #endregion
        }
    }
}
