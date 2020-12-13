using productshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productshop.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        void AddProduct(Product product);

        void DeleteProduct(int productId);
        Task<Product> FindProduct(int id);

    }
}
