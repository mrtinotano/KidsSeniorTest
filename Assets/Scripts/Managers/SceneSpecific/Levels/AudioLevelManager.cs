using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public class AudioLevelManager : LevelManager
    {
        public enum AudioState
        {
            Stopped,
            Playing,
            Paused
        }

        [SerializeField, Scene] private string m_LobbyScene;

        private AudioClip m_AudioClip;
        private AudioSource m_AudioSource;

        public AudioState CurrentAudioState { get; private set; } = AudioState.Stopped;
        public float AudioTime => m_AudioSource.time;

        public static new AudioLevelManager Instance => LevelManager.Instance as AudioLevelManager;

        protected override void Awake()
        {
            base.Awake();

            AudioLevelConfigSO config = AppLevelsSO.Instance.GetLevelConfig(m_LevelID) as AudioLevelConfigSO;
            m_AudioClip = config.LevelAudio;
        }

        public void ReturnToLobby()
        {
            SceneManager.Instance.LoadScene(m_LobbyScene);
        }

        public AudioClip PlayAudio()
        {
            m_AudioSource = AudioManager.Instance.PlaySound(m_AudioClip);
            CurrentAudioState = AudioState.Playing;
            return m_AudioClip;
        }

        public void PauseAudio()
        {
            AudioManager.Instance.PauseSound(m_AudioSource);
            CurrentAudioState = AudioState.Paused;
        }

        public void UnPauseAudio()
        {
            AudioManager.Instance.UnPauseSound(m_AudioSource);
            CurrentAudioState = AudioState.Playing;
        }
    }
}
