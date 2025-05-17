using UnityEngine;
using KidsTest.Utils;

namespace KidsTest
{
    [CreateAssetMenu(fileName = "SO_AppScenes", menuName = "Kids Test/App Scenes")]
    public class AppScenesSO : ScriptableObjectSingleton<AppScenesSO>
    {
        [SerializeField, Scene] private string m_LoginScene;
        [SerializeField, Scene] private string m_LobbyScene;
        [SerializeField, Scene] private string m_GameScene;

        public string LoginScene => m_LoginScene;
        public string LobbyScene => m_LobbyScene;
        public string GameScene => m_GameScene;
    }
}
