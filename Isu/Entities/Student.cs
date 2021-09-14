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

        public static Student CreateInstance(string name, int id) => new Student(name, id);

        public override int GetHashCode() => HashCode.Combine(Name, Id);

        public override bool Equals(object? obj) => Equals(obj as Student);
        private bool Equals(Student? student)
        {
            return student is not null && student.Id == this.Id;
        }
    }
}