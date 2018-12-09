using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace HaroutTatarianGameProject
{
    public class InitialInputWindow : PopUp
    {
        private readonly int textWidth;
        private readonly int textX;
        private readonly int textY;
        private readonly string text = "";

        private const int maxInputLength = 3;
        private int inputWidth;
        private int inputX;
        private int inputY;

        public string Input { get; set; }
        private KeyboardState previousKeyboardState;

        public InitialInputWindow(Game game, SpriteBatch spriteBatch) : base(game,spriteBatch)
        {
            text = "ENTER YOUR INITIALS";
            textWidth = (int) subTitleFont.MeasureString(text).X;
            textX = rectangle.X + Math.Abs(textWidth-rectangle.Width)/2;
            textY = rectangle.Y + 20;

            Input = "";
            inputWidth = (int)subTitleFont.MeasureString(Input).X;
            inputX = rectangle.X  + Math.Abs(inputWidth - rectangle.Width) / 2;
            inputY = rectangle.Y + Math.Abs(subTitleFont.LineSpacing - rectangle.Height) / 2;
        }

        public override void Draw()
        {
            base.Draw();
            spriteBatch.DrawString(subTitleFont, text, new Vector2(textX, textY), Color.Chartreuse);
            spriteBatch.DrawString(subTitleFont, Input, new Vector2(inputX, inputY), Color.GreenYellow);
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] keys = keyboardState.GetPressedKeys();

            if (keys.Length != 0)
            {
                if ((previousKeyboardState.GetPressedKeys().Count() == 0 ||
                        previousKeyboardState.GetPressedKeys().ElementAt(0) != keys.ElementAt(0)))
                {
                    if (keys.Contains(Keys.Back) && Input.Length >= 1)
                    {
                        Input = Input.Remove(Input.Length - 1);
                    }
                    else if (Input.Length < maxInputLength
                        && (keys.ElementAt(0) >= Keys.A && keys.ElementAt(0) <= Keys.Z))
                    {
                        Input += keyboardState.GetPressedKeys().ElementAt(0).ToString().ToUpper();
                    }
                }
            }

            inputWidth = (int)subTitleFont.MeasureString(Input).X;
            inputX = rectangle.X + Math.Abs(inputWidth - rectangle.Width) / 2;
            inputY = rectangle.Y + Math.Abs(subTitleFont.LineSpacing - rectangle.Height) / 2;

            previousKeyboardState = keyboardState;
        }
    }
}
