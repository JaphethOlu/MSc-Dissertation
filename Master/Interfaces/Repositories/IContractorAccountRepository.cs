using System;
using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IContractorAccountRepository
    {
        // TODO: Presave Action Password Encryption
        void SaveContractorAccount(ContractorAccount newUser);
        ContractorAccount FindContractorAccount(string emailAddress);
        void DeleteContractorAccount(string emailAddress);
    }
}