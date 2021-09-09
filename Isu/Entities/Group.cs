#nullable enable
using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private readonly List<Student> _studentsList;

        public Group(string groupName)
        {
            GroupNumber = Convert.ToUInt16(groupName.Substring(3, 2));
            CourseNumber = CourseNumber.CreateInstance(Convert.ToUInt16(groupName.Substring(2, 1)));
            if (GroupNumber > 14)
            {
                throw new IsuException("Invalid group number");
            }

            if (CourseNumber.Number > 4)
            {
                throw new IsuException("Invalid course number");
            }

            if (string.IsNullOrWhiteSpace(groupName) || groupName.Length != 5)
            {
                throw new IsuException("Invalid group");
            }

            _studentsList = new List<Student>();
        }

        public CourseNumber CourseNumber { get; set; }
        public uint GroupNumber { get; set; }

        public void AddStudent(Student student)
        {
            _studentsList.Add(student);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            var a = (Group)obj;
            return a.GroupNumber == this.GroupNumber && a.CourseNumber.Equals(this.CourseNumber);
        }

        public override int GetHashCode() => HashCode.Combine(CourseNumber, GroupNumber);
    }
}