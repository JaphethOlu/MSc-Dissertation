using System;
using NUnit;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Master;
using Master.Models;
using Master.Services;
using Master.Contexts;
using Master.Controllers;
using Master.Repositories;
using Master.Interfaces.Models;
using Master.Interfaces.Repositories;

namespace Tests.Controllers
{
    [TestFixture]
    public class ContractorControllerTests
    {
        ContractorAccountController controller;
        ContractorAccountRepository contractorAccountRepository;
        ContractorAccount contractorAccount = new ContractorAccount();
        PasswordHasher passwordHasher;
        ContractorAccount trueContractor;
        ContractorAccount falseEmailContractor;
        ContractorAccount existingContractor;

        public ContractorControllerTests()
        {
            controller = new ContractorAccountController(contractorAccountRepository, contractorAccount);
        }

        [OneTimeSetUp]
        public void SetupContractorAccounts()
        {
            trueContractor = new ContractorAccount
            {
                EmailAddress = "johndoe@example.com",
                Password = "IAmAContractor",
                FirstName = "James",
                LastName = "Bond"
            };
            
            falseEmailContractor = new ContractorAccount
            {
                EmailAddress = "agsdzs",
                Password = "IAmAContractor",
                FirstName = "Jason",
                LastName = "Bourne"
            };

            existingContractor = new ContractorAccount
            {
                EmailAddress = "bourneCoder@example.com",
                Password = "IAmAContractor",
                FirstName = "Jason",
                LastName = "Bourne"
            };
        }

        [OneTimeTearDown]
        public void RemoveSetupAccount()
        {
            DissertationContext dbContext = new DissertationContext();
            ContractorAccountRepository repo = new ContractorAccountRepository(dbContext);
            repo.DeleteContractorAccount(trueContractor.EmailAddress);
        }

        [Test]
        public void TrueContractorAccount()
        {
            var actualResult = controller.CreateContractorAccount(trueContractor, passwordHasher);

            var expectedResult = new HttpResponseMessage(HttpStatusCode.Created);

            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }
        
        [Test]
        public void FalseEmailContractorAccount()
        {
            var actualResult = controller.CreateContractorAccount(falseEmailContractor, passwordHasher);

            var expectedResult = new HttpResponseMessage(HttpStatusCode.Forbidden);

            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }
        
        [Test]
        public void ExisitingContractorAccount()
        {
            var actualResult = controller.CreateContractorAccount(existingContractor, passwordHasher);

            var expectedResult = new HttpResponseMessage(HttpStatusCode.Forbidden);
            
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }
    }
}