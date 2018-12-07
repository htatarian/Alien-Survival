using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HaroutTatarianGameProject
{
    public enum GameState
    { 
        StartScene,
        ActionScene,
        HelpScene,
        CreditScene,
        None
    }

    public class AudioManager
    {
        private readonly SoundEffectInstance mainMenuThemeInstance;
        private readonly SoundEffectInstance mainGameThemeInstance;
        private GameState gameState;

        public AudioManager(Game game)
        {
            SoundEffect mainMenuTheme = game.Content.Load<SoundEffect>("intro_soundeffect");
            mainMenuThemeInstance = mainMenuTheme.CreateInstance();
            mainMenuThemeInstance.IsLooped = true;
            mainMenuThemeInstance.Play();

            SoundEffect mainGameTheme = game.Content.Load<SoundEffect>("action_scenesfx");
            mainGameThemeInstance = mainGameTheme.CreateInstance();
            mainGameThemeInstance.IsLooped = true;
        }

        public void SetGameState(GameState gameState)
        {
            this.gameState = gameState;
            switch (gameState)
            {
                case GameState.StartScene:
                    mainMenuThemeInstance.Play();
                    mainGameThemeInstance.Stop();
                    break;
                case GameState.ActionScene:
                    mainGameThemeInstance.Play();
                    mainMenuThemeInstance.Stop();
                    break;
                default:
                    mainGameThemeInstance.Stop();
                    mainMenuThemeInstance.Stop();
                    break;
            }
        }

        public GameState GetGameState()
        {
            return gameState;
        }
    }
}
