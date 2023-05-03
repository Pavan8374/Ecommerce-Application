using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CRUDWeb.Models
{
    public class ViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Product Name is Required!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets are allowed!")]
        [DisplayName("Product Name")]
        public string? ProductName { get; set; }
        [Required(ErrorMessage = "Brand Name is Required!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets are allowed!")]
        public string? Brand { get; set; }       
        public string? Category { get; set; }
        public double Price { get; set; }
        public string? ImageURL { get; set; }
        public string? Description { get; set; }
    }
}
