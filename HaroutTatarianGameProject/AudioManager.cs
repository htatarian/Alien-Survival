using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HaroutTatarianGameProject
{
    public enum Audio
    { 
        StartScene,
        ActionScene,
        HelpScene,
        CreditScene,
        SelectionChange,
        StarCollected,
        Win,
        Lose,
        Hurt,
        None
    }

    public class AudioManager
    {
        private readonly SoundEffectInstance mainMenuThemeInstance;
        private readonly SoundEffectInstance mainGameThemeInstance;
        private readonly SoundEffectInstance winGameInstance;
        private readonly SoundEffectInstance loseGameSfxInstance;
        private readonly SoundEffectInstance hurtSfxInstance;
        private readonly SoundEffectInstance selectionChangeSfxInstance;
        private readonly SoundEffectInstance starCollectedSfxInstance;

        private Audio audio;

        public AudioManager(Game game)
        {
            SoundEffect mainMenuTheme = game.Content.Load<SoundEffect>("mainMenuTheme");
            mainMenuThemeInstance = mainMenuTheme.CreateInstance();
            mainMenuThemeInstance.IsLooped = true;
            mainMenuThemeInstance.Play();

            SoundEffect mainGameTheme = game.Content.Load<SoundEffect>("mainGameTheme");
            mainGameThemeInstance = mainGameTheme.CreateInstance();
            mainGameThemeInstance.IsLooped = true;

            SoundEffect winGameSfx = game.Content.Load<SoundEffect>("winGameSfx");
            winGameInstance = winGameSfx.CreateInstance();

            SoundEffect loseGameSfx = game.Content.Load<SoundEffect>("loseGameSfx");
            loseGameSfxInstance = loseGameSfx.CreateInstance();

            SoundEffect hurtSfx = game.Content.Load<SoundEffect>("hurtSfx");
            hurtSfxInstance = hurtSfx.CreateInstance();

            SoundEffect selectionChangeSfx = game.Content.Load<SoundEffect>("selectionChangeSfx");
            selectionChangeSfxInstance = selectionChangeSfx.CreateInstance();

            SoundEffect starCollectedSfx = game.Content.Load<SoundEffect>("starCollectedSfx");
            starCollectedSfxInstance = starCollectedSfx.CreateInstance();
        }

        public void Play(Audio audio)
        {
            this.audio = audio;
            switch (audio)
            {
                case Audio.StartScene:
                    mainMenuThemeInstance.Play();
                    mainGameThemeInstance.Stop();
                    break;
                case Audio.ActionScene:
                    mainGameThemeInstance.Play();
                    mainMenuThemeInstance.Stop();
                    break;
                case Audio.Win:
                    winGameInstance.Play();
                    break;
                case Audio.Lose:
                    loseGameSfxInstance.Play();
                    break;
                case Audio.Hurt:
                    hurtSfxInstance.Play();
                    break;
                case Audio.StarCollected:
                    starCollectedSfxInstance.Play();
                    break;
                case Audio.SelectionChange:
                    selectionChangeSfxInstance.Play();
                    break;
                default:
                    mainGameThemeInstance.Stop();
                    mainMenuThemeInstance.Stop();
                    break;
            }
        }

        public Audio GetGameState()
        {
            return audio;
        }
    }
}
