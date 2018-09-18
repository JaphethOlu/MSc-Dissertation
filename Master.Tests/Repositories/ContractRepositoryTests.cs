using static System.Console;

using NUnit.Framework;

using Master.Models;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Repositories;

namespace Tests.Repositories
{
    [TestFixture]
    public class ContractRepositoryTests
    {
        DissertationContext DbContext = new DissertationContext();
        ContractRepository Repository;
        Contract TestContract = new Contract();
        int TestContractId = 10000;

        public ContractRepositoryTests()
        {
            Repository = new ContractRepository(DbContext);
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
        [Ignore("Tested To Ensure DBConnection and functionality")]
        public void TestDeleteContract()
        {
            Repository.DeleteContract(TestContractId);
            Contract savedContract = Repository.FindContractById(TestContractId);
            Assert.Null(savedContract);
        }
        
        [Test]
        [Ignore("Tested To Ensure DBConnection and functionality")]
        public void TestSaveContract()
        {
            Repository.SaveNewContract(TestContract);
            Contract savedContract = Repository.FindContractById(TestContractId);
            Assert.NotNull(savedContract);
            Assert.NotNull(savedContract.DateAdded);
        }
    }
}