using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using C3.XNA;

namespace HaroutTatarianGameProject
{
    public class HelpScene
    {
        private const string title = "Help Menu";

        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont;
        private readonly SpriteFont titleFont;
        private readonly Background background;
        private readonly int textX;
        private readonly Game game;
        private readonly List<string> lines;
        private readonly Rectangle frame;
        private readonly Rectangle border;


        public HelpScene(Game game, SpriteBatch spriteBatch)
        {
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("start_scene_background");
            background = new Background(game, spriteBatch, backgroundTexture);
            this.spriteBatch = spriteBatch;
            this.game = game;
            lines = new List<string>();

            // Load items
            spriteFont = game.Content.Load<SpriteFont>("Courier New");
            titleFont = game.Content.Load<SpriteFont>("Courier New Title");

            //textY = (int)spriteFont.MeasureString("X").Y;
            textX = (int)spriteFont.MeasureString("XXXXXXXXXXXXXXXXXXXX").X;

            lines.Add("Objectives:");
            lines.Add("1. Survive");
            lines.Add("2. Collect the stars");
            lines.Add("");
            lines.Add("Controls:");
            lines.Add("W S A D To Move");
            lines.Add("");

            int centreX = game.GraphicsDevice.DisplayMode.Width / 2;
            int centreY = game.GraphicsDevice.DisplayMode.Height / 2;
            int frameWidth = game.GraphicsDevice.DisplayMode.Width - game.GraphicsDevice.DisplayMode.Width / 2;
            int frameHeight = game.GraphicsDevice.DisplayMode.Height - game.GraphicsDevice.DisplayMode.Height / 2;

            frame = new Rectangle(centreX - frameWidth / 2, centreY - frameHeight / 2, frameWidth, frameHeight);
            border = new Rectangle(centreX - frameWidth / 2, centreY - frameHeight / 2, frameWidth, frameHeight);
        }

        public void Draw()
        {
            background.Draw();
            spriteBatch.FillRectangle(frame, Color.Black, 0.8);
            spriteBatch.DrawRectangle(frame, Color.Black, 3f);
            for (int i = 0; i < lines.Count; i++)
            {
                spriteBatch.DrawString(spriteFont, lines[i],
                    new Vector2(game.GraphicsDevice.DisplayMode.Width/2 - textX/2,
                    (game.GraphicsDevice.DisplayMode.Height - spriteFont.LineSpacing * lines.Count) / 2 + spriteFont.LineSpacing * i),
                    Color.LightGreen);
            }
            spriteBatch.DrawString(titleFont, title,
                new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - titleFont.MeasureString(title).X / 2f, 25),
                Color.Cornsilk);
        }
    }
}
