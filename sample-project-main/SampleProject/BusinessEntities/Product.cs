using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Product:IdObject
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            Name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));
            Price = price;
        }

        public void SetDescription(string description)
        {
            Description = description ?? string.Empty;
        }
    }
}
