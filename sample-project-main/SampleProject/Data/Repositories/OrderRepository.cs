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
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public async Task Save(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var existing = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existing != null)
            {
                _orders.Remove(existing);
            }
            _orders.Add(order);
            await Task.CompletedTask;
        }

        public async Task Delete(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var existing = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Order with ID {order.Id} not found.");

            _orders.Remove(existing);
            await Task.CompletedTask;
        }

        public async Task DeleteAll()
        {
            _orders.Clear();
            await Task.CompletedTask;
        }

        public Order Get(Guid id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> Get(OrderStatus? status = null, string customerId = null)
        {
            var query = _orders.AsEnumerable();

            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            if (!string.IsNullOrWhiteSpace(customerId))
            {
                query = query.Where(o => o.CustomerId.Equals(customerId, StringComparison.OrdinalIgnoreCase));
            }

            return await Task.FromResult(query);
        }

    }
}
