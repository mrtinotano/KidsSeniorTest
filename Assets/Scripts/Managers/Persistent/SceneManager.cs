using KidsTest.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace KidsTest
{
    public class SceneManager : PersistentSingleton<SceneManager>
    {
        [SerializeField, Scene] private string m_LogInScene; 
        [SerializeField, Scene] private string m_LobbyScene; 
        [SerializeField, Scene] private string m_GameScene;

        public void LoadLogInScene()
        {
            StartCoroutine(LoadSceneAsync(m_LogInScene));
        }

        public void LoadLobbyScene()
        {
            StartCoroutine(LoadSceneAsync(m_LobbyScene));
        }

        public void LoadGameScene()
        {
            StartCoroutine(LoadSceneAsync(m_GameScene));
        }

        private IEnumerator LoadSceneAsync(string scene)
        {
            Scene previousScene = UnitySceneManager.GetActiveScene();

            AsyncOperation op = UnitySceneManager.LoadSceneAsync(scene);
            op.allowSceneActivation = true;

            //Show a loding icon here or a Loading Scene

            yield return op;

            if (previousScene.IsValid())
                yield return UnitySceneManager.UnloadSceneAsync(previousScene);
        }
    }
}
