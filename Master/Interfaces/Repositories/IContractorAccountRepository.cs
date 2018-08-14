using System;
using Microsoft.EntityFrameworkCore;

using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IContractorAccountRepository
    {
        void SaveContractorAccount(ContractorAccount newContractorAccount);
        ContractorAccount FindContractorAccount(string emailAddress);
        void DeleteContractorAccount(string emailAddress);
        bool CheckIfAccountExist(string emailAddress);
        void MarkAsModified(ContractorAccount contractorAccount);
    }
}