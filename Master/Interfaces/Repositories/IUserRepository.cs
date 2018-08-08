using System;
using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IUserRepository
    {
        // TODO: Presave Action Password Encryption
        void SaveUserAccount(UserAccount newUser);
        UserAccount FindUserAccount(string emailAddress);
        void DeleteUserAccount(string emailAddress);
    }
}