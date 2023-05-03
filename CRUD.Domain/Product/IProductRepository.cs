using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Domain.Employee
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid id);
        Task<Product> AddProduct(Product prod);
        Task<Product> EditProduct(Product prod);
        Task<Product> DeleteProduct(Product prod);
    }
}
