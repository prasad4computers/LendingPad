using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IOrderRepository
    {
        Task Save(Order order);
        Task Delete(Order order);
        Task DeleteAll();
        Order Get(Guid id);
        Task<IEnumerable<Order>> Get(OrderStatus? status = null, string customerId = null);

    }
}
