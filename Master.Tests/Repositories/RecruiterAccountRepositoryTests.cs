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
    [Ignore("Tested to ensure functional database mapping")]
    public class RecruiterAccountRepositoryTests
    {
        IRecruiterAccountRepository RecruiterAccountRepository;
        DissertationContext DissertationContext = new DissertationContext();
        RecruiterAccount NewRecruiterAccount;
        string TestEmailAddress = "exampleRecruiter@example.com";
        string FakeEmailAddress = "nonexistent@example.com";
        
        public RecruiterAccountRepositoryTests()
        {
            RecruiterAccountRepository = new RecruiterAccountRepository(DissertationContext);
            NewRecruiterAccount = new RecruiterAccount{
                EmailAddress = TestEmailAddress,
                Password = "IAmARecruiter",
                FirstName = "John",
                LastName = "Doe",
                OrganisationId = 101100
            };
        }
        
        [Test]
        public void TestDeleteRecruiter()
        {
            RecruiterAccountRepository.DeleteRecruiterAccount(TestEmailAddress);
            Assert.Null(RecruiterAccountRepository.FindRecruiterAccount(TestEmailAddress));
        }

        [Test]
        public void TestFindFakeRecruiterAccounts()
        {
            RecruiterAccount foundAccount;
            foundAccount = RecruiterAccountRepository.FindRecruiterAccount(FakeEmailAddress);
            Assert.Null(foundAccount);
        }
        
        [Test]
        public void TestSaveRecruiter()
        {
            RecruiterAccountRepository.SaveNewRecruiterAccount(NewRecruiterAccount);
            RecruiterAccount savedAccount = RecruiterAccountRepository.FindRecruiterAccount(TestEmailAddress);
            Assert.NotNull(savedAccount);
        }
    }
}