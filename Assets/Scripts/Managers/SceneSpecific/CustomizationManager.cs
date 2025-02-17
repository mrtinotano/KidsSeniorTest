using KidsTest.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KidsTest
{
    public class CustomizationManager : Singleton<CustomizationManager>
    {
        [Header("Character")]
        [SerializeField] private CharacterCustomization m_CharacterPrefab;
        [SerializeField] private Transform m_CharacterPlacement;

        [Space]
        [SerializeField, Scene] private string m_GameScene;

        private CharacterCustomization m_InstantiatedCharacter;

        private Dictionary<CustomCharacterPartType, int> m_PartsIndex = new Dictionary<CustomCharacterPartType, int>();

        protected override void Awake()
        {
            base.Awake();

            m_InstantiatedCharacter = Instantiate(m_CharacterPrefab, m_CharacterPlacement);
        }

        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;

            var values = Enum.GetValues(typeof(CustomCharacterPartType)).Cast<CustomCharacterPartType>();

            foreach (var value in values)
            {
                m_PartsIndex[value] = m_InstantiatedCharacter.GetCustomPartIndex(value);
            }
        }

        private void OnSceneUnloaded(UnityEngine.SceneManagement.Scene current)
        {
            AppSaveManager.Instance.SaveCharacterData(m_PartsIndex);
        }

        public void LoadGameScene()
        {
            SceneManager.Instance.LoadScene(m_GameScene);
        }

        public void SetNextCustomPart(CustomCharacterPartType partType)
        {
            int index = (m_PartsIndex[partType] + 1) % CustomizationPartsSO.Instance.GetCustomPartLength(partType);
            SetCharacterNewPart(partType, index);
        }

        public void SetPreviousCustomPart(CustomCharacterPartType partType)
        {
            int length = CustomizationPartsSO.Instance.GetCustomPartLength(partType);
            int index = (m_PartsIndex[partType] - 1 + length) % length;
            SetCharacterNewPart(partType, index);
        }

        private void SetCharacterNewPart(CustomCharacterPartType partType, int index)
        {
            m_InstantiatedCharacter.SetCustomPart(partType, index);
            m_PartsIndex[partType] = index;
        }
    }
}
