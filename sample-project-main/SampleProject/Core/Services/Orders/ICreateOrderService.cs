using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid id, DateTime orderDate, OrderStatus status, decimal totalAmount, string customerId, IEnumerable<string> productIds, IEnumerable<string> tags);
    }

}
