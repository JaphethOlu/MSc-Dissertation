using System;

namespace Master.Interfaces.Models
{
    public interface IUser
    {
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Role { get; }
    }
}