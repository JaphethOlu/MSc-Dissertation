using System;
using Xunit;
using Master;
using Master.Models;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Repositories;

namespace Tests
{
    public class UserRepositoryTests
    {
        IUserRepository userRepository;
        DissertationContext dissertationContext = new DissertationContext();
        UserAccount NewUserAccount;
        string ExampleEmailAddress = "example@email.com";
        
        public UserRepositoryTests()
        {
            userRepository = new UserRepository(dissertationContext);
            NewUserAccount = new UserAccount(ExampleEmailAddress, "IAmAContractor", Role.Recruiter);
        }

        [Fact]
        public void TestDeleteUser()
        {
            userRepository.DeleteUserAccount(ExampleEmailAddress);
            Assert.Null(userRepository.FindUserAccount(ExampleEmailAddress));
        }

        [Theory]
        [InlineData("someemail@email.com")]
        [InlineData("nonexistent@email.com")]
        [InlineData("hello@email.com")]
        public void TestFindFakeUserAccounts(string email)
        {
            UserAccount foundAccount;
            foundAccount = userRepository.FindUserAccount(email);
            Assert.Null(foundAccount);
        }
        
        [Fact]
        public void TestSaveUser()
        {
            userRepository.SaveUserAccount(NewUserAccount);
            UserAccount savedAccount = userRepository.FindUserAccount("example@email.com");
            Assert.NotNull(savedAccount);
        }
    }
}