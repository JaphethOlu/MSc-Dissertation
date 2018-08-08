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

        public void SaveUser(User newUser)
        {
            DbContext.Users.Add(newUser);
            DbContext.SaveChanges();
        }
    }
}