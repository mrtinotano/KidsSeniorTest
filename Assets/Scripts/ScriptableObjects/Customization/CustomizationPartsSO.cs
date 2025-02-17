using KidsTest.Utils;
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

    [CreateAssetMenu(fileName = "SO_CustomizationParts", menuName = "Kids Test/Customiation Parts")]
    public class CustomizationPartsSO : ScriptableObjectSingleton<CustomizationPartsSO>
    {
        [System.Serializable]
        private struct CustomPart
        {
            public CustomCharacterPartType PartType;
            public GameObject[] CustomParts;
        }

        [SerializeField] private CustomPart[] m_CustomParts;

        public Mesh GetCustomPart(CustomCharacterPartType partType, int index)
        {
            Mesh customMesh = null;

            foreach (CustomPart customPart in m_CustomParts)
            {
                if (customPart.PartType != partType)
                    continue;

                GameObject part = customPart.CustomParts[index];
                customMesh = part ? part.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh : null;
                break;
            }

            return customMesh;
        }

        public int GetCustomPartLength(CustomCharacterPartType partType)
        {
            int length = 0;

            foreach (CustomPart customPart in m_CustomParts)
            {
                if (customPart.PartType != partType)
                    continue;

                length = customPart.CustomParts.Length;
                break;
            }

            return length;
        }

        public int GetCustomPartIndex(CustomCharacterPartType partType, Mesh mesh)
        {
            foreach (CustomPart customPart in m_CustomParts)
            {
                if (customPart.PartType != partType)
                    continue;

                for (int i = 0; i < customPart.CustomParts.Length; i++)
                {
                    GameObject part = customPart.CustomParts[i];
                    Mesh customMesh = part ? part.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh : null;

                    if (mesh != customMesh)
                        continue;

                    return i;
                }
            }

            return 0;
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
