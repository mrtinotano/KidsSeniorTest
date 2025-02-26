using KidsTest.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class CustomizationPartView : MonoBehaviour
    {
        [Header("Part Type")]
        [SerializeField] private CustomCharacterPartType m_Part;

        [Header("UI")]
        [SerializeField] private Button m_PreviousSelectionButton;
        [SerializeField] private Button m_NextSelectionButton;
        [SerializeField] private TMP_Text m_PartText;

        private CustomizationPartViewModel m_ViewModel;

        private void Awake()
        {
            m_ViewModel = new CustomizationPartViewModel(m_Part);
            m_ViewModel.OnPartNameUpdated += SetPartName;

            m_NextSelectionButton.onClick.AddListener(()=> m_ViewModel.NextOption());
            m_PreviousSelectionButton.onClick.AddListener(()=> m_ViewModel.PreviousOption());
            EventManager<SetupInitialCharDataEvent>.AddListener(InitialDataCharacterSetup);
            EventManager<SetupBlankCharacterEvent>.AddListener(BlankCharacterSetup);
        }

        private void OnDestroy()
        {
            m_ViewModel.OnPartNameUpdated -= SetPartName;
            EventManager<SetupInitialCharDataEvent>.RemoveListener(InitialDataCharacterSetup);
            EventManager<SetupBlankCharacterEvent>.RemoveListener(BlankCharacterSetup);
        }

        private void InitialDataCharacterSetup(SetupInitialCharDataEvent eventData)
        {
            m_ViewModel.InitialDataSetup(eventData.CustomizationDataLoaded);
        }

        private void BlankCharacterSetup(SetupBlankCharacterEvent eventData)
        {
            m_ViewModel.InitialSetup();
        }

        private void SetPartName(string partName) => m_PartText.text = partName;
    }
}
