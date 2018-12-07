using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HaroutTatarianGameProject
{
    public class HelpScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D helpTex;

        public HelpScene(Game game,SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            helpTex = game.Content.Load<Texture2D>("helpImage");
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(helpTex, Vector2.Zero, Color.White);
        }
    }
}
