    using CRUD.Domain.Employee;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    namespace CRUD.Domain.Employee
    {
        public class ShoppingCart
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            [ForeignKey("ProductId")]
            [ValidateNever]
            public Product Product { get; set; }
            public int TotalItems { get; set; }
            [NotMapped]
            public double TotalAmount { get; set; }
        }
    }
