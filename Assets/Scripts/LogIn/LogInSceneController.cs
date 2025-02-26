using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public class LogInSceneController : MonoBehaviour
    {
        [SerializeField, Scene] private string m_LobbyScene;

        private void Awake()
        {
            EventManager<LogInSuccessEvent>.AddListener(LogInSuccessful); 
        }

        private void LogInSuccessful(LogInSuccessEvent eventData)
        {
            AppSceneManager.LoadScene(m_LobbyScene, AppSceneManager.SceneLoadMode.Async);
        }
    }
}
