using static System.Console;

using NUnit.Framework;

using Master.Models;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Repositories;

namespace Tests.Repositories
{
    [TestFixture]
    [Ignore("Tested to ensure functional database mapping")]
    public class ContractRepositoryTests
    {
        DissertationContext DbContext = new DissertationContext();
        ContractRepository ContractRepository;
        Contract TestContract = new Contract();
        int TestContractId = 10000;

        public ContractRepositoryTests()
        {
            ContractRepository = new ContractRepository(DbContext);
        }

        [OneTimeSetUp]
        public void ContractRepositoryTestSetup()
        {
            TestContract = new Contract
            {
                JobTitle = "System Architect",
                OrganisationId = 101100,
                Location = "NewCastle",
                Duration = 6,
                MaximumSalary = 250,
                MinimumSalary = 150
            };
        }
        
        [Test]
        public void TestDeleteContract()
        {
            ContractRepository.DeleteContract(TestContractId);
            Contract savedContract = ContractRepository.FindContractById(TestContractId);
            Assert.Null(savedContract);
        }
        
        [Test]
        public void TestSaveContract()
        {
            ContractRepository.SaveNewContract(TestContract);
            Contract savedContract = ContractRepository.FindContractById(TestContractId);
            Assert.NotNull(savedContract);
            Assert.NotNull(savedContract.DateAdded);
        }
    }
}