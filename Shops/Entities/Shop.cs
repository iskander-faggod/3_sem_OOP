using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private const int MaxAddressLength = 20;
        private static int _idGenerator = 0;
        private readonly int _shopId;
        private string _shopName;
        private string _address;

        public Shop(string name, string address, double fund)
        {
            if (_shopId < 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || fund < 0)
            {
                throw new ShopException("Invalid data");
            }

            if (address.Length > MaxAddressLength)
            {
                throw new ShopException("Invalid address length");
            }

            _shopId = _idGenerator++;
            _shopName = name;
            _address = address;
            Fund = fund;
            ProductBase = new Dictionary<Product, int>();
        }

        public Dictionary<Product, int> ProductBase { get; }
        public double Fund { get; private set; }

        public void Transaction(double price)
        {
            Fund -= price;
        }

        public override int GetHashCode() => _shopId.GetHashCode();

        public override bool Equals(object obj)
        {
            return obj is Shop shop && shop._shopId == _shopId;
        }
    }
}