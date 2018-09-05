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
        PasswordManager PasswordManager;

        string TruePassword = "ChocolateMilk";

        string FalsePassword = "StrawberryMilk";

        [SetUp]
        public void CreatePasswordManager()
        {
            PasswordManager = new PasswordManager();
        }

        [Test]
        public void TestTruePassword()
        {
            string trueHashPassword = PasswordManager.GeneratePassword(TruePassword);

            Assert.True(PasswordManager.VerifyPassword(trueHashPassword, TruePassword));
        }

        [Test]
        public void TestFalsePassword()
        {
            string trueHashPassword = PasswordManager.GeneratePassword(TruePassword);

            Assert.False(PasswordManager.VerifyPassword(trueHashPassword, FalsePassword));
        }
    }
}