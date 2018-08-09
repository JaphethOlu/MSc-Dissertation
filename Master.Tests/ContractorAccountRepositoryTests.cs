using System;
using Xunit;
using Master;
using Master.Models;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Repositories;

namespace Tests
{
    public class ContractorAccountRepositoryTests
    {
        IContractorAccountRepository ContractorAccountRepository;
        DissertationContext dissertationContext = new DissertationContext();
        ContractorAccount NewContractorAccount;
        string ExampleEmailAddress = "example@email.com";
        
        public ContractorAccountRepositoryTests()
        {
            ContractorAccountRepository = new ContractorAccountRepository(dissertationContext);
            NewContractorAccount = new ContractorAccount(ExampleEmailAddress, "IAmAContractor", "John", "Doe");
        }

        [Fact]
        public void TestDeleteContractor()
        {
            ContractorAccountRepository.DeleteContractorAccount(ExampleEmailAddress);
            Assert.Null(ContractorAccountRepository.FindContractorAccount(ExampleEmailAddress));
        }

        [Theory]
        [InlineData("someemail@email.com")]
        [InlineData("nonexistent@email.com")]
        [InlineData("hello@email.com")]
        public void TestFindFakeContractorAccounts(string email)
        {
            ContractorAccount foundAccount;
            foundAccount = ContractorAccountRepository.FindContractorAccount(email);
            Assert.Null(foundAccount);
        }
        
        [Fact]
        public void TestSaveContractor()
        {
            ContractorAccountRepository.SaveContractorAccount(NewContractorAccount);
            ContractorAccount savedAccount = ContractorAccountRepository.FindContractorAccount("example@email.com");
            Assert.NotNull(savedAccount);
        }
    }
}