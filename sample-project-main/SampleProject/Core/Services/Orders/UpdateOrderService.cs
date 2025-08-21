using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, DateTime orderDate, OrderStatus status, decimal totalAmount, string customerId, IEnumerable<string> productIds, IEnumerable<string> tags)
        {
            order.SetOrderDate(orderDate);
            order.SetStatus(status);
            order.SetTotalAmount(totalAmount);
            order.SetCustomerId(customerId);
            order.SetProductIds(productIds);
        }
    }
}
