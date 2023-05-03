using CRUD.Domain.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Domain.Product
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCart>> GetAllItems();
        Task<ShoppingCart?> GetCartById(Guid id);
        Task<ShoppingCart> AddToCart(ShoppingCart cart);
        Task<ShoppingCart> UpdateCart(ShoppingCart cart);
        Task<ShoppingCart> RemoveFromCart(ShoppingCart cart);
    }
}
