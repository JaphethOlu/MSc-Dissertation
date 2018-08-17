using System;

using Master.Interfaces.Models;

namespace Master.Models
{
    public class RecruiterAccount : IAccount
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OrganisationId { get; set; }

        public RecruiterAccount(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public RecruiterAccount(string emailAddress, string password,
                                string firstName, string lastName)
        {
            EmailAddress = emailAddress;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public Organisation Organisation { get; set; }
    }
}