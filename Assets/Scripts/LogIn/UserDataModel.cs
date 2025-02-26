namespace KidsTest
{
    public class UserDataModel
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }
    }
}
