using CRUD.Domain.Employee;
using CRUD.Domain.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.EF.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public readonly ProductDbContext dbContext;
        public ShoppingCartRepository(ProductDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<List<ShoppingCart>> GetAllItems()
        {
            return await dbContext.ShoppingCartTable.AsNoTracking().Include(x => x.Product).ToListAsync();
        }
        public async Task<ShoppingCart> AddToCart(ShoppingCart cart)
        {
            cart.Id = Guid.NewGuid();
            dbContext.ShoppingCartTable.Add(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }
        public async Task<ShoppingCart?> GetCartById(Guid id)
        {
            return await dbContext.ShoppingCartTable.AsNoTracking().Where(x => x.Id == id).Include(x => x.Product).FirstOrDefaultAsync();
        }
        public async Task<ShoppingCart> UpdateCart(ShoppingCart cart)
        {
            dbContext.ShoppingCartTable.Update(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }
        public async Task<ShoppingCart> RemoveFromCart(ShoppingCart cart)
        {            
            dbContext.ShoppingCartTable.Remove(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }
    }
}
