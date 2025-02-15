using KidsTest.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KidsTest
{
    public enum CustomCharacterParts
    {
        Body,
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
        [System.Serializable]
        private struct CustomParts
        {
            public CustomCharacterParts Part;
            public GameObject[] Meshes;
        }

        [Header("Character")]
        [SerializeField] private CharacterCustomization m_CharacterPrefab;
        [SerializeField] private Transform m_CharacterPlacement;

        [Header("Parts")]
        [SerializeField] private CustomParts[] m_CustomParts;

        private CharacterCustomization m_InstantiatedCharacter;

        private Dictionary<CustomCharacterParts, int> m_PartsIndex = new Dictionary<CustomCharacterParts, int>();

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
                foreach (CustomParts customPart in m_CustomParts)
                {
                    int index = customPart.Part switch
                    {
                        CustomCharacterParts.Body => charData.BodyIndex,
                        CustomCharacterParts.Accessory => charData.AccessoryIndex,
                        CustomCharacterParts.Glasses => charData.GlassesIndex,
                        CustomCharacterParts.Hair => charData.HairIndex,
                        CustomCharacterParts.Hat => charData.HatIndex,
                        CustomCharacterParts.Pants => charData.PantsIndex,
                        CustomCharacterParts.Outer => charData.OuterIndex,
                        CustomCharacterParts.Shoes => charData.ShoesIndex
                    };

                    m_PartsIndex[customPart.Part] = index;
                    Mesh mesh = customPart.Meshes[index].GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
                    m_InstantiatedCharacter.SetNewPart(customPart.Part, mesh);
                }
            }
            else
            {
                foreach (CustomParts customPart in m_CustomParts)
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

        public void SetNextPart(CustomCharacterParts part)
        {
            foreach (CustomParts customPart in m_CustomParts)
            {
                if (customPart.Part != part)
                    continue;

                int index = (m_PartsIndex[customPart.Part] + 1) % customPart.Meshes.Length;
                Mesh mesh = customPart.Meshes[index].GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
                m_InstantiatedCharacter.SetNewPart(customPart.Part, mesh);
                m_PartsIndex[customPart.Part] = index;
                break;
            }
        }

        public void SetPreviousPart(CustomCharacterParts part)
        {
            foreach (CustomParts customPart in m_CustomParts)
            {
                if (customPart.Part != part)
                    continue;

                int index = (m_PartsIndex[customPart.Part] - 1) % customPart.Meshes.Length;
                Mesh mesh = customPart.Meshes[index].GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
                m_InstantiatedCharacter.SetNewPart(customPart.Part, mesh);
                m_PartsIndex[customPart.Part] = index;
                break;
            }
        }

        public string GetCustomizationPartName(CustomCharacterParts part)
        {
            return part switch
            {
                CustomCharacterParts.Body => "Body",
                CustomCharacterParts.Accessory => "Accessory",
                CustomCharacterParts.Glasses => "Glasses",
                CustomCharacterParts.Hair => "Hair",
                CustomCharacterParts.Hat => "Hat",
                CustomCharacterParts.Pants => "Pants",
                CustomCharacterParts.Outer => "Outer",
                CustomCharacterParts.Shoes => "Shoes"
            };
        }
    }
}
