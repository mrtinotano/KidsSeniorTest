using KidsTest.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KidsTest
{
    public struct UpdateCharacterPartMeshEvent
    {
        public CustomCharacterPartType PartType;
        public int PartIndex;
    }

    public struct SetupInitialCharDataEvent
    {
        public CharacterDataSerialized CustomizationDataLoaded;
    }

    public struct SetupBlankCharacterEvent { }

    public class CharacterCustomization : MonoBehaviour
    {
        [Serializable]
        private class CharacterPart
        {
            public CustomCharacterPartType PartType;
            public SkinnedMeshRenderer PartMesh;
        }

        [SerializeField] private CharacterPart[] m_CharacterParts;

        private void Awake()
        {
            EventManager<UpdateCharacterPartMeshEvent>.AddListener(CharacterPartMeshUpdated);
        }

        private void Start()
        {
            LoadCharacterCustomization();
        }

        private void OnDestroy()
        {
            EventManager<UpdateCharacterPartMeshEvent>.RemoveListener(CharacterPartMeshUpdated);
        }

        private void LoadCharacterCustomization()
        {
            if (AppSaveManager.LoadCharacterData(out CharacterDataSerialized charData))
            {
                SetCustomPart(CustomCharacterPartType.Accessory, charData.AccessoryIndex);
                SetCustomPart(CustomCharacterPartType.Glasses, charData.GlassesIndex);
                SetCustomPart(CustomCharacterPartType.Hair, charData.HairIndex);
                SetCustomPart(CustomCharacterPartType.Hat, charData.HatIndex);
                SetCustomPart(CustomCharacterPartType.Pants, charData.PantsIndex);
                SetCustomPart(CustomCharacterPartType.Outer, charData.OuterIndex);
                SetCustomPart(CustomCharacterPartType.Shoes, charData.ShoesIndex);

                EventManager<SetupInitialCharDataEvent>.TriggerEvent(new SetupInitialCharDataEvent
                {
                    CustomizationDataLoaded = charData
                });
            }
            else
                EventManager<SetupBlankCharacterEvent>.TriggerEvent(new SetupBlankCharacterEvent());
        }

        private void CharacterPartMeshUpdated(UpdateCharacterPartMeshEvent eventData)
        {
            SetCustomPart(eventData.PartType, eventData.PartIndex);
        }

        private void SetCustomPart(CustomCharacterPartType partType, int partIndex)
        {
            foreach (CharacterPart part in m_CharacterParts)
            {
                if (part.PartType != partType)
                    continue;

                part.PartMesh.sharedMesh = CustomizationPartsSO.Instance.GetCustomPart(partType, partIndex);
                break;
            }
        }

        public int GetCustomPartIndex(CustomCharacterPartType partType)
        {
            foreach (CharacterPart part in m_CharacterParts)
            {
                if (part.PartType != partType)
                    continue;

                return CustomizationPartsSO.Instance.GetCustomPartIndex(partType, part.PartMesh?.sharedMesh ?? null);
            }

            return 0;
        }

        public void SaveCustomization()
        {
            Dictionary<CustomCharacterPartType, int> saveData = new Dictionary<CustomCharacterPartType, int>();

            foreach (CharacterPart part in m_CharacterParts)
            {
                saveData[part.PartType] = GetCustomPartIndex(part.PartType);
            }
            
            AppSaveManager.SaveCharacterData(saveData);
        }
    }
}
