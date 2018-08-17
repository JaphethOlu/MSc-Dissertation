using System;

namespace Master.Interfaces.Models
{
    public interface ILogin
    {
        string EmailAddress { get; set; }
        string Password { get; set; }
    }
}