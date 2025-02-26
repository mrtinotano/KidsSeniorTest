using KidsTest.Utils;
using System;

namespace KidsTest
{
    public class CustomizationPartViewModel
    {
        private CustomizationPartModel m_Model;
        private int m_MeshCount;

        public Action<string> OnPartNameUpdated;

        public CustomizationPartViewModel(CustomCharacterPartType partType)
        {
            m_Model = new CustomizationPartModel();
            m_Model.SetCurrentMeshIndex(0);
            m_Model.SetPartType(partType);
        }

        public void InitialDataSetup(CharacterDataSerialized charData)
        {
            int index = m_Model.CharacterPartType switch
            {
                CustomCharacterPartType.Accessory => charData.AccessoryIndex,
                CustomCharacterPartType.Glasses => charData.GlassesIndex,
                CustomCharacterPartType.Hair => charData.HairIndex,
                CustomCharacterPartType.Hat => charData.HatIndex,
                CustomCharacterPartType.Pants => charData.PantsIndex,
                CustomCharacterPartType.Outer => charData.OuterIndex,
                CustomCharacterPartType.Shoes => charData.ShoesIndex
            };

            m_Model.SetCurrentMeshIndex(index);

            InitialSetup();
        }

        public void InitialSetup()
        {
            m_MeshCount = CustomizationPartsSO.Instance.GetCustomPartMeshLength(m_Model.CharacterPartType);
            string partName = CustomizationPartsSO.Instance.GetCustomizationPartName(m_Model.CharacterPartType);
            OnPartNameUpdated?.Invoke(partName);
        }

        public void NextOption()
        {
            int nextIndex = (m_Model.CurrentMeshIndex + 1) % m_MeshCount;
            m_Model.SetCurrentMeshIndex(nextIndex);
            NotifyMeshUpdate();
        }

        public void PreviousOption()
        {
            int previousIndex = (m_Model.CurrentMeshIndex - 1 + m_MeshCount) % m_MeshCount;
            m_Model.SetCurrentMeshIndex(previousIndex);
            NotifyMeshUpdate();
        }

        private void NotifyMeshUpdate()
        {
            EventManager<UpdateCharacterPartMeshEvent>.TriggerEvent(new UpdateCharacterPartMeshEvent()
            {
                PartType = m_Model.CharacterPartType,
                PartIndex = m_Model.CurrentMeshIndex
            });
        }
    }
}
