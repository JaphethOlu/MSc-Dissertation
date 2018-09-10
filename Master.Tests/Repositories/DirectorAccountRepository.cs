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
        IDirectorAccountRepository DirectorAccountRepository;
        DissertationContext DissertationContext = new DissertationContext();
        DirectorAccount NewDirectorAccount;
        string TestEmailAddress = "exampleDirector@email.com";
        string FakeEmailAddress = "nonexistent@email.com";
        
        public DirectorAccountRepositoryTests()
        {
            DirectorAccountRepository = new DirectorAccountRepository(DissertationContext);
            NewDirectorAccount = new DirectorAccount{
                EmailAddress = TestEmailAddress,
                Password = "IAmADirector",
                FirstName = "John",
                LastName = "Doe"
            };
        }
        
        [Test]
        public void TestDeleteDirector()
        {
            DirectorAccountRepository.DeleteDirectorAccount(TestEmailAddress);
            Assert.Null(DirectorAccountRepository.FindDirectorAccount(TestEmailAddress));
        }

        [Test]
        public void TestFindFakeDirectorAccounts()
        {
            DirectorAccount foundAccount;
            foundAccount = DirectorAccountRepository.FindDirectorAccount(FakeEmailAddress);
            Assert.Null(foundAccount);
        }
        
        [Test]
        public void TestSaveDirector()
        {
            DirectorAccountRepository.SaveNewDirectorAccount(NewDirectorAccount);
            DirectorAccount savedAccount = DirectorAccountRepository.FindDirectorAccount(TestEmailAddress);
            Assert.NotNull(savedAccount);
        }
    }
}