using System.Linq;
using UnityEngine;

namespace KidsTest
{
    public class CharacterCustomization : MonoBehaviour
    {
        [System.Serializable]
        private struct CustomizationPart
        {
            public CustomCharacterPartType Part;
            public SkinnedMeshRenderer PartMesh;
        }

        [SerializeField] private CustomizationPart[] m_CustomizationParts;
        
        private void LoadCharacterCustomization()
        {

        }

        public void SetNewPart(CustomCharacterPartType part, Mesh mesh)
        {
            foreach (CustomizationPart customPart in m_CustomizationParts)
            {
                if (customPart.Part != part)
                    continue;

                customPart.PartMesh.sharedMesh = mesh;
                break;
            }
        }

        public void SetNewBodyColor()
        {
            //m_CustomizationParts.Select(x => x.Part == )
        }
    }
}
