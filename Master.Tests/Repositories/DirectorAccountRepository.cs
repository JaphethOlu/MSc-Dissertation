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
    public class DirectorAccountRepositoryTests
    {
        IDirectorAccountRepository Repository;
        DissertationContext DissertationContext = new DissertationContext();
        DirectorAccount NewDirectorAccount;
        string TestEmailAddress = "exampleDirector@email.com";
        string FakeEmailAddress = "nonexistent@email.com";
        
        public DirectorAccountRepositoryTests()
        {
            Repository = new DirectorAccountRepository(DissertationContext);
            NewDirectorAccount = new DirectorAccount{
                EmailAddress = TestEmailAddress,
                Password = "IAmADirector",
                FirstName = "John",
                LastName = "Doe"
            };
        }
        
        [Test]
        [Ignore("Tested To Ensure DBConnection and functionality")]
        public void TestDeleteDirector()
        {
            Repository.DeleteDirectorAccount(TestEmailAddress);
            Assert.Null(Repository.FindDirectorAccount(TestEmailAddress));
        }

        [Test]
        public void TestFindFakeDirectorAccounts()
        {
            DirectorAccount foundAccount;
            foundAccount = Repository.FindDirectorAccount(FakeEmailAddress);
            Assert.Null(foundAccount);
        }
        
        [Test]
        [Ignore("Tested To Ensure DBConnection and functionality")]
        public void TestSaveDirector()
        {
            Repository.SaveNewDirectorAccount(NewDirectorAccount);
            DirectorAccount savedAccount = Repository.FindDirectorAccount(TestEmailAddress);
            Assert.NotNull(savedAccount);
        }
    }
}