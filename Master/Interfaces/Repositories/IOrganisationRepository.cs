using System;
using System.Collections.Generic;

using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IOrganisationRepository
    {
        void SaveNewOrganisation(Organisation organisation);

        Organisation FindOrganisationById(int organisationId);

        Organisation FindOrganisationByName(string organisationName);

        void IncreaseNumberOfContracts(int organisationId);

        void DecreaseNumberOfContracts(int organisationId);

        List<Organisation> GetMostContractsByAgency();

        List<Organisation> GetMostContractsByEmployer();

    }
}