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
        int TestContractId = 10600;

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
            OrganisationRepository HelperRepository = new OrganisationRepository(DbContext);

            Organisation organisation = HelperRepository.FindOrganisationById(TestContract.OrganisationId);
            ushort? previousNumberOfContracts = organisation.NumberOfContracts;

            Repository.DeleteContract(TestContractId);
            Contract savedContract = Repository.FindContractById(TestContractId);

            organisation = HelperRepository.FindOrganisationById(TestContract.OrganisationId);
            ushort? currentNumberOfContracts = organisation.NumberOfContracts;
            
            Assert.Multiple(() => 
            {
                Assert.Null(savedContract);
                Assert.Less(currentNumberOfContracts, previousNumberOfContracts);
            });
        }
        
        [Test]
        [Ignore("Tested To Ensure DBConnection and functionality")]
        public void TestSaveContract()
        {
            OrganisationRepository HelperRepository = new OrganisationRepository(DbContext);

            Organisation organisation = HelperRepository.FindOrganisationById(TestContract.OrganisationId);
            ushort? previousNumberOfContracts = organisation.NumberOfContracts;

            Repository.SaveNewContract(TestContract);

            organisation = HelperRepository.FindOrganisationById(TestContract.OrganisationId);
            ushort? currentNumberOfContracts = organisation.NumberOfContracts;

            Contract savedContract = Repository.FindContractById(TestContractId);

            Assert.Multiple(() => {
                Assert.NotNull(savedContract);
                Assert.NotNull(savedContract.DateAdded);
                Assert.Greater(currentNumberOfContracts, previousNumberOfContracts);
            });
        }
    }
}