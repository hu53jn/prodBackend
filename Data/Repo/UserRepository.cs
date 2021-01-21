using Microsoft.EntityFrameworkCore;
using productshop.Interfaces;
using productshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productshop.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;
        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<User> Authenticate(string email, string password)
        {
            return await dc.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}
