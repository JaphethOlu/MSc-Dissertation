using System;
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
			//DbContext.Users.Add(newContractorAccount);
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
    }
}