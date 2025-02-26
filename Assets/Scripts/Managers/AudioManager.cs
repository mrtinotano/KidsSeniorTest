using KidsTest.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace KidsTest
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioMixer m_AudioMixer;
        [SerializeField] private AudioSource[] m_SoundSources;

        private Queue<AudioSource> m_SoundSourcesQueue = new Queue<AudioSource>();

        protected override void Awake()
        {
            base.Awake();

            foreach (AudioSource source in m_SoundSources)
            {
                m_SoundSourcesQueue.Enqueue(source);
            }
        }

        private float VolumeToDecibels(float volume)
        {
            return volume == 0f ? -80f : Mathf.Log10(volume * 0.01f) * 20f; ;
        }

        private float DecibelsToVolume(float decibels)
        {
            decibels = Mathf.Clamp(decibels, -80f, 0f);
            return Mathf.Round(Mathf.Pow(10.0f, decibels / 20.0f) * 100f); ;
        }

        public void SetMixerVolume(string param, float volume)
        {
            m_AudioMixer.SetFloat(param, VolumeToDecibels(volume));
        }

        public float GetMixerVolume(string param)
        {
            m_AudioMixer.GetFloat(param, out float decibels);
            return DecibelsToVolume(decibels);
        }

        public AudioSource PlaySound(AudioClip clip, float delay = 0f)
        {
            if (clip is null)
                return null;

            AudioSource audioSource = m_SoundSourcesQueue.Dequeue();
            StartCoroutine(PlaySoundCo(clip, audioSource, delay));

            return audioSource;
        }

        private IEnumerator PlaySoundCo(AudioClip clip, AudioSource audioSource, float delay = 0f)
        {
            if (delay > 0)
            {
                if (Time.timeScale == 0)
                    yield return new WaitForSecondsRealtime(delay);
                else
                    yield return new WaitForSeconds(delay);
            }

            audioSource.clip = clip;
            audioSource.Play();

            while (audioSource.isPlaying || audioSource.time > 0)
            {
                yield return null;
            }

            m_SoundSourcesQueue.Enqueue(audioSource);
        }

        public void PauseSound(AudioSource audioSource) => audioSource.Pause();
        public void UnPauseSound(AudioSource audioSource) => audioSource.UnPause();
        public void StopSound(AudioSource audioSource)
        {
            audioSource.Stop();
            m_SoundSourcesQueue.Enqueue(audioSource);
        }

        public void StopAllSounds()
        {
            foreach (AudioSource source in m_SoundSourcesQueue)
            {
                source.Stop();
                m_SoundSourcesQueue.Enqueue(source);
            }
        }
    }
}
