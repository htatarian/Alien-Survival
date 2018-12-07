using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using C3.XNA;

namespace HaroutTatarianGameProject
{
    public class InitialInputWindow : PopUp
    {
        private readonly int textWidth;
        private readonly int textHeight;
        private readonly int reallyTextHeight;
        private readonly int textX;
        private readonly int textY;
        private readonly string text = "";

        private const int maxInputLength = 3;
        private int inputWidth;
        private int inputHeight;
        private int inputX;
        private int inputY;

        public string Input { get; set; }
        private KeyboardState previousKeyboardState;

        public InitialInputWindow(Game game, SpriteBatch spriteBatch) : base(game,spriteBatch)
        {
            text = "ENTER YOUR INITIALS";
            textWidth = (int) spriteFont.MeasureString(text).X;
            textHeight = (int) spriteFont.MeasureString(text).Y;
            textX = x + Math.Abs(textWidth-width)/2;
            textY = y + 20;

            Input = "";
            inputWidth = (int)spriteFont.MeasureString(Input).X;
            inputHeight = (int)spriteFont.MeasureString(Input).Y;
            inputX = x + Math.Abs(inputWidth - width) / 2;
            inputY = y + Math.Abs(inputHeight - height) / 2;
            reallyTextHeight = (int)spriteFont.MeasureString("X").Y;
        }

        public override void Draw()
        {
            base.Draw();
            spriteBatch.DrawString(spriteFont, text, new Vector2(textX, textY), Color.Chartreuse);
            spriteBatch.DrawString(spriteFont, Input, new Vector2(inputX, inputY), Color.Chartreuse);
            spriteBatch.DrawLine(new Vector2(x, y + (height + reallyTextHeight) / 2),
                new Vector2(x+width, y + (height + reallyTextHeight) /2), Color.Chartreuse);
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

            inputWidth = (int)spriteFont.MeasureString(Input).X;
            inputHeight = (int)spriteFont.MeasureString(Input).Y;
            inputX = x + Math.Abs(inputWidth - width) / 2;
            inputY = y + Math.Abs(inputHeight - height) / 2;

            previousKeyboardState = keyboardState;
        }
    }
}
