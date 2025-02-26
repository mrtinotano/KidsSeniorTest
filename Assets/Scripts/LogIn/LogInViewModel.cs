using KidsTest.Utils;
using System;

namespace KidsTest
{
    public struct LogInSuccessEvent { }

    public class LogInViewModel
    {
        private UserDataModel UserData;

        public Action<string> OnLogInError;
        public Action<string> OnLogInSuccess;

        public LogInViewModel()
        {
            UserData = new UserDataModel();
        }

        public void OnEmailFieldValueChanged(string email) => UserData.SetEmail(email);
        public void OnPasswordFieldValueChanged(string password) => UserData.SetPassword(password);

        public void OnLogInSubmit()
        {
            TryLogIn();
        }

        private void TryLogIn()
        {
            if (LogInManager.TryLogIn(UserData.Email, UserData.Password, out string outputMessage))
            {
                OnLogInSuccess?.Invoke(outputMessage);
                EventManager<LogInSuccessEvent>.TriggerEvent(new LogInSuccessEvent());
            }
            else
                OnLogInError?.Invoke(outputMessage);
        }
    }
}
