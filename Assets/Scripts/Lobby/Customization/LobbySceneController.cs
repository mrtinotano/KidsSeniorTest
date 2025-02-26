using KidsTest.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KidsTest
{
    public struct LoadGameSceneEvent { }

    public class LobbySceneController : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] private CharacterCustomization m_CharacterPrefab;
        [SerializeField] private Transform m_CharacterPlacement;

        [Space]
        [SerializeField, Scene] private string m_GameScene;

        private CharacterCustomization m_InstantiatedCharacter;

        protected void Awake()
        {
            m_InstantiatedCharacter = Instantiate(m_CharacterPrefab, m_CharacterPlacement);
            
            EventManager<LoadGameSceneEvent>.AddListener(LoadGameScene);
        }

        private void LoadGameScene(LoadGameSceneEvent eventData)
        {
            m_InstantiatedCharacter.SaveCustomization();
            AppSceneManager.LoadScene(m_GameScene, AppSceneManager.SceneLoadMode.Async);
        }
    }
}
