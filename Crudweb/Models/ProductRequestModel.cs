using CRUD.Domain.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CRUDWeb.Models
{
    public class ProductRequestModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Product Name is Required!")]

        [DisplayName("Product Name")]
        public string? ProductName { get; set; }
        [Required(ErrorMessage = "Brand Name is Required!")]

        public string? Brand { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

    }
}
