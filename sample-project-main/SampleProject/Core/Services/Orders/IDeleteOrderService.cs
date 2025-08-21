using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IDeleteOrderService
    {
        void Delete(Order order);
        void DeleteAll();
    }
}
