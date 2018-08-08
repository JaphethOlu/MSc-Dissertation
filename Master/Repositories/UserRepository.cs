using System;
using Master.Models;
using Master.Contexts;
using Master.Interfaces.Repositories;

namespace Master.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DissertationContext DbContext;

        public UserRepository(DissertationContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveUserAccount(UserAccount newUserAccount)
        {
			//DbContext.Users.Add(newUserAccount);
			DbContext.UserAccounts.Add(newUserAccount);
            DbContext.SaveChanges();
        }

        public UserAccount FindUserAccount(string emailAddress)
        {
			UserAccount user = DbContext.UserAccounts.Find(emailAddress);
            return user;
        }

        public void DeleteUserAccount(string emailAddress)
        {
            UserAccount userToDelete = FindUserAccount(emailAddress);
            DbContext.UserAccounts.Remove(userToDelete);
            DbContext.SaveChanges();
        }
    }
}