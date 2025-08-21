using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount cannot be negative.")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string CustomerId { get; set; }

        public IEnumerable<string> ProductIds { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
