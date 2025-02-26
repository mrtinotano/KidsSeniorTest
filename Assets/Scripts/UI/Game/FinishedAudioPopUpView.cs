using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class FinishedAudioPopUpView : MonoBehaviour
    {
        [SerializeField] private Button m_LobbyButton;

        private FinishedAudioPopUpViewViewModel m_ViewModel;

        private void Awake()
        {
            m_ViewModel = new FinishedAudioPopUpViewViewModel();
            
            m_LobbyButton.onClick.AddListener(()=> m_ViewModel.OnReturnButtonSubmit());
        }

        private void OnEnable()
        {
            transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 1);
        }
    }
}
