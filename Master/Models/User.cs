using System;

namespace Master.Models
{
    public class UserAccount
    {
        private string _EmailAddress;
        private string _Password;
        private Role _UserRole;

        public UserAccount(string emailAddress, string password, Role userRole)
        {
            _EmailAddress = emailAddress;
            _Password = password;
            _UserRole = userRole;
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

        public Role UserRole
        {
            get
            {
                return _UserRole;
            }

			set
			{
				_UserRole = value;
			}
        }
    }
}