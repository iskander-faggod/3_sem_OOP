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

        public string Name { get; }

        public double Fund { get; private set; }

        public int Id { get; }

        public void Transaction(double price)
        {
            if (price > Fund) throw new ShopException("You don't have that much money");
            if (price < 0) throw new ShopException($"Invalid price - {price}");
            Fund -= price;
        }
    }
}