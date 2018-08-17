using System;

using Master.Interfaces.Models;

namespace Master.Models
{
    public class Contractor : IUser
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role
        {
            get
            {
                return "contractor";
            }
        }

        public Contractor()
        {

        }
    }
}