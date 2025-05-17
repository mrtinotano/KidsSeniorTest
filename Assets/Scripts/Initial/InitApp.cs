using UnityEngine;

namespace KidsTest
{
    public class InitApp : MonoBehaviour
    {
        private void Awake()
        {
            AppSceneManager.LoadScene(AppScenesSO.Instance.LoginScene, AppSceneManager.SceneLoadMode.Async);
        }
    }
}
