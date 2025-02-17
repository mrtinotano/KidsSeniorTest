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
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
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
