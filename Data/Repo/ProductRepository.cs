using Microsoft.EntityFrameworkCore;
using productshop.Interfaces;
using productshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productshop.Data.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext dc;
        public ProductRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddProduct(Product product)
        {
            dc.Products.AddAsync(product);
        }

        public void DeleteProduct(int productId)
        {
            var product = dc.Products.Find(productId);
            dc.Products.Remove(product);
        }

        public async Task<Product> FindProduct(int id)
        {
            return await dc.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await dc.Products.ToListAsync();
        }

    }
}
