using System;
using Microsoft.EntityFrameworkCore;

using Master.Models;
using Master.Contexts;
using Master.Interfaces.Repositories;

namespace Master.Repositories
{
    public class ContractorProfileRepository : IContractorProfileRepository
    {
        private readonly DissertationContext DbContext;

        public ContractorProfileRepository(DissertationContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveNewContractorProfile(ContractorProfile contractorProfile)
        {
            DbContext.ContractorProfiles.Add(contractorProfile);
            DbContext.SaveChanges();
        }

        public ContractorProfile FindContractorProfile(string emailAddress)
        {
            ContractorProfile contractorProfile = DbContext.ContractorProfiles.Find(emailAddress);
            return contractorProfile;
        }

		public void DeleteContractorProfile(string emailAddress)
		{
			ContractorProfile profileToDelete = FindContractorProfile(emailAddress);
			DbContext.ContractorProfiles.Remove(profileToDelete);
			DbContext.SaveChanges();
		}

        public void MarkAsModified(ContractorProfile contractorProfile)
        {
            DbContext.Entry(contractorProfile).State = EntityState.Modified;
        }
    }   
}