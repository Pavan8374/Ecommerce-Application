using CRUD.Domain.Employee;
using CRUD.Domain.Product;
using CRUD.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService productService;
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IProductService _productService, IShoppingCartService _shoppingCartService)
        {
            productService = _productService;
            shoppingCartService = _shoppingCartService;
        }
        public async Task<IActionResult> Index()
        {
            var cartFromDb = await shoppingCartService.GetAllItems();
            return View(cartFromDb);
        }

        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var cart = await shoppingCartService.GetCartById(id);
            var cartFromDb = await shoppingCartService.RemoveFromCart(cart);
            TempData["Success"] = "Item Removed Successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Plus(Guid id)
        {
            var cartFromDb = await shoppingCartService.GetCartById(id);
            cartFromDb.TotalItems += 1;
            await shoppingCartService.UpdateCart(cartFromDb);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Minus(Guid id)
        {
            var cartFromDb = await shoppingCartService.GetCartById(id);
            if(cartFromDb.TotalItems <= 1)
            {
                await shoppingCartService.RemoveFromCart(cartFromDb);
            }
            else
            {
                cartFromDb.TotalItems -= 1;
                await shoppingCartService.UpdateCart(cartFromDb);
            }
            
            return RedirectToAction("Index");
        }
    }
}
