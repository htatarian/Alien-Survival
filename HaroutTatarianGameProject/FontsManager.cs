using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaroutTatarianGameProject
{
    public enum Font
    {
        CourierNew30,
        CourierNew40,
        CourierNew60,
        CourierNew120,
        CourierNew160
    }
    public class FontsManager
    {
        #region private readonly sprite fonts
        private readonly SpriteFont CourierNew40;
        private readonly SpriteFont CourierNew60;
        private readonly SpriteFont CourierNew120;
        private readonly SpriteFont CourierNew160;
        #endregion

        public FontsManager(Game game)
        {
            // Load fonts
            CourierNew40 = game.Content.Load<SpriteFont>("Courier_New_40");
            CourierNew60 = game.Content.Load<SpriteFont>("Courier_New_60");
            CourierNew120 = game.Content.Load<SpriteFont>("Courier_New_120");
            CourierNew160 = game.Content.Load<SpriteFont>("Courier_New_160");
        }

        /// <summary>
        /// Returns the spriteFont based on the enum values
        /// </summary>
        /// <param name="font">Enum value to be passed</param>
        /// <returns>Return spirte font equivalent to the enum value passsed</returns>
        public SpriteFont GetFont(Font font)
        {
            SpriteFont spriteFont;

            switch (font)
            {
                case Font.CourierNew40:
                    spriteFont = CourierNew40;
                    break;
                case Font.CourierNew60:
                    spriteFont = CourierNew60;
                    break;
                case Font.CourierNew120:
                    spriteFont = CourierNew120;
                    break;
                case Font.CourierNew160:
                    spriteFont = CourierNew160;
                    break;
                default:
                    spriteFont = CourierNew40;
                    break;
            }

            return spriteFont;
        }
    }
}
