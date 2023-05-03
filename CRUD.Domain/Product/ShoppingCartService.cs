using CRUD.Domain.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Domain.Product
{
    public class ShoppingCartService : IShoppingCartService
    {
        public readonly IShoppingCartRepository shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository _shoppingCartRepository)
        {
            shoppingCartRepository = _shoppingCartRepository;
        }
        public async Task<ShoppingCart> AddToCart(ShoppingCart cart)
        {
            return await shoppingCartRepository.AddToCart(cart);
        }

        public async Task<List<ShoppingCart>> GetAllItems()
        {
            return await shoppingCartRepository.GetAllItems();
        }

        public async Task<ShoppingCart?> GetCartById(Guid id)
        {
            return await shoppingCartRepository.GetCartById(id);
        }

        public async Task<ShoppingCart> RemoveFromCart(ShoppingCart cart)
        {
            return await shoppingCartRepository.RemoveFromCart(cart);
        }

        public async Task<ShoppingCart> UpdateCart(ShoppingCart cart)
        {
            return await shoppingCartRepository.UpdateCart(cart);
        }
    }
}
