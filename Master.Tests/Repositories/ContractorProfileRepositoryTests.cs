using NUnit.Framework;

using Master.Models;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Repositories;

namespace Tests.Repositories
{
    [TestFixture]
    public class ContractorProfileRepositoryTests
    {
        IContractorProfileRepository ContractorProfileRepository;
        DissertationContext DissertationContext = new DissertationContext();
        ContractorProfile ContractorProfile;
        string ContractorAccountEmail = "bourneCoder@example.com";

        public ContractorProfileRepositoryTests()
        {
            ContractorProfileRepository = new ContractorProfileRepository(DissertationContext);
        }
        
        [OneTimeSetUp]
        public void SetUpContractorProfile()
        {
            ContractorProfile = new ContractorProfile {
                EmailAddress = ContractorAccountEmail,
                FirstName = "Jason",
                LastName = "Bourne"
            };
        }

        [OneTimeTearDown]
        public void TearDownContractorProfile()
        {
            ContractorProfileRepository.DeleteContractorProfile(ContractorAccountEmail);
        }

        [Test]
        public void TestSaveContractorProfile()
        {
            ContractorProfileRepository.SaveNewContractorProfile(ContractorProfile);
            ContractorProfile savedContractorProfile = ContractorProfileRepository.FindContractorProfile(ContractorAccountEmail);
            Assert.NotNull(savedContractorProfile);
        }
    }
}