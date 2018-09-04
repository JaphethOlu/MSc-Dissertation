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
        LoginController Controller;
        ContractorAccountRepository ContractorAccountRepository;
        EmailValidator EmailValidator;
        TokenGenerator TokenGenerator;
        ContractorAccount Account;
        Login Login;
        Login TrueContractor;
        Login FalseContractor;
        Login InvalidEmailContractor;
        Login NoAccountContractor;

        public LoginTests()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
            TokenGenerator = new TokenGenerator(config);
            Controller = new LoginController(ContractorAccountRepository, TokenGenerator);
        }

        [OneTimeSetUp]
        public void LoginTestsSetup()
        {
            TrueContractor = new Login
            {
                EmailAddress = "bourneCoder@example.com",
                Password = "TestPassword",
            };
            
            FalseContractor = new Login
            {
                EmailAddress = "bourneCoder@example.com",
                Password = "ThisIsAtestContractor",
            };

            InvalidEmailContractor = new Login
            {
                EmailAddress = "bourneCoder.example.com",
                Password = "TestPassword"
            };

            NoAccountContractor = new Login
            {
                EmailAddress = "johnDoe@example.com",
                Password = "TestPassword"
            };
        }

        [Test]
        public void TestTrueAuthContractor()
        {
            IActionResult actualResult = Controller.LoginContractor(TrueContractor);//, EmailValidator);
            var resultContent = actualResult as AcceptedResult;
            Assert.NotNull(actualResult);
            Assert.IsInstanceOf(typeof(AcceptedResult), actualResult);
            Assert.AreEqual(202, resultContent.StatusCode);
        }

        [Test]
        public void TestFalseAuthContractor()
        {
            IActionResult actualResult = Controller.LoginContractor(FalseContractor);//, EmailValidator);
            var resultContent = actualResult as UnauthorizedResult;
            Assert.NotNull(actualResult);
            Assert.IsInstanceOf(typeof(UnauthorizedResult), actualResult);
            Assert.AreEqual(401, resultContent.StatusCode);
        }

        [Test]
        public void TestInvalidEmailContractor()
        {
            IActionResult actualResult = Controller.LoginContractor(InvalidEmailContractor);//, EmailValidator);
            var resultContent = actualResult as UnauthorizedResult;
            Assert.NotNull(actualResult);
            Assert.IsInstanceOf(typeof(UnauthorizedResult), actualResult);
            Assert.AreEqual(401, resultContent.StatusCode);
        }

        [Test]
        public void TestNoAccountContractorLogin()
        {
            IActionResult actualResult = Controller.LoginContractor(NoAccountContractor);//, EmailValidator);
            var resultContent = actualResult as UnauthorizedResult;
            Assert.NotNull(actualResult);
            Assert.IsInstanceOf(typeof(UnauthorizedResult), actualResult);
            Assert.AreEqual(401, resultContent.StatusCode);
        }
    }
}