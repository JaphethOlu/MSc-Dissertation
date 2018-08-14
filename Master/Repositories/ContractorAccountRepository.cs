using System;
using Microsoft.EntityFrameworkCore;

using Master.Models;
using Master.Contexts;
using Master.Interfaces.Repositories;

namespace Master.Repositories
{
    public class ContractorAccountRepository : IContractorAccountRepository
    {
        private readonly DissertationContext DbContext;

        public ContractorAccountRepository(DissertationContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveContractorAccount(ContractorAccount newContractorAccount)
        {
			DbContext.ContractorAccounts.Add(newContractorAccount);
            DbContext.SaveChanges();
        }

        public ContractorAccount FindContractorAccount(string emailAddress)
        {
			ContractorAccount contractor = DbContext.ContractorAccounts.Find(emailAddress);
            return contractor;
        }

        public void DeleteContractorAccount(string emailAddress)
        {
            ContractorAccount userToDelete = FindContractorAccount(emailAddress);
            DbContext.ContractorAccounts.Remove(userToDelete);
            DbContext.SaveChanges();
        }

        public bool CheckIfAccountExist(string emailAddress)
        {
            if(FindContractorAccount(emailAddress) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MarkAsModified(ContractorAccount contractorAccount)
        {
            DbContext.Entry(contractorAccount).State = EntityState.Modified;
        }
    }
}