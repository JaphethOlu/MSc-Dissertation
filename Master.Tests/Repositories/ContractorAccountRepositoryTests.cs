using System;
using NUnit.Framework;

using Master;
using Master.Models;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Repositories;

namespace Tests.Repositories
{
    [TestFixture]
    [Ignore("Tested to ensure functional database mapping")]
    public class ContractorAccountRepositoryTests
    {
        IContractorAccountRepository ContractorAccountRepository;
        DissertationContext DissertationContext = new DissertationContext();
        ContractorAccount NewContractorAccount;
        string ExampleEmailAddress = "example@email.com";
        string FakeEmailAddress = "nonexistent@email.com";
        
        public ContractorAccountRepositoryTests()
        {
            ContractorAccountRepository = new ContractorAccountRepository(DissertationContext);
            NewContractorAccount = new ContractorAccount(ExampleEmailAddress, "IAmAContractor", "John", "Doe");
        }
        
        [Test]
        public void TestDeleteContractor()
        {
            ContractorAccountRepository.DeleteContractorAccount(ExampleEmailAddress);
            Assert.Null(ContractorAccountRepository.FindContractorAccount(ExampleEmailAddress));
        }

        [Test]
        public void TestFindFakeContractorAccounts()
        {
            ContractorAccount foundAccount;
            foundAccount = ContractorAccountRepository.FindContractorAccount(FakeEmailAddress);
            Assert.Null(foundAccount);
        }
        
        [Test]
        public void TestSaveContractor()
        {
            ContractorAccountRepository.SaveNewContractorAccount(NewContractorAccount);
            ContractorAccount savedAccount = ContractorAccountRepository.FindContractorAccount("example@email.com");
            Assert.NotNull(savedAccount);
        }
    }
}