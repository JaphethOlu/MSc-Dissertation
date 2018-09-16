using System;
using System.ComponentModel.DataAnnotations;

using Master.Interfaces.Models;

namespace Master.Models
{
    public class RecruiterAccount : IAccount
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        

        [Required]
        [Range(101100, 2147483647)]
        public int OrganisationId { get; set; }

        public RecruiterAccount()
        {

        }

        public RecruiterAccount(string emailAddress, string password,
                                string firstName, string lastName, int orgId)
        {
            EmailAddress = emailAddress;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            OrganisationId = orgId;
        }

        public Organisation Organisation { get; set; }
    }
}