using System;
using Microsoft.EntityFrameworkCore;

using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IContractorProfileRepository
    {
        void SaveNewContractorProfile(ContractorProfile contractorProfile);

        ContractorProfile FindContractorProfile(string emailAddress);
        void DeleteContractorProfile(string emailAddress);
    }
}