using System;
using Microsoft.EntityFrameworkCore;

using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IRecruiterAccountRepository
    {
        void SaveNewRecruiterAccount(RecruiterAccount newRecruiterAccount);
        RecruiterAccount FindRecruiterAccount(string emailAddress);
        void DeleteRecruiterAccount(string emailAddress);
        bool CheckIfAccountExist(string emailAddress);
        void MarkAsModified(RecruiterAccount recruiterAccount);
    }
}