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
    public class ContractorAccountRepositoryTests
    {
        IContractorAccountRepository Repository;
        DissertationContext DissertationContext = new DissertationContext();
        ContractorAccount NewContractorAccount;
        string ExampleEmailAddress = "example@email.com";
        string FakeEmailAddress = "nonexistent@email.com";
        
        public ContractorAccountRepositoryTests()
        {
            Repository = new ContractorAccountRepository(DissertationContext);
            NewContractorAccount = new ContractorAccount(ExampleEmailAddress, "IAmAContractor", "John", "Doe");
        }
        
        [Test]
        [Ignore("Tested To Ensure DBConnection and functionality")]
        public void TestDeleteContractor()
        {
            Repository.DeleteContractorAccount(ExampleEmailAddress);
            Assert.Null(Repository.FindContractorAccount(ExampleEmailAddress));
        }

        [Test]
        public void TestFindFakeContractorAccounts()
        {
            ContractorAccount foundAccount;
            foundAccount = Repository.FindContractorAccount(FakeEmailAddress);
            Assert.Null(foundAccount);
        }
        
        [Test]
        [Ignore("Tested To Ensure DBConnection and functionality")]
        public void TestSaveContractor()
        {
            Repository.SaveNewContractorAccount(NewContractorAccount);
            ContractorAccount savedAccount = Repository.FindContractorAccount("example@email.com");
            Assert.NotNull(savedAccount);
        }
    }
}