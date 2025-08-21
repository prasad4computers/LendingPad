using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderData
    {
        public Guid Id { get; }
        public DateTime OrderDate { get; }
        public OrderStatus Status { get; }
        public decimal TotalAmount { get; }
        public string CustomerId { get; }
        public IEnumerable<string> ProductIds { get; }
        public IEnumerable<string> Tags { get; }

        public OrderData(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            Id = order.Id;
            OrderDate = order.OrderDate;
            Status = order.Status;
            TotalAmount = order.TotalAmount;
            CustomerId = order.CustomerId;
            ProductIds = order.ProductIds;
        }
    }
}
