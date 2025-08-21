using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepository, IUpdateOrderService updateOrderService)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _updateOrderService = updateOrderService;
        }

        public Order Create(Guid id, DateTime orderDate, OrderStatus status, decimal totalAmount, string customerId, IEnumerable<string> productIds, IEnumerable<string> tags)
        {
            var orderObj = _orderRepository.Get(id);
            if (orderObj != null)
            {
                throw new InvalidOperationException($"Order with Id {id} already exists.");
            }

            var order = _orderFactory.Create(id);
            _updateOrderService.Update(order, orderDate, status, totalAmount, customerId, productIds, tags);
            _orderRepository.Save(order);
            return order;
        }
    }
}