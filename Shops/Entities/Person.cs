using System.Collections.Generic;

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
            if (price > 0) Fund -= price;
        }
    }
}