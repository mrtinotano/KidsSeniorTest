using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class LogInView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_EmailField;
        [SerializeField] private TMP_InputField m_PasswordField;
        [SerializeField] private UIButton m_LogInButton;
        [SerializeField] private TMP_Text m_LogInMessage;

        private void Start()
        {
            m_LogInButton.onClick.AddListener(()=> TryLogIn());
        }

        private void TryLogIn()
        {
            string message;
            
            if (UserManager.Instance.TryLogIn(m_EmailField.text, m_PasswordField.text, out message))
            {
                m_LogInButton.interactable = false;
                m_LogInMessage.text = message;
                m_LogInMessage.color = Color.green;
            }
            else
            {
                m_LogInMessage.text = message;
                m_LogInMessage.color = Color.red;
            }
        }
    }
}
