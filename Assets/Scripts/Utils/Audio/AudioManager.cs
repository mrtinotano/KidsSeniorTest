using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace KidsTest.Utils
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioMixer m_AudioMixer;
        private AudioSource m_MusicSource;
        private GameObject m_AudioManagerGO;

        private List<AudioSource> m_SoundSources = new List<AudioSource>();

        public AudioManager()
        {
            Init();
        }

        protected virtual void Init()
        {
            m_AudioMixer = AudioConfigSO.Instance.AudioMixer;
            CreateAudioManagerGameObject();
            CreateMusicSource();
        }

        private void CreateAudioManagerGameObject()
        {
            m_AudioManagerGO = new GameObject("AudioManager");
            Object.DontDestroyOnLoad(m_AudioManagerGO);
        }

        private void CreateMusicSource()
        {
            m_MusicSource = new GameObject("MusicSource").AddComponent<AudioSource>();
            m_MusicSource.outputAudioMixerGroup = AudioConfigSO.Instance.MusicGroup;
            m_MusicSource.playOnAwake = false;
            m_MusicSource.transform.parent = m_AudioManagerGO.transform;
        }

        private AudioSource CreateSoundSource()
        {
            AudioSource soundSource = new GameObject("SoundSource").AddComponent<AudioSource>();
            soundSource.outputAudioMixerGroup = AudioConfigSO.Instance.SoundsGroup;
            soundSource.playOnAwake = false;
            soundSource.transform.parent = m_AudioManagerGO.transform;
            m_SoundSources.Add(soundSource);

            return soundSource;
        }

        private AudioSource GetAvailableSoundSource()
        {
            foreach (AudioSource source in m_SoundSources)
            {
                if (!source.isPlaying && source.time == 0)
                    return source;
            }

            return CreateSoundSource();
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

        public void PlayMusic(AudioClip clip)
        {
            if (m_MusicSource.clip == clip)
                return;

            m_MusicSource.clip = clip;
            m_MusicSource.Play();
        }

        public void PauseMusic()
        {
            m_MusicSource.Pause();
        }

        public void UnpauseMusic()
        {
            m_MusicSource.Play();
        }

        public void StopMusic()
        {
            m_MusicSource.clip = null;
            m_MusicSource.Stop();
        }

        public AudioSource PlaySound(AudioClip clip)
        {
            AudioSource source = GetAvailableSoundSource();
            source.clip = clip;
            source.Play();

            return source;
        }

        public void StopAllSounds()
        {
            foreach (AudioSource source in m_SoundSources)
            {
                source.Stop();
            }
        }
    }
}
