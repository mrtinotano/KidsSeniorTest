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
        [SerializeField] private AudioSource m_AudioSource;

        public AudioState CurrentAudioState { get; private set; } = AudioState.Stopped;
        public float AudioTime => m_AudioSource.time;

        public AudioClip PlayAudio()
        {
            m_AudioSource.clip = m_AudioClip;
            m_AudioSource.Play();
            CurrentAudioState = AudioState.Playing;
            return m_AudioClip;
        }

        public void PauseAudio()
        {
            m_AudioSource.Pause();
            CurrentAudioState = AudioState.Paused;
        }

        public void UnPauseAudio()
        {
            m_AudioSource.UnPause();
            CurrentAudioState = AudioState.Playing;
        }
    }
}
