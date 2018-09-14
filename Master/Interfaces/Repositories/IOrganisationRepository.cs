using System;

using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IOrganisationRepository
    {
        void SaveNewOrganisation(Organisation organisation);

        Organisation FindOrganisationById(int organisationId);

        Organisation FindOrganisationByName(string organisationName);
    }
}