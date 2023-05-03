
using CRUD.Domain.Employee;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.EF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly ProductDbContext dbContext;
        public ProductRepository(ProductDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await dbContext.ProductsTable.AsNoTracking().ToListAsync();
        }
        public async Task<Product?> GetProductById(Guid id)
        {
            return await dbContext.ProductsTable.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Product> AddProduct(Product prod)
        {
            prod.Id = Guid.NewGuid();
            dbContext.ProductsTable.Add(prod);
            await dbContext.SaveChangesAsync();
            return prod;
        }
        public async Task<Product> EditProduct(Product prod)
        {
            dbContext.ProductsTable.Update(prod);
            await dbContext.SaveChangesAsync();
            return prod;
        }
        public async Task<Product> DeleteProduct(Product prod)
        {
            dbContext.ProductsTable.Remove(prod);
            await dbContext.SaveChangesAsync();
            return prod;
        }
    }
}
