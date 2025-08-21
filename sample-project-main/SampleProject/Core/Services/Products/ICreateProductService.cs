using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
        Product Create(Guid id, string name, decimal price, string description);
    }
}
