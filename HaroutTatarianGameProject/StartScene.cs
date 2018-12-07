using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HaroutTatarianGameProject
{
    public class StartScene
    {
        public MenuComponent Menu;
        Background background;
        public StartScene(Game game,SpriteBatch spriteBatch)
        {
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("intro");
            background = new Background(game, spriteBatch,backgroundTexture);
            Menu = new MenuComponent(game, spriteBatch);

            Game1.audioManager.SetGameState(GameState.StartScene);
        }
        public void Update()
        {
            Menu.Update();
        }

        public void Draw()
        {
            background.Draw();
            Menu.Draw();
        }
    }
}
