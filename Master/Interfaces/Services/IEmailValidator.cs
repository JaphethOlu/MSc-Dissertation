using System;

namespace Master.Interfaces.Services
{
    public interface IEmailValidator
    {
        bool IsValidEmail(string email);
    }
}