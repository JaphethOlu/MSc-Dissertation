using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Master.Models;
using Master.Contexts;
using Master.Interfaces.Repositories;

namespace Master.Repositories
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly DissertationContext DbContext;

        public OrganisationRepository(DissertationContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveNewOrganisation(Organisation organisation)
        {
            DbContext.Organisations.Add(organisation);
            DbContext.SaveChanges();
        }
        
        public void DeleteOrganisation(string organisationName)
        {
            Organisation organisation = FindOrganisationByName(organisationName);
            DbContext.Remove(organisation);
            DbContext.SaveChanges();
        }

        public Organisation FindOrganisationById(int organisationId)
        {
            Organisation organisation = DbContext.Organisations.Find(organisationId);
            return organisation;
        }

        public Organisation FindOrganisationByName(string organisationName)
        {            
            Organisation organisation = DbContext.Organisations.SingleOrDefault(o => o.OrganisationName.Contains(organisationName));
            return organisation;
        }
    }
}