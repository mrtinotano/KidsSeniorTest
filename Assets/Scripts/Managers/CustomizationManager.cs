using KidsTest.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KidsTest
{
    public enum CustomCharacterParts
    {
        Body,
        Accessory,
        Face,
        Glasses,
        Gloves,
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
        
            foreach (CustomParts customPart in m_CustomParts)
            {
                m_PartsIndex[customPart.Part] = 0;
            }
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
                CustomCharacterParts.Face => "Face",
                CustomCharacterParts.Glasses => "Glasses",
                CustomCharacterParts.Gloves => "Gloves",
                CustomCharacterParts.Hair => "Hair",
                CustomCharacterParts.Hat => "Hat",
                CustomCharacterParts.Pants => "Pants",
                CustomCharacterParts.Outer => "Outer",
                CustomCharacterParts.Shoes => "Shoes"
            };
        }

    }
}
