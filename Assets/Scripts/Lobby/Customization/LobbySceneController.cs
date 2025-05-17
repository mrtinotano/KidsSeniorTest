using KidsTest.Utils;
using UnityEngine;

namespace KidsTest
{
    public struct LoadGameSceneEvent { }

    public class LobbySceneController : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] private CharacterCustomization m_CharacterPrefab;
        [SerializeField] private Transform m_CharacterPlacement;

        private CharacterCustomization m_InstantiatedCharacter;

        protected void Awake()
        {
            m_InstantiatedCharacter = Instantiate(m_CharacterPrefab, m_CharacterPlacement);
            
            EventManager<LoadGameSceneEvent>.AddListener(LoadGameScene);
        }

        private void OnDestroy()
        {
            EventManager<LoadGameSceneEvent>.RemoveListener(LoadGameScene);
        }

        private void LoadGameScene(LoadGameSceneEvent eventData)
        {
            m_InstantiatedCharacter.SaveCustomization();
            AppSceneManager.LoadScene(AppScenesSO.Instance.GameScene, AppSceneManager.SceneLoadMode.Async);
        }
    }
}
