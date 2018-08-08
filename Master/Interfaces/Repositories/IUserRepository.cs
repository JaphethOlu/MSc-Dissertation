using System;
using Master.Model;

namespace Master.Interfaces.Repositories
{
    public interface IUserRepository
    {
        // TODO: Presave Action Password Encryption
        void SaveUser(User newUser);
    }
}