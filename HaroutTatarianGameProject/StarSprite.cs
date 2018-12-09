using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using PROG2370CollisionLibrary;
using System;
using System.Collections.Generic;

namespace HaroutTatarianGameProject
{
    public class StarSprite
    {
        public int CollectedStarsCount { get; set; }

        #region animation fields
        private const int shineStartFrame = 0;
        private const int shineEndFrame = 5;
        private const int shineFrameMaxDelay = 3;
        private const int shineAnimationMaxDelay = 200;
        private const int textureReducationScale = 25;

        private int shineFrameDelayCount = 0;
        private int shineAnimationDelayCount = 0;

        private int shineCurrentFrame = shineStartFrame;
        #endregion

        #region private fields
        private readonly Game game;
        private readonly SpriteBatch spriteBatch;
        private readonly List<Texture2D> spriteTextures;
        private readonly SpriteFont spriteFont = Game1.FontsManager.GetFont(Font.CourierNew40);
        private readonly Player player;
        private Rectangle spriteRectangle;
        #endregion


        public StarSprite(Game game, SpriteBatch spriteBatch, Player player)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.player = player;

            spriteTextures = new List<Texture2D>
            {
                game.Content.Load<Texture2D>("star coin 1"),
                game.Content.Load<Texture2D>("star coin 2"),
                game.Content.Load<Texture2D>("star coin 3"),
                game.Content.Load<Texture2D>("star coin 4"),
                game.Content.Load<Texture2D>("star coin 5"),
                game.Content.Load<Texture2D>("star coin 6")
            };

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            spriteRectangle = GetRandomSpriteRectangle(textureReducationScale);
        }

        public void Draw()
        {
            // Draw score at the top right corner
            spriteBatch.DrawString(spriteFont, "SCORE: " + CollectedStarsCount.ToString(),
                new Vector2(game.GraphicsDevice.DisplayMode.Width - spriteFont.MeasureString("SCORE: " + CollectedStarsCount.ToString()).X -10 , 10),
                Color.Chartreuse);
            // Draw the star sprite
            spriteBatch.Draw(spriteTextures[shineCurrentFrame], spriteRectangle, Color.Tan);
        }

        public void Update()
        {
            AnimateShine();

            // If star collides with player then change position randomly
            if (spriteRectangle.CheckCollisions(player.SpriteRectangle) != Sides.None)
            {
                spriteRectangle = GetRandomSpriteRectangle(textureReducationScale);
                CollectedStarsCount++;
                Game1.AudioManager.Play(Audio.StarCollected);
            }
        }

        /// <summary>
        /// Animates shine for the star
        /// </summary>
        private void AnimateShine()
        {
            if (shineAnimationDelayCount == shineAnimationMaxDelay)
            {
                if (shineFrameDelayCount == shineFrameMaxDelay)
                {
                    shineCurrentFrame++;
                    if (shineCurrentFrame > shineEndFrame)
                    {
                        shineCurrentFrame = shineStartFrame;
                        shineAnimationDelayCount = 0;
                    }
                    shineFrameDelayCount = 0;
                }
                else
                {
                    shineFrameDelayCount++;
                }
            }
            else
            {
                shineAnimationDelayCount++;
            }
        }
        
        /// <summary>
        /// Returns a rectangle on the screen
        /// </summary>
        /// <param name="scale"> scale to be divided by </param>
        /// <returns></returns>
        private Rectangle GetRandomSpriteRectangle(int scale)
        {
            Random randomAxis = new Random();
            int x = randomAxis.Next(0, game.GraphicsDevice.DisplayMode.Width - spriteTextures[shineCurrentFrame].Width / scale);
            int y = randomAxis.Next(0, game.GraphicsDevice.DisplayMode.Height - spriteTextures[shineCurrentFrame].Height / scale);
            return new Rectangle(x, y, spriteTextures[shineCurrentFrame].Width / scale, spriteTextures[shineCurrentFrame].Height / scale);
        }
    }
}
