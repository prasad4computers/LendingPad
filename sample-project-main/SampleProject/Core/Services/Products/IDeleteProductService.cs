using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface IDeleteProductService
    {
        void Delete(Product product);
        void DeleteAll();
    }
}
