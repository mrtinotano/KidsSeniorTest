using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public struct PlayAudioEvent { }

    public struct SetPlayingAudioSourceEvent
    {
        public AudioSource AudioSource;
    }

    public struct ReturnToLobbyEvent { }

    public class AudioLevelController : LevelController
    {
        protected void Awake()
        {
            EventManager<PlayAudioEvent>.AddListener(PlayLevelAudio);
            EventManager<ReturnToLobbyEvent>.AddListener(ReturnToLobby);
        }

        private void OnDestroy()
        {
            EventManager<PlayAudioEvent>.RemoveListener(PlayLevelAudio);
            EventManager<ReturnToLobbyEvent>.RemoveListener(ReturnToLobby);
        }

        private void PlayLevelAudio(PlayAudioEvent eventData)
        {
            if (!AppLevelsSO.Instance.GetLevelConfig(m_LevelID, out LevelConfigSO config))
                return;

            if (config is not AudioLevelConfigSO audioLevelConfig)
                return;

            if (audioLevelConfig.LevelAudio is null)
                return;

            AudioSource source = AudioManager.Instance.PlaySound(audioLevelConfig.LevelAudio);

            EventManager<SetPlayingAudioSourceEvent>.TriggerEvent(new SetPlayingAudioSourceEvent
            {
                AudioSource = source
            });
        }

        private void ReturnToLobby(ReturnToLobbyEvent eventData)
        {
            AppSceneManager.LoadScene(AppScenesSO.Instance.LobbyScene, AppSceneManager.SceneLoadMode.Async);
        }
    }
}
