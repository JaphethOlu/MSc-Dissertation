using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Master.Models;

namespace Master.Interfaces.Repositories
{
    public interface IContractRepository
    {
        void SaveNewContract(Contract contract);
        Contract FindContractById(int contractId);
        List<Contract> FindContractsByPosition(string position);
        void DeleteContract(int contractId);
    }
}