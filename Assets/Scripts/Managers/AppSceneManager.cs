using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace KidsTest
{
    public static class AppSceneManager
    {
        public enum SceneLoadMode
        {
            Sync,
            Async
        }

        public static void LoadScene(string sceneName, SceneLoadMode mode)
        {
            switch (mode)
            {
                case SceneLoadMode.Sync:
                    LoadSceneSync(sceneName);
                    break;
                case SceneLoadMode.Async:
                    LoadSceneAsync(sceneName);
                    break;
            }
        }

        private static void LoadSceneSync(string scene)
        {
            Scene previousScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(scene);

            //Show a loding icon here or a Loading Scene

            if (previousScene.IsValid())
                SceneManager.UnloadSceneAsync(previousScene);
        }

        private static async Task LoadSceneAsync(string scene)
        {
            Scene previousScene = SceneManager.GetActiveScene();

            AsyncOperation op = SceneManager.LoadSceneAsync(scene);
            op.allowSceneActivation = true;

            //Show a loding icon here or a Loading Scene

            await op;

            if (previousScene.IsValid())
                await SceneManager.UnloadSceneAsync(previousScene);
        }
    }
}
