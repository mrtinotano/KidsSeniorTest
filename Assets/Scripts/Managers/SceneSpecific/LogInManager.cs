using KidsTest.Utils;
using System.Net.Mail;
using UnityEngine;

namespace KidsTest
{
    public class LogInManager : Singleton<LogInManager>
    {
        [SerializeField, Scene] private string m_LobbyScene;

        public bool TryLogIn(string email, string password, out string logInMessage)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    throw new System.Exception("Missing Email");

                if (string.IsNullOrEmpty(password))
                    throw new System.Exception("Missing Password");

                new MailAddress(email);
            }
            catch (System.FormatException e)
            {
                logInMessage = "Email format is incorrect";
                return false;
            }
            catch (System.Exception e)
            {
                logInMessage = e.Message;
                return false;
            }

            logInMessage = "Log In Successful";
            SceneManager.Instance.LoadScene(m_LobbyScene);

            return true;
        }
    }
}
