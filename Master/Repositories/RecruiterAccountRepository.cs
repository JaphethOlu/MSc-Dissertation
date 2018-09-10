using System;
using Microsoft.EntityFrameworkCore;

using Master.Models;
using Master.Contexts;
using Master.Interfaces.Repositories;

namespace Master.Repositories
{
    public class RecruiterAccountRepository : IRecruiterAccountRepository
    {
        private readonly DissertationContext DbContext;

        public RecruiterAccountRepository(DissertationContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveNewRecruiterAccount(RecruiterAccount recruiterAccount)
        {
			DbContext.RecruiterAccounts.Add(recruiterAccount);
            DbContext.SaveChanges();
        }

        public RecruiterAccount FindRecruiterAccount(string emailAddress)
        {
			RecruiterAccount recruiter = DbContext.RecruiterAccounts.Find(emailAddress);
            return recruiter;
        }

        public void DeleteRecruiterAccount(string emailAddress)
        {
            RecruiterAccount userToDelete = FindRecruiterAccount(emailAddress);
            DbContext.RecruiterAccounts.Remove(userToDelete);
            DbContext.SaveChanges();
        }

        public bool CheckIfAccountExist(string emailAddress)
        {
            if(FindRecruiterAccount(emailAddress) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MarkAsModified(RecruiterAccount recruiterAccount)
        {
            DbContext.Entry(recruiterAccount).State = EntityState.Modified;
        }
    }
}