using System;
using Microsoft.EntityFrameworkCore;

using Master.Models;
using Master.Contexts;
using Master.Interfaces.Repositories;

namespace Master.Repositories
{
    public class DirectorAccountRepository : IDirectorAccountRepository
    {
        private readonly DissertationContext DbContext;

        public DirectorAccountRepository(DissertationContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveNewDirectorAccount(DirectorAccount directorAccount)
        {
			DbContext.DirectorAccounts.Add(directorAccount);
            DbContext.SaveChanges();
        }

        public DirectorAccount FindDirectorAccount(string emailAddress)
        {
			DirectorAccount director = DbContext.DirectorAccounts.Find(emailAddress);
            return director;
        }

        public void DeleteDirectorAccount(string emailAddress)
        {
            DirectorAccount userToDelete = FindDirectorAccount(emailAddress);
            DbContext.DirectorAccounts.Remove(userToDelete);
            DbContext.SaveChanges();
        }

        public bool CheckIfAccountExist(string emailAddress)
        {
            if(FindDirectorAccount(emailAddress) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MarkAsModified(DirectorAccount directorAccount)
        {
            DbContext.Entry(directorAccount).State = EntityState.Modified;
        }
    }
}