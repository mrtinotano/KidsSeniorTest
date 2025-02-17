using UnityEngine;

namespace KidsTest
{
    public abstract class LevelConfigSO : ScriptableObject
    {
        [SerializeField] private int m_LevelID;

        public int LevelID => m_LevelID;
    }
}

