using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private const int MaxAddressLength = 20;

        public Shop(string name, string address, double fund)
        {
            if (Id < 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || fund < 0)
            {
                throw new ShopException("Invalid data");
            }

            if (address.Length > MaxAddressLength)
            {
                throw new ShopException("Invalid address length");
            }

            Id = Id++;
            Name = name;
            Address = address;
            Fund = fund;
            ProductBase = new Dictionary<Product, int>();
        }

        public Dictionary<Product, int> ProductBase { get; private set; }
        public double Fund { get; private set; }
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Address { get; private set; }

        public void Transaction(double price)
        {
            Fund -= price;
        }

        public void ChangePrice(Product product, double price)
        {
            Product item = ProductBase.Keys.FirstOrDefault(currentProduct => currentProduct == product);
            if (item != null) item.Price = price;
        }

        public override int GetHashCode() => Id.GetHashCode();
        public override bool Equals(object obj)
        {
            return obj is Shop shop && shop.Id == Id;
        }
    }
}