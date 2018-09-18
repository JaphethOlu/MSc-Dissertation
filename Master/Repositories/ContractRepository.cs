using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Master.Models;
using Master.Contexts;
using Master.Interfaces.Repositories;

namespace Master.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly DissertationContext DbContext;

        public ContractRepository(DissertationContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveNewContract(Contract newContract)
        {
            DbContext.Contracts.Add(newContract);
            DbContext.SaveChanges();

            OrganisationRepository HelperRepository = new OrganisationRepository(DbContext);
            HelperRepository.IncreaseNumberOfContracts(newContract.OrganisationId);
        }

        public Contract FindContractById(int contractId)
        {
            Contract contract = DbContext.Contracts.Find(contractId);
            return contract;
        }

        public List<Contract> FindContractsByPosition(string position)
        {
            List<Contract> contracts = DbContext.Contracts
                                                .Where(c => c.JobTitle.Contains(position))
                                                .ToList();

            return contracts;
        }

        public void DeleteContract(int contractId)
        {
            Contract contract = FindContractById(contractId);
            DbContext.Contracts.Remove(contract);
            DbContext.SaveChanges();

            OrganisationRepository HelperRepository = new OrganisationRepository(DbContext);
            HelperRepository.DecreaseNumberOfContracts(contract.OrganisationId);
        }
    }
}