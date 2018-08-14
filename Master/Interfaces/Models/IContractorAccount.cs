using System;

namespace Master.Interfaces.Models
{
    public interface IContractorAccount
    {
        string EmailAddress { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}