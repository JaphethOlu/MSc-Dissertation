using System;

namespace Master.Interfaces.Services
{
    public interface IPasswordManager
    {
        string GeneratePassword(string password);
        bool VerifyPassword(string truePassword, string providedPassword);
    }
}