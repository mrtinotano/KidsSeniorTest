using KidsTest.Utils;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KidsTest
{
    public class LogInManager : Singleton<LogInManager>
    {
        public bool TryLogIn(string email, string password, out string logInMessage)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    throw new System.Exception("Missing Email or Password");

                if (!ValidateEmail(email))
                    throw new System.Exception("Invalid email");

            }
            catch (System.Exception e)
            {
                logInMessage = e.Message;
                return false;
            }

            logInMessage = "Log In Successful";
            SceneManager.Instance.LoadLobbyScene();

            return true;
        }

        private bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
