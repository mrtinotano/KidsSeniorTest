using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public class AudioLevelManager : Singleton<AudioLevelManager>
    {
        public enum AudioState
        {
            Stopped,
            Playing,
            Paused
        }

        [SerializeField] private AudioClip m_AudioClip;

        private AudioSource m_AudioSource;

        public AudioState CurrentAudioState { get; private set; } = AudioState.Stopped;
        public float AudioTime => m_AudioSource.time;

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
