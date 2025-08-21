using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Common;
using System.Threading.Tasks;

namespace Data.Repositories
{


    [AutoRegister]
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public async Task Save(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                _products.Remove(existing);
            }
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task Delete(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");

            _products.Remove(existing);
            await Task.CompletedTask;
        }

        public async Task DeleteAll()
        {
            _products.Clear();
            await Task.CompletedTask;
        }

        public Product Get(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> Get(string name = null, decimal? minPrice = null)
        {
            var query = _products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            return await Task.FromResult(query);
        }

    }
}
