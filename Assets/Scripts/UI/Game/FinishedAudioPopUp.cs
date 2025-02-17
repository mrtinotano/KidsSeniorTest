using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class FinishedAudioPopUp : MonoBehaviour
    {
        [SerializeField] private Button m_LobbyButton;

        private void Awake()
        {
            transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 1);
            m_LobbyButton.onClick.AddListener(()=> AudioLevelManager.Instance.ReturnToLobby());
        }
    }
}
