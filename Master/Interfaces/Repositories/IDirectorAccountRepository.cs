using System;
using Microsoft.EntityFrameworkCore;

using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IDirectorAccountRepository
    {
        void SaveNewDirectorAccount(DirectorAccount newDirectorAccount);
        DirectorAccount FindDirectorAccount(string emailAddress);
        void DeleteDirectorAccount(string emailAddress);
        bool CheckIfAccountExist(string emailAddress);
        void MarkAsModified(DirectorAccount directorAccount);
    }
}