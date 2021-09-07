#nullable enable
using System;

namespace Isu.Classes
{
    public class Student
    {
        private Student(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }

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