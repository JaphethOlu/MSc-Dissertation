using System;

namespace Master.Model
{
    public class User
    {
        private string EmailAddress{ get; }
        private string Password;
        private UserRole Role{ get; }

        public User(string emailAddress, string password, UserRole role)
        {
            EmailAddress = emailAddress;
            Password = password;
            Role = role;
        }
    }
}