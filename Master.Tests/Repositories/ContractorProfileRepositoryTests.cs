using NUnit.Framework;

using Master.Models;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Repositories;

namespace Tests.Repositories
{
    [TestFixture]
    [Ignore("Tested to ensure functional database mapping")]
    public class ContractorProfileRepositoryTests
    {
        IContractorProfileRepository Repository;
        DissertationContext DissertationContext = new DissertationContext();
        ContractorProfile ContractorProfile;
        string ContractorAccountEmail = "bourneCoder@example.com";

        public ContractorProfileRepositoryTests()
        {
            Repository = new ContractorProfileRepository(DissertationContext);
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
            Repository.DeleteContractorProfile(ContractorAccountEmail);
        }

        [Test]
        public void TestSaveContractorProfile()
        {
            Repository.SaveNewContractorProfile(ContractorProfile);
            ContractorProfile savedContractorProfile = Repository.FindContractorProfile(ContractorAccountEmail);
            Assert.NotNull(savedContractorProfile);
        }
    }
}