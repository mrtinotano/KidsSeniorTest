using UnityEngine;

namespace KidsTest
{
    [CreateAssetMenu(fileName = "SO_AudioLevelConfig", menuName = "Kids Test/Levels/Audio Level Config")]
    public class AudioLevelConfigSO : LevelConfigSO
    {
        [SerializeField] private AudioClip m_LevelAudio;

        public AudioClip LevelAudio => m_LevelAudio;
    }
}
