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
        private const string theEndStr = "The End";
        private readonly Game game;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont;
        private readonly SpriteFont theEndFont;
        private readonly List<string> lines;
        private float scrollUp;

        public CreditScene(Game game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            lines = new List<string>();
            scrollUp = 0;

            // Set fonts
            spriteFont = Game1.fontsManager.GetFont(Font.CourierNew40);
            theEndFont = Game1.fontsManager.GetFont(Font.CourierNew120);
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
            lines.Add("PICTURES");
            lines.Add("");
            lines.Add("Redwallpapers");
            lines.Add("Craftpix");
            lines.Add("Matkojedyna");
            lines.Add("Derpibooru");
            lines.Add("DontMind8");
            lines.Add("");
            lines.Add("MUSIC");
            lines.Add("");
            lines.Add("Oymaldonado");
            lines.Add("Jimhancock");
            lines.Add("Tuudurt");
            lines.Add("Cabledmess");
            lines.Add("Michel88");
            lines.Add("Shnur_");
            lines.Add("Mikobuntu");
            lines.Add("Haydensayshi123");
            lines.Add("ShadyDave");
        }

        public void Draw()
        {
            spriteBatch.FillRectangle(new Rectangle(0, 0, game.GraphicsDevice.DisplayMode.Width, game.GraphicsDevice.DisplayMode.Height), Color.Black);
            float yAxis = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                yAxis = game.GraphicsDevice.DisplayMode.Height / 4 + spriteFont.LineSpacing * i + scrollUp;
                spriteBatch.DrawString(spriteFont, lines[i],
                    new Vector2(game.GraphicsDevice.DisplayMode.Width / 2 - spriteFont.MeasureString(lines[i]).X / 2,
                    yAxis),
                    Color.White * ((yAxis - (game.GraphicsDevice.DisplayMode.Width / 30) * 1.3f) * 0.0025f));
            }

            if (yAxis < 0f)
            {
                spriteBatch.DrawString(theEndFont, theEndStr,
                    new Vector2((game.GraphicsDevice.DisplayMode.Width - theEndFont.MeasureString(theEndStr).X) / 2f,
                    (game.GraphicsDevice.DisplayMode.Height - theEndFont.LineSpacing) / 2f),
                    Color.White);
            }
            
            scrollUp = (scrollUp - (0.07f * Game1.DeltaTime));
        }
    }
}
