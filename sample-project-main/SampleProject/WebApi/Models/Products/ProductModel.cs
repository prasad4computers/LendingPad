using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
        public decimal Price { get; set; }

        public string Description { get; set; }

    }
}
