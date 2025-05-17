using UnityEngine;
using UnityEngine.Audio;

namespace KidsTest.Utils
{
    [CreateAssetMenu(fileName = "SO_AudioConfig", menuName = "Kids Test/Audio/Config")]
    public class AudioConfigSO : ScriptableObjectSingleton<AudioConfigSO>
    {
        [SerializeField] private AudioMixer m_AudioMixer;
        [SerializeField] private AudioMixerGroup m_MusicGroup;
        [SerializeField] private AudioMixerGroup m_SoundsGroup;

        public AudioMixer AudioMixer => m_AudioMixer;
        public AudioMixerGroup MusicGroup => m_MusicGroup;
        public AudioMixerGroup SoundsGroup => m_SoundsGroup;
    }
}
