#nullable enable
using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private const int MaxGroupNumber = 14;
        private const int MaxCourseNumber = 4;
        private const int MaxGroupLength = 5;
        private readonly List<Student> _studentsList;

        public Group(string groupName)
        {
            GroupNumber = Convert.ToUInt16(groupName.Substring(3, 2));
            CourseNumber = CourseNumber.CreateInstance(Convert.ToUInt16(groupName.Substring(2, 1)));
            if (GroupNumber > MaxGroupNumber)
            {
                throw new IsuException($"Invalid group number, group number - {GroupNumber}");
            }

            if (CourseNumber.Number > MaxCourseNumber)
            {
                throw new IsuException($"Invalid course number, course number - {CourseNumber}");
            }

            if (string.IsNullOrWhiteSpace(groupName) || groupName.Length != MaxGroupLength)
            {
                throw new IsuException($"Invalid group, with name - {groupName}");
            }

            _studentsList = new List<Student>();
        }

        public CourseNumber CourseNumber { get; }
        public uint GroupNumber { get; }

        public void AddStudent(Student student)
        {
            _studentsList.Add(student);
        }

        public override bool Equals(object? obj) => Equals(obj as Group);
        public override int GetHashCode() => HashCode.Combine(CourseNumber, GroupNumber);

        private bool Equals(Group? group)
        {
            return @group is not null && @group.GroupNumber == this.GroupNumber;
        }
    }
}