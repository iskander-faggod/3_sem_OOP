using System;
using System.Collections;
using Shops.Tools;

namespace Shops.Entities
{
    public class Product
    {
        public Product(string name, double price)
        {
            if (string.IsNullOrWhiteSpace(name) || price < 0)
            {
                throw new ShopException($"Invalid data - {name}, {price}");
            }

            Name = name;
            Price = price;
        }

        public double Price { get; set; }

        public string Name { get; }

        public override int GetHashCode() => Name.GetHashCode();

        public Product Clone()
        {
            return new Product(Name, Price);
        }

        public override bool Equals(object obj)
        {
            if (obj is Product product)
            {
                return product.Name == Name;
            }

            return false;
        }
    }
}