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
    public class LoginTests
    {
        LoginController controller;
        ContractorAccountRepository contractorAccountRepository;
        EmailValidator emailValidator;
        TokenGenerator tokenGenerator;
        ContractorAccount account;
        Login login;
        Login trueContractorLogin;
        Login falseContractorLogin;


        public LoginTests()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
            tokenGenerator = new TokenGenerator(config);
            controller = new LoginController(contractorAccountRepository, account, login, tokenGenerator);
        }

        [OneTimeSetUp]
        public void LoginTestsSetup()
        {
            trueContractorLogin = new Login
            {
                EmailAddress = "bourneCoder@example.com",
                Password = "ThisIsATestContractor",
            };
            
            falseContractorLogin = new Login
            {
                EmailAddress = "bourneCoder@example.com",
                Password = "ThisIsAtestContractor",
            };
        }

        [Test]
        public void TestTrueAuthContractor()
        {
            IActionResult actualResult = controller.LoginContractor(trueContractorLogin, emailValidator);
            var resultContent = actualResult as AcceptedResult;
            Assert.NotNull(actualResult);
            Assert.IsInstanceOf(typeof(AcceptedResult), actualResult);
            Assert.AreEqual(202, resultContent.StatusCode);
        }

        public void TestFalseAuthContractor()
        {
            IActionResult actualResult = controller.LoginContractor(falseContractorLogin, emailValidator);
            var resultContent = actualResult as UnauthorizedResult;
            Assert.NotNull(actualResult);
            Assert.IsInstanceOf(typeof(UnauthorizedResult), actualResult);
            Assert.AreEqual(401, resultContent.StatusCode);
        }
    }
}