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

            m_PartText.text = CustomizationPartsSO.Instance.GetCustomizationPartName(m_Part);
        }

        private void SelectNextPart() => CustomizationManager.Instance.SetNextCustomPart(m_Part);

        private void SelectPreviousPart() => CustomizationManager.Instance.SetPreviousCustomPart(m_Part);
    }
}
