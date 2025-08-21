using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string Description { get; }
        public IEnumerable<string> Tags { get; }

        public ProductData(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
        }
    }
}
