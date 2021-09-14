#nullable enable
using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        public Student(string name, int id)
        {
            if (id < 1)
            {
                throw new IsuException($"Invalid id, id - {id}");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new IsuException($"Invalid name, name - {name}");
            }

            Name = name;
            Id = id;
        }

        public string Name { get; }
        public int Id { get; }
        public override int GetHashCode() => HashCode.Combine(Name, Id);

        public override bool Equals(object? obj) => base.Equals(obj);

        protected bool Equals(Student other) => Name == other.Name && Id == other.Id;
    }
}