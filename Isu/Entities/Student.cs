#nullable enable
using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        private Student(string name, int id)
        {
            if (id < 1)
            {
                throw new IsuException("Invalid id");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new IsuException("Invalid name");
            }

            Name = name;
            Id = id;
        }

        public string Name { get; }
        public int Id { get; }

        public static Student CreateInstance(string name, int id)
        {
            return new Student(name, id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Id);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            var a = (Student)obj;
            return a.Id == this.Id && a.Name == this.Name;
        }
    }
}