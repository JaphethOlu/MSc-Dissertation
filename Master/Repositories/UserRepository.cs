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

        public void SaveUser(UserAccount newUserAccount)
        {
            DbContext.Users.Add(newUserAccount);
            DbContext.SaveChanges();
        }
    }
}