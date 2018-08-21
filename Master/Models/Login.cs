using System;

using Master.Interfaces.Models;

namespace Master.Models
{
    public class Login : ILogin
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public Login()
        {

        }
    }
}