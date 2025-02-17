using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public abstract class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] protected int m_LevelID;
    }
}
