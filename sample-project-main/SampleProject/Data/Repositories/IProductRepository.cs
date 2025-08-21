using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IProductRepository
    {
        Task Save(Product product);
        Task Delete(Product product);
        Task DeleteAll();
        Product Get(Guid id);
        Task<IEnumerable<Product>> Get(string name = null, decimal? minPrice = null);
    }
}
