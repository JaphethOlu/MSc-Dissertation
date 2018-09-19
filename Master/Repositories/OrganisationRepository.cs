using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Master.Models;
using Master.Contexts;
using Master.Interfaces.Repositories;

namespace Master.Repositories
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly DissertationContext DbContext;

        private const byte TopOrganisationsNumber = 5;

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
            DbContext.Organisations.Remove(organisation);
            DbContext.SaveChanges();
        }

        public Organisation FindOrganisationById(int organisationId)
        {
            Organisation organisation = DbContext.Organisations.Find(organisationId);
            return organisation;
        }

        public Organisation FindOrganisationByName(string organisationName)
        {            
            Organisation organisation = DbContext.Organisations
                                                 .SingleOrDefault(o => o.OrganisationName.Contains(organisationName));
            return organisation;
        }

        public void IncreaseNumberOfContracts(int organisationId)
        {
            Organisation organisation = FindOrganisationById(organisationId);
            organisation.NumberOfContracts += 1;
            DbContext.Organisations.Update(organisation);
            DbContext.SaveChanges();
        }

        public void DecreaseNumberOfContracts(int organisationId)
        {
            Organisation organisation = FindOrganisationById(organisationId);
            if(organisation.NumberOfContracts > 0)
            {
                organisation.NumberOfContracts -= 1;
                DbContext.Organisations.Update(organisation);
                DbContext.SaveChanges();
            }
        }

        public dynamic GetMostContractsByAgency()
        {
            var topAgencies = DbContext.Organisations
                                    .Where(o => o.OrganisationType == OrganisationType.Agency)
                                    .OrderByDescending(o => o.NumberOfContracts)
                                    .Take(TopOrganisationsNumber)
                                    .Select(o => new
                                            {
                                                organisationId = o.OrganisationId,
                                                organisationName = o.OrganisationName,
                                                organisationType = o.OrganisationType,
                                                statement = o.OrganisationStatement,
                                                location = o.Location,
                                                numberOfContracts = o.NumberOfContracts
                                            })
                                    .ToList();

            return topAgencies;
        }

        public dynamic GetMostContractsByEmployer()
        {
            var topEmployers = DbContext.Organisations
                                    .Where(o => o.OrganisationType == OrganisationType.Agency)
                                    .OrderByDescending(o => o.NumberOfContracts)
                                    .Take(TopOrganisationsNumber)
                                    .Select(o => new
                                    {
                                        organisationId = o.OrganisationId,
                                        organisationName = o.OrganisationName,
                                        organisationType = o.OrganisationType,
                                        statement = o.OrganisationStatement,
                                        location = o.Location,
                                        numberOfContracts = o.NumberOfContracts
                                    })
                                    .ToList();

            return topEmployers;
        }
    }
}