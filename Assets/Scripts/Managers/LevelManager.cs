using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private AudioClip m_AudioClip;
        [SerializeField] private AudioSource m_AudioSource;

        public float AudioTime => m_AudioSource.time;

        public AudioClip PlayAudio()
        {
            m_AudioSource.clip = m_AudioClip;
            m_AudioSource.Play();
            return m_AudioClip;
        }
    }
}
