using UnityEngine;

namespace KidsTest
{
    [CreateAssetMenu(fileName = "Customization Parts", menuName = "KidsTest/Customiation Parts")]
    public class CustomizationPartsSO : ScriptableObject
    {
        [SerializeField] private CustomCharacterPartType m_PartType;
        [SerializeField] private GameObject[] m_CustomParts;

        public CustomCharacterPartType Part => m_PartType;
        public GameObject[] CustomParts => m_CustomParts;
    }
}
