using System;
using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IUserRepository
    {
        // TODO: Presave Action Password Encryption
        void SaveUser(UserAccount newUser);
    }
}