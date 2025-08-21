﻿using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IUpdateProductService _updateProductService;
        private readonly IIdObjectFactory<Product> _productFactory;
        private readonly IProductRepository _productRepository;

        public CreateProductService(IIdObjectFactory<Product> productFactory, IProductRepository productRepository, IUpdateProductService updateProductService)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
            _updateProductService = updateProductService;
        }

        public Product Create(Guid id, string name, decimal price, string description)
        {
            var productObj = _productRepository.Get(id);
            if (productObj != null)
            {
                throw new InvalidOperationException($"Product with Id {id} already exists.");
            }

            var product = _productFactory.Create(id);
            _updateProductService.Update(product, name, price, description);
            _productRepository.Save(product);
            return product;
        }
    }
}
