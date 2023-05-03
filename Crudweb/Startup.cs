using CRUD.Domain.Employee;
using CRUD.EF.Repositories;
using CRUD.EF;
using Microsoft.EntityFrameworkCore;
using CRUD.Domain.Product;

namespace CRUDWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public readonly IWebHostEnvironment env1;
        public Startup(IConfiguration _configuration, IWebHostEnvironment env)
        {
            Configuration = _configuration;
            env1 = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(

                Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
           
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            


        }
        public void Configure(WebApplication app)
        {
            if (!env1.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
