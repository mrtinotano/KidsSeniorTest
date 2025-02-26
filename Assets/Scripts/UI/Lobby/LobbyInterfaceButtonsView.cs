using UnityEngine;

namespace KidsTest
{
    public class LobbyInterfaceButtonsView : MonoBehaviour
    {
        [SerializeField] private UIButton m_PlayButton;

        private LobbyInterfaceButtonsViewModel m_ViewModel;

        private void Awake()
        {
            m_ViewModel = new LobbyInterfaceButtonsViewModel();

            m_PlayButton.onClick.AddListener(()=> m_ViewModel.OnPlaySubmit());
        }
    }
}
