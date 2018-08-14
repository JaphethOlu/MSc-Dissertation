using System;
using System.ComponentModel.DataAnnotations;

using Master.Interfaces.Models;

namespace Master.Models
{
    public class ContractorAccount : IContractorAccount
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

        public ContractorAccount()
        {

        }
        
        public ContractorAccount(string emailAddress, string password, string firstName, string lastName)
        {
            EmailAddress = emailAddress;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
	}
}