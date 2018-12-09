using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaroutTatarianGameProject
{
    public class CreditScene
    {
        #region private fields
        private const string theEndStr = "The End";
        private readonly Game game;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont = Game1.FontsManager.GetFont(Font.CourierNew40);
        private readonly SpriteFont theEndFont = Game1.FontsManager.GetFont(Font.CourierNew120);
        private readonly List<string> lines = new List<string>();
        private float scrollUp = 0f;
        #endregion

        public CreditScene(Game game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            #region lines to be displayed
            lines.Add("");
            lines.Add("");
            lines.Add("");
            lines.Add("");
            lines.Add("CREDITS");
            lines.Add("");
            lines.Add("");
            lines.Add("HAROUT TATARIAN");
            lines.Add("Developer");
            lines.Add("");
            lines.Add("");
            lines.Add("DR. STEVE HENDRIKSE");
            lines.Add("Instructor");
            lines.Add("");
            lines.Add("");
            lines.Add("LIBRARIES");
            lines.Add("");
            lines.Add("Pritives2D by John McDonald");
            lines.Add("Collisions by Steve Hendrikse");
            lines.Add("");
            lines.Add("");
            lines.Add("PICTURES & SKINS");
            lines.Add("");
            lines.Add("Start Scene by Redwallpapers");
            lines.Add("Alien Skin by Craftpix");
            lines.Add("Leaderboard by Matkojedyna");
            lines.Add("Action Scene by Derpibooru");
            lines.Add("Star Coins by DontMind8");
            lines.Add("");
            lines.Add("MUSIC & SOUND EFFECTS");
            lines.Add("");
            lines.Add("Menu Theme by Haydensayshi123");
            lines.Add("Main Theme by Mikobuntu");
            lines.Add("Leaderboard Theme by ShadyDave");
            lines.Add("Credits Theme by Oymaldonado");
            lines.Add("Win SFX by Jimhancock");
            lines.Add("Game SFX End by Tuudurt");
            lines.Add("Lose SFX by Cabledmess");
            lines.Add("Hurt SFX by Michel88");
            lines.Add("Coin SFX By Shnur_");
            #endregion
        }

        public void Draw()
        {
            // Display black background
            spriteBatch.FillRectangle(new Rectangle(0, 0, game.GraphicsDevice.DisplayMode.Width, game.GraphicsDevice.DisplayMode.Height), Color.Black);

            // Display all the lines
            float currentLineYAxis = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                currentLineYAxis = game.GraphicsDevice.DisplayMode.Height - spriteFont.LineSpacing + spriteFont.LineSpacing * i + scrollUp;
                // Fade as getting close to fade region
                float fadeRegion = (currentLineYAxis - (game.GraphicsDevice.DisplayMode.Width / 30) * 1.3f);
                float fadeSpeed = 0.0025f;
                spriteBatch.DrawString(spriteFont, lines[i],
                    new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - spriteFont.MeasureString(lines[i]).X / 2,
                    currentLineYAxis),
                    Color.White * (fadeRegion * fadeSpeed));
            }

            // Display the end message after all lines are done
            if (currentLineYAxis < 0f)
            {
                spriteBatch.DrawString(theEndFont, theEndStr,
                    new Vector2((game.GraphicsDevice.DisplayMode.Width - theEndFont.MeasureString(theEndStr).X) / 2f,
                    (game.GraphicsDevice.DisplayMode.Height - theEndFont.LineSpacing) / 2f),
                    Color.White);
            }
            
            scrollUp = scrollUp - (0.064f * Game1.DeltaTime);
        }
    }
}
