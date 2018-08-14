using System;

namespace Master.Interfaces.Services
{
    public interface IPasswordHasher
    {
        string GeneratePassword(string password);
    }
}