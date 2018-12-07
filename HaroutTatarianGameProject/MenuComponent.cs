using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HaroutTatarianGameProject
{
    public class MenuComponent
    {
        private const string title = "Alien Survival";

        public int SelectedIndex { get; set; }
        private readonly Color regularColor = Color.LightGreen;
        private readonly Color hilightColor = Color.GreenYellow;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont regularFont;
        private readonly SpriteFont hilightFont;
        private readonly SpriteFont titleFont;
        private readonly List<string> menuItems;
        private readonly Game game;
        private KeyboardState previousKeyboardState;

        public MenuComponent(Game game, SpriteBatch spriteBatch)
        {
            // Load items
            regularFont = game.Content.Load<SpriteFont>("Courier New");
            hilightFont = game.Content.Load<SpriteFont>("Courier New Bold");
            titleFont = game.Content.Load<SpriteFont>("Courier New Title");

            // Initilize class fields
            this.game = game;
            this.spriteBatch = spriteBatch;
            menuItems = new List<string> {"Start Game", "Leaderboard", "Credit", "Help", "Quit" };
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down) || (keyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S)))
            {
                Game1.audioManager.Play(Audio.SelectionChange);
                SelectedIndex++;
                if (SelectedIndex == menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up) || (keyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W)))
            {
                Game1.audioManager.Play(Audio.SelectionChange);
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void Draw()
        {        

            Vector2 tempPos = new Vector2(game.GraphicsDevice.DisplayMode.Width / 10, titleFont.MeasureString(title).Y + game.GraphicsDevice.DisplayMode.Height/ 8);

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i],
                        tempPos, hilightColor);
                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i],
                        tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.DrawString(titleFont, title,
                new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - titleFont.MeasureString(title).X/2f, 25),
                Color.Cornsilk);
        }
    }
}
