using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public class LogInSceneController : MonoBehaviour
    {
        private void Awake()
        {
            EventManager<LogInSuccessEvent>.AddListener(LogInSuccessful); 
        }

        private void OnDestroy()
        {
            EventManager<LogInSuccessEvent>.RemoveListener(LogInSuccessful);
        }

        private void LogInSuccessful(LogInSuccessEvent eventData)
        {
            AppSceneManager.LoadScene(AppScenesSO.Instance.LobbyScene, AppSceneManager.SceneLoadMode.Async);
        }
    }
}
