using System;
using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, double fund)
        {
            Name = name;
            Fund = fund;
            Id++;
        }

        public double Fund { get; private set; }

        private string Name { get; }
        private int Id { get; set; }

        public void Transaction(double price)
        {
            if (price > Fund) throw new ShopException("You don't have that much money");
            if (price < 0) throw new ShopException($"Invalid price - {price}");
            if (price > 0) Fund -= price;
        }
    }
}