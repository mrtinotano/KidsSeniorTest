using KidsTest.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KidsTest
{
    public enum CustomCharacterPartType
    {
        Accessory,
        Glasses,
        Hair,
        Hat,
        Pants,
        Outer,
        Shoes
    }

    public class CustomizationManager : Singleton<CustomizationManager>
    {
        [Header("Character")]
        [SerializeField] private CharacterCustomization m_CharacterPrefab;
        [SerializeField] private Transform m_CharacterPlacement;

        [Header("Parts")]
        [SerializeField] private CustomizationPartsSO[] m_CustomParts;

        private CharacterCustomization m_InstantiatedCharacter;

        private Dictionary<CustomCharacterPartType, int> m_PartsIndex = new Dictionary<CustomCharacterPartType, int>();

        protected override void Awake()
        {
            base.Awake();

            m_InstantiatedCharacter = Instantiate(m_CharacterPrefab, m_CharacterPlacement);

            LoadCharacterIndex();
        }

        private void LoadCharacterIndex()
        {
            CharacterDataSerialized charData = AppSaveManager.Instance.LoadCharacterData();

            if (charData != null)
            {
                foreach (CustomizationPartsSO customPart in m_CustomParts)
                {
                    int index = customPart.Part switch
                    {
                        CustomCharacterPartType.Accessory => charData.AccessoryIndex,
                        CustomCharacterPartType.Glasses => charData.GlassesIndex,
                        CustomCharacterPartType.Hair => charData.HairIndex,
                        CustomCharacterPartType.Hat => charData.HatIndex,
                        CustomCharacterPartType.Pants => charData.PantsIndex,
                        CustomCharacterPartType.Outer => charData.OuterIndex,
                        CustomCharacterPartType.Shoes => charData.ShoesIndex
                    };

                    m_PartsIndex[customPart.Part] = index;
                    GameObject part = customPart.CustomParts[index];
                    Mesh mesh = part ? part.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh : null;
                    m_InstantiatedCharacter.SetNewPart(customPart.Part, mesh);
                }
            }
            else
            {
                foreach (CustomizationPartsSO customPart in m_CustomParts)
                {
                    m_PartsIndex[customPart.Part] = 0;
                }
            }
        }

        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(UnityEngine.SceneManagement.Scene current)
        {
            AppSaveManager.Instance.SaveCharacterData(m_PartsIndex);
        }

        public void SetNextPart(CustomCharacterPartType partType)
        {
            foreach (CustomizationPartsSO customPart in m_CustomParts)
            {
                if (customPart.Part != partType)
                    continue;

                int index = (m_PartsIndex[customPart.Part] + 1) % customPart.CustomParts.Length;
                GameObject part = customPart.CustomParts[index];
                Mesh mesh = part ? part.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh : null;
                m_InstantiatedCharacter.SetNewPart(customPart.Part, mesh);
                m_PartsIndex[customPart.Part] = index;
                break;
            }
        }

        public void SetPreviousPart(CustomCharacterPartType partType)
        {
            foreach (CustomizationPartsSO customPart in m_CustomParts)
            {
                if (customPart.Part != partType)
                    continue;

                int index = (m_PartsIndex[customPart.Part] - 1) % customPart.CustomParts.Length;
                GameObject part = customPart.CustomParts[index];
                Mesh mesh = part ? part.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh : null;
                m_InstantiatedCharacter.SetNewPart(customPart.Part, mesh);
                m_PartsIndex[customPart.Part] = index;
                break;
            }
        }

        public string GetCustomizationPartName(CustomCharacterPartType part)
        {
            return part switch
            {
                CustomCharacterPartType.Accessory => "Accessory",
                CustomCharacterPartType.Glasses => "Glasses",
                CustomCharacterPartType.Hair => "Hair",
                CustomCharacterPartType.Hat => "Hat",
                CustomCharacterPartType.Pants => "Pants",
                CustomCharacterPartType.Outer => "Outer",
                CustomCharacterPartType.Shoes => "Shoes"
            };
        }
    }
}
