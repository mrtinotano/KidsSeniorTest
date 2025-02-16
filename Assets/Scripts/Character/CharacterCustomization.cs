using System;
using UnityEngine;

namespace KidsTest
{
    public class CharacterCustomization : MonoBehaviour
    {
        [Serializable]
        private struct CharacterPart
        {
            public CustomCharacterPartType PartType;
            public SkinnedMeshRenderer PartMesh;
        }

        [SerializeField] private CharacterPart[] m_CharacterParts;

        private void Awake()
        {
            LoadCharacterCustomization();
        }

        private void LoadCharacterCustomization()
        {
            CharacterDataSerialized charData = AppSaveManager.Instance.LoadCharacterData();

            if (charData != null)
            {
                SetCustomPart(CustomCharacterPartType.Accessory, charData.AccessoryIndex);
                SetCustomPart(CustomCharacterPartType.Glasses, charData.GlassesIndex);
                SetCustomPart(CustomCharacterPartType.Hair, charData.HairIndex);
                SetCustomPart(CustomCharacterPartType.Hat, charData.HatIndex);
                SetCustomPart(CustomCharacterPartType.Pants, charData.PantsIndex);
                SetCustomPart(CustomCharacterPartType.Outer, charData.OuterIndex);
                SetCustomPart(CustomCharacterPartType.Shoes, charData.ShoesIndex);
            }
        }

        public void SetCustomPart(CustomCharacterPartType partType, int partIndex)
        {
            foreach (CharacterPart part in m_CharacterParts)
            {
                if (part.PartType != partType)
                    continue;

                part.PartMesh.sharedMesh = CustomizationPartsSO.Instance.GetCustomPart(partType, partIndex);
            }
        }

        public int GetCustomPartIndex(CustomCharacterPartType partType)
        {
            foreach (CharacterPart part in m_CharacterParts)
            {
                if (part.PartType != partType)
                    continue;

                return CustomizationPartsSO.Instance.GetCustomPartIndex(partType, part.PartMesh.sharedMesh);
            }

            return 0;
        }
    }
}
