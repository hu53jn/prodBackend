using productshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productshop.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string email, string password);
    }
}
