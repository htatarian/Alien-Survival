using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace HaroutTatarianGameProject
{
    public enum Audio
    { 
        StartScene,
        ActionScene,
        LeaderboardScene,
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
        private readonly SoundEffectInstance leaderboardThemeInstance;
        private readonly SoundEffectInstance winGameInstance;
        private readonly SoundEffectInstance loseGameSfxInstance;
        private readonly SoundEffectInstance hurtSfxInstance;
        private readonly SoundEffectInstance selectionChangeSfxInstance;
        private readonly SoundEffectInstance starCollectedSfxInstance;
        private readonly SoundEffectInstance creditsSceneThemeInstance;

        public AudioManager(Game game)
        {
            SoundEffect mainMenuTheme = game.Content.Load<SoundEffect>("mainMenuTheme");
            mainMenuThemeInstance = mainMenuTheme.CreateInstance();
            mainMenuThemeInstance.IsLooped = true;

            SoundEffect leaderboardTheme = game.Content.Load<SoundEffect>("leaderboardTheme");
            leaderboardThemeInstance = leaderboardTheme.CreateInstance();
            leaderboardThemeInstance.IsLooped = true;

            SoundEffect mainGameTheme = game.Content.Load<SoundEffect>("mainGameTheme");
            mainGameThemeInstance = mainGameTheme.CreateInstance();
            mainGameThemeInstance.IsLooped = true;

            SoundEffect creditsSceneTheme = game.Content.Load<SoundEffect>("creditsSceneTheme");
            creditsSceneThemeInstance = creditsSceneTheme.CreateInstance();

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
            switch (audio)
            {
                case Audio.StartScene:
                    leaderboardThemeInstance.Stop();
                    creditsSceneThemeInstance.Stop();
                    mainGameThemeInstance.Stop();
                    mainMenuThemeInstance.Play();
                    break;
                case Audio.LeaderboardScene:
                    creditsSceneThemeInstance.Stop();
                    mainGameThemeInstance.Stop();
                    mainMenuThemeInstance.Stop();
                    leaderboardThemeInstance.Play();
                    break;
                case Audio.ActionScene:
                    creditsSceneThemeInstance.Stop();
                    leaderboardThemeInstance.Stop();
                    mainMenuThemeInstance.Stop();
                    mainGameThemeInstance.Play();
                    break;
                case Audio.CreditScene:
                    leaderboardThemeInstance.Stop();
                    mainMenuThemeInstance.Stop();
                    mainGameThemeInstance.Stop();
                    creditsSceneThemeInstance.Play();
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
                    leaderboardThemeInstance.Stop();
                    mainMenuThemeInstance.Stop();
                    creditsSceneThemeInstance.Stop();
                    break;
            }
        }
    }
}
