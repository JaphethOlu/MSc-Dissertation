using System;
using NUnit;
using NUnit.Framework;

using Master;
using Master.Models;
using Master.Services;

namespace Master.Tests.Services
{
    [TestFixture]
    public class PasswordManagerTests
    {
        PasswordManager passwordManager;

        string truePassword = "ChocolateMilk";

        string falsePassword = "StrawberryMilk";

        [SetUp]
        public void CreatePasswordManager()
        {
            passwordManager = new PasswordManager();
        }

        [Test]
        public void TestTruePassword()
        {
            string trueHashPassword = passwordManager.GeneratePassword(truePassword);

            Assert.True(passwordManager.VerifyPassword(trueHashPassword, truePassword));
        }

        [Test]
        public void TestFalsePassword()
        {
            string trueHashPassword = passwordManager.GeneratePassword(truePassword);

            Assert.False(passwordManager.VerifyPassword(trueHashPassword, falsePassword));
        }
    }
}