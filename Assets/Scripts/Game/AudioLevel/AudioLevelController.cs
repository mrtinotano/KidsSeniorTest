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
        [SerializeField, Scene] private string m_LobbyScene;

        protected void Awake()
        {
            EventManager<PlayAudioEvent>.AddListener(PlayLevelAudio);
            EventManager<ReturnToLobbyEvent>.AddListener(ReturnToLobby);
        }

        private void PlayLevelAudio(PlayAudioEvent eventData)
        {
            LevelConfigSO config = AppLevelsSO.Instance.GetLevelConfig(m_LevelID);

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
            AppSceneManager.LoadScene(m_LobbyScene, AppSceneManager.SceneLoadMode.Async);
        }
    }
}
