using AutoMapper;
using CRUD.Domain.Employee;
using CRUD.EF;
using CRUDWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using X.PagedList;

namespace CRUDWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext context;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IProductService _productService, ProductDbContext _context, IMapper _mapper, IWebHostEnvironment _env)
        {
            productService = _productService;
            webHostEnvironment = _env;
            context = _context;
            mapper = _mapper;

        }
        public async Task<IActionResult> Index(int? page, string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            var products = await productService.GetAllProducts();
            var prod1 = from s in products select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                prod1 = prod1.Where(s => s.ProductName.Contains(searchString) || s.Brand.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstname_desc":
                    prod1 = prod1.OrderByDescending(e => e.ProductName);
                    break;
                default:
                    prod1 = prod1.OrderBy(e => e.ProductName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var prod2 = prod1.ToPagedList(pageNumber, pageSize);
            var prod3 = prod2.ToList();

            return View(prod2);
        }
        public async Task<IActionResult> Post(ProductRequestModel prodview)
        {
            var DTOmapped = mapper.Map<Product>(prodview);
            return Ok(DTOmapped);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductRequestModel prod)
        {
            if (ModelState.IsValid)
            {
                var product = mapper.Map<Product>(prod);
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (prod.Image != null)
                {
                    string fileName = "images/img/";
                    fileName += Guid.NewGuid().ToString() + Path.GetExtension(prod.Image.FileName);
                    string serverfolder = Path.Combine(wwwRootPath, fileName);
                    await prod.Image.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                    product.ImageURL = "/" + fileName;

                }
                             
                await productService.AddProduct(product);
                TempData["Success"] = "Product Registered Successfully!";
                return RedirectToAction("Index");
            }
            return View(prod);
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prod = await productService.GetProductById(id.Value);
            if (prod == null)
            {
                return NotFound();
            }
            var prod1 = mapper.Map<ProductRequestModel>(prod);
            return View(prod1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductRequestModel prod)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = await productService.GetProductById(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                mapper.Map(prod, existingProduct);
                
                if (prod.Image != null)
                {
                    string folder = "images/";
                    folder += Guid.NewGuid().ToString() + "_" + prod.Image.FileName;
                    string serverfolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                    using (var fileStream = new FileStream(serverfolder, FileMode.Create))
                    {
                        await prod.Image.CopyToAsync(fileStream);
                    }
                    existingProduct.ImageURL = "/" + folder;
                }
                await productService.EditProduct(existingProduct);
                TempData["Success"] = "Product Record Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(prod);
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prod = await productService.GetProductById(id.Value);
            if (prod == null)
            {
                return NotFound();
            }
            var prod1 = mapper.Map<ProductRequestModel>(prod);
            return View(prod1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Product prod)
        {
            await productService.DeleteProduct(prod);

            TempData["Success"] = "Product Record Deleted Successfully!";
            return RedirectToAction("Index");          
        }
    }
}
