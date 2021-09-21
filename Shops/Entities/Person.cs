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
            Card = new List<Product>();
        }

        public List<Product> Card { get; set; }
        public double Fund { get; set; }

        private string Name { get; set; }
        private int Id { get; set; }

        public void Transaction(double price)
        {
            Fund -= price;
        }
    }
}