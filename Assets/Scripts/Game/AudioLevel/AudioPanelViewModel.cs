using KidsTest.Utils;
using System;
using UnityEngine;

namespace KidsTest
{
    public class AudioPanelViewModel
    {
        public enum AudioState
        {
            Stopped,
            Playing,
            Paused
        }

        private AudioSource m_AudioSource;

        private float m_SliderMaxValue;
        private float m_SliderValue;

        public Action<float> OnAudioSliderMaxValueChanged;
        public Action<float> OnAudioSliderValueChanged;
        public Action<AudioState> OnAudioStateChanged;

        public Action OnAudioFinished;

        public void PlayAudio()
        {
            EventManager<PlayAudioEvent>.TriggerEvent(new PlayAudioEvent());
        }

        public void SetPlayingAudioSource(AudioSource audioSource)
        {
            m_AudioSource = audioSource;
            m_SliderMaxValue = audioSource.clip.length;
            m_SliderValue = 0;

            OnAudioSliderMaxValueChanged?.Invoke(m_SliderMaxValue);
            OnAudioStateChanged?.Invoke(AudioState.Playing);
            UpdateSliderValue();
        }

        public void PauseAudio()
        {
            m_AudioSource.Pause();
            OnAudioStateChanged?.Invoke(AudioState.Paused);
        }

        public void UnPauseAudio()
        {
            m_AudioSource.UnPause();
            OnAudioStateChanged?.Invoke(AudioState.Playing);
        }

        public void UpdateAudioValue()
        {
            m_SliderValue = Mathf.Clamp(m_AudioSource.time, 0, m_SliderMaxValue);
            UpdateSliderValue();
        }

        private void UpdateSliderValue()
        {
            OnAudioSliderValueChanged?.Invoke(m_SliderValue);

            if (m_SliderValue == m_SliderMaxValue)
            {
                OnAudioStateChanged?.Invoke(AudioState.Stopped);
                OnAudioFinished?.Invoke();
            }
        }
    }
}
