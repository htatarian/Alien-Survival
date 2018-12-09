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
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("start_scene_background");
            background = new Background(game, spriteBatch,backgroundTexture);
            Menu = new MenuComponent(game, spriteBatch);
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
