using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    [CreateAssetMenu(fileName = "SO_AppLevels", menuName = "Kids Test/Levels/App Levels")]
    public class AppLevelsSO : ScriptableObjectSingleton<AppLevelsSO>
    {
        [SerializeField] private LevelConfigSO[] m_LevelsConfigs;

        public LevelConfigSO GetLevelConfig(int levelID)
        {
            foreach (LevelConfigSO levelConfig in m_LevelsConfigs)
            {
                if (levelConfig.LevelID != levelID)
                    continue;

                return levelConfig;
            }

            return null;
        }
    }
}
