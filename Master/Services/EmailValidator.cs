using System;

using Master.Interfaces.Services;

namespace Master.Services
{
    public class EmailValidator : IEmailValidator
    {
        public EmailValidator()
        {

        }

        public bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return true; //addr.Address == email;
            }
            catch 
            {
                return false;
            }
        }
    }
}