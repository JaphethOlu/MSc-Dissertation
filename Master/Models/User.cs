using System;

namespace Master.Model
{
    public class User
    {
        private string _EmailAddress;
        private string _Password;
        private UserRole _Role;

        public User(string emailAddress, string password, UserRole role)
        {
            _EmailAddress = emailAddress;
            _Password = password;
            _Role = role;
        }

        public string EmailAddress
		{
			get
			{
				return _EmailAddress;
			}
		}

		public string Password
		{
			get
			{
				return _Password;
			}
		}

        public UserRole Role
        {
            get
            {
                return _Role;
            }
        }
    }
}