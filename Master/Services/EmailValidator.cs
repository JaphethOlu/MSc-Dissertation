using System;

using Master.Interfaces.Services;

namespace Master.Services
{
    public class EmailValidator : IEmailValidator
    {
        
        private string Email;

        public EmailValidator(string email)
        {
            Email = email;
        }

        public bool IsValidEmail()
        {
            try {
                var addr = new System.Net.Mail.MailAddress(Email);
                return true; //addr.Address == email;
            }
            catch 
            {
                return false;
            }
        }
    }
}