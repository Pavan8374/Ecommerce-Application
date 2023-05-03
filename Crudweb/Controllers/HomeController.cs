using AutoMapper;
using CRUD.Domain.Employee;
using CRUD.Domain.Product;
using CRUD.EF;
using CRUD.EF.Repositories;
using CRUDWeb.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace CRUDWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductDbContext context;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IShoppingCartService cartservice;


        public HomeController(ILogger<HomeController> logger, IProductService _productService, ProductDbContext _context, IMapper _mapper, IShoppingCartService _cartservice)
        {
            _logger = logger;
            productService = _productService;
            context = _context;
            mapper = _mapper;
            cartservice = _cartservice;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProducts();
            return View(products);
        }
        public async Task<IActionResult> Post(ProductRequestModel prodview)
        {
            var DTOmapped = mapper.Map<Product>(prodview);
            return Ok(DTOmapped);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            ShoppingCart cart = new()
            {
                Product = await productService.GetProductById(id),
                TotalItems = 1,
                ProductId = id
            };
            return View(cart);
        }
        [HttpPost]
        public async Task<IActionResult> Details(ShoppingCart cart)
        {
            var cartFromDb = await cartservice.GetCartById(cart.ProductId);
            if (cartFromDb != null)
            {
                cartFromDb.TotalItems += cart.TotalItems;
                cartservice.UpdateCart(cartFromDb);
            }
            else
            {
                cartservice.AddToCart(cart);
            }
            TempData["Success"] = "Product added to Cart Successfully!";
            return RedirectToAction(nameof(Index));
            
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }


}