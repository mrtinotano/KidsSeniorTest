using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public class InitApp : MonoBehaviour
    {
        [SerializeField, Scene] private string m_LogInScene;

        private void Awake()
        {
            SceneManager.Instance.LoadScene(m_LogInScene);
        }
    }
}
