using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productshop.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        Task<bool> SaveAsync();
    }
}
