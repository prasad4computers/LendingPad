using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class Order: IdObject
    {
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string CustomerId { get; private set; }
        public IList<string> ProductIds { get; private set; }

        public void SetOrderDate(DateTime orderDate)
        {
            if (orderDate > DateTime.UtcNow)
                throw new ArgumentException("Order date cannot be in the future.", nameof(orderDate));
            OrderDate = orderDate;
        }

        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }

        public void SetTotalAmount(decimal totalAmount)
        {
            if (totalAmount < 0)
                throw new ArgumentException("Total amount cannot be negative.", nameof(totalAmount));
            TotalAmount = totalAmount;
        }

        public void SetCustomerId(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("Customer ID cannot be empty.", nameof(customerId));
            CustomerId = customerId;
        }

        public void SetProductIds(IEnumerable<string> productIds)
        {
            ProductIds = productIds?.ToList() ?? new List<string>();
        }
    }
}
