using TMPro;
using UnityEngine;

namespace KidsTest
{
    public class LogInView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_EmailField;
        [SerializeField] private TMP_InputField m_PasswordField;
        [SerializeField] private UIButton m_LogInButton;
        [SerializeField] private TMP_Text m_LogInMessage;

        private LogInViewModel m_ViewModel;

        private readonly Color m_ErrorMessageColor = Color.red;
        private readonly Color m_SuccessMessageColor = Color.green;

        private void Awake()
        {
            m_ViewModel = new LogInViewModel();
            m_ViewModel.OnLogInSuccess += ShowLogInSuccessMessage;
            m_ViewModel.OnLogInError += ShowLogInErrorMessage;

            m_LogInButton.onClick.AddListener(()=> m_ViewModel.OnLogInSubmit());
            m_EmailField.onValueChanged.AddListener((email)=> m_ViewModel.OnEmailFieldValueChanged(email));
            m_PasswordField.onValueChanged.AddListener((password) => m_ViewModel.OnPasswordFieldValueChanged(password));
        }

        private void OnDestroy()
        {
            m_ViewModel.OnLogInSuccess -= ShowLogInSuccessMessage;
            m_ViewModel.OnLogInError -= ShowLogInErrorMessage;
        }

        private void ShowLogInSuccessMessage(string message)
        {
            m_LogInButton.interactable = false;
            m_LogInMessage.text = message;
            m_LogInMessage.color = m_SuccessMessageColor;
        }

        private void ShowLogInErrorMessage(string message)
        {
            m_LogInMessage.text = message;
            m_LogInMessage.color = m_ErrorMessageColor;
        }
    }
}
