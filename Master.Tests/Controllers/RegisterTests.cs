using System;
using NUnit;
using NUnit.Framework;
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
using Microsoft.Extensions.Configuration;

namespace Tests.Controllers
{
    [TestFixture]
    public class RegisterTests
    {
        RegisterController controller;
        ContractorAccountRepository contractorAccountRepository;
        ContractorAccount contractorAccount = new ContractorAccount();
        PasswordManager passwordManager;
        TokenGenerator tokenGenerator;
        ContractorAccount trueContractor;
        ContractorAccount falseEmailContractor;
        ContractorAccount existingContractor;        

        public RegisterTests()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
            tokenGenerator = new TokenGenerator(config);
            controller = new RegisterController(contractorAccountRepository, contractorAccount, tokenGenerator);
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
            IActionResult actualResult = controller.RegisterContractor(trueContractor, passwordManager);
            var resultContent = actualResult as OkObjectResult;

            Assert.IsNotNull(resultContent);
            Assert.IsInstanceOf(typeof(OkObjectResult), actualResult);
            Assert.AreEqual(200, resultContent.StatusCode);
        }
        
        
        [Test]
        public void FalseEmailContractorAccount()
        {
            IActionResult actualResult = controller.RegisterContractor(falseEmailContractor, passwordManager);
            var resultContent = actualResult as BadRequestObjectResult;

            Assert.IsNotNull(resultContent);
            Assert.AreEqual(400, resultContent.StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), actualResult);
        }
        
        [Test]
        public void ExisitingContractorAccount()
        {
            IActionResult actualResult = controller.RegisterContractor(existingContractor, passwordManager);
            var resultContent = actualResult as BadRequestObjectResult;

            Assert.IsNotNull(resultContent);
            Assert.AreEqual(400, resultContent.StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), actualResult);
        }
    }
}