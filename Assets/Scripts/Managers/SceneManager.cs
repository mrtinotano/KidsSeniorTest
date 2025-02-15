using KidsTest.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace KidsTest
{
    public class SceneManager : PersistentSingleton<SceneManager>
    {
        [SerializeField, Scene] private string m_LogInScene; 
        [SerializeField, Scene] private string m_LobbyScene; 
        [SerializeField, Scene] private string m_SoundScene;

        public void LoadLogInScene()
        {
            UnitySceneManager.LoadScene(m_LogInScene);
        }

        public void LoadLobbyScene()
        {
            UnitySceneManager.LoadScene(m_LobbyScene);
        }

        public void LoadSoundScene()
        {
            UnitySceneManager.LoadScene(m_SoundScene);
        }
    }
}
