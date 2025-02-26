using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public class InitApp : MonoBehaviour
    {
        [SerializeField, Scene] private string m_LogInScene;

        private void Awake()
        {
            AppSceneManager.LoadScene(m_LogInScene, AppSceneManager.SceneLoadMode.Async);
        }
    }
}
