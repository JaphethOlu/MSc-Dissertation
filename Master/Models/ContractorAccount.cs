using System;

namespace Master.Models
{
    public class ContractorAccount
    {
        private string _EmailAddress;
        private string _Password;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ContractorAccount(string emailAddress, string password)
        {
            _EmailAddress = emailAddress;
            _Password = password;
        }

        public ContractorAccount(string emailAddress, string password, string firstName, string lastName)
        {
            _EmailAddress = emailAddress;
            _Password = password;
            FirstName = firstName;
            LastName = lastName;
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
    }
}