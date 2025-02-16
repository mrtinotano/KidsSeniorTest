using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class CustomizationPartSelection : MonoBehaviour
    {
        [Header("Part Type")]
        [SerializeField] private CustomCharacterPartType m_Part;

        [Header("UI")]
        [SerializeField] private Button m_PreviousSelectionButton;
        [SerializeField] private Button m_NextSelectionButton;
        [SerializeField] private TMP_Text m_PartText;

        private void Awake()
        {
            m_PreviousSelectionButton.onClick.AddListener(()=> SelectPreviousPart());
            m_NextSelectionButton.onClick.AddListener(()=> SelectNextPart());

            m_PartText.text = CustomizationManager.Instance.GetCustomizationPartName(m_Part);
        }

        private void SelectNextPart() => CustomizationManager.Instance.SetNextPart(m_Part);

        private void SelectPreviousPart() => CustomizationManager.Instance.SetPreviousPart(m_Part);
    }
}
