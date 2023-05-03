using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Domain.Employee
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository productRepository;
        public ProductService(IProductRepository _employeeRepository)
        {
            productRepository = _employeeRepository;
        }
        public async Task<Product> AddProduct(Product prod)
        {
            return await productRepository.AddProduct(prod);
        }
        public async Task<Product> DeleteProduct(Product prod)
        {
            return await productRepository.DeleteProduct(prod);
        }
        public async Task<Product> EditProduct(Product prod)
        {
            return await productRepository.EditProduct(prod);
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await productRepository.GetAllProducts();
        }
        public async Task<Product?> GetProductById(Guid id)
        {
            return await productRepository.GetProductById(id);
        }
    }
}
