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
        CourierNew40,
        CourierNew60,
        CourierNew120,
        CourierNew160
    }

    public class FontsManager
    {
        private readonly SpriteFont CourierNew40;
        private readonly SpriteFont CourierNew60;
        private readonly SpriteFont CourierNew120;
        private readonly SpriteFont CourierNew160;

        public FontsManager(Game game)
        {
            CourierNew40 = game.Content.Load<SpriteFont>("Courier New 40");
            CourierNew60 = game.Content.Load<SpriteFont>("Courier New 60");
            CourierNew120 = game.Content.Load<SpriteFont>("Courier New 120");
            CourierNew160 = game.Content.Load<SpriteFont>("Courier New 160");
        }

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
