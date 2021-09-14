#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private const int MaxGroupNumber = 14;
        private const int MaxCourseNumber = 4;
        private const int MaxGroupLength = 5;

        public Group(string groupName)
        {
            GroupName = groupName;
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

            StudentsList = new List<Student>();
        }

        public CourseNumber CourseNumber { get; }
        public uint GroupNumber { get; }

        public string GroupName { get; }
        private List<Student> StudentsList { get; }
        public void AddStudent(Student student)
        {
            if (!StudentsList.Contains(student))
            {
                StudentsList.Add(student);
            }
            else
            {
                throw new IsuException("Student already in Group");
            }
        }

        public void RemoveStudent(Student student)
        {
            if (StudentsList.Contains(student))
            {
                StudentsList.Remove(student);
            }
            else
            {
                throw new IsuException("Student is not in the group");
            }
        }

        public Student GetStudentById(int id)
        {
            return StudentsList.FirstOrDefault(student => student.Id == id) ??
                   throw new IsuException($"Invalid student with id - {id}");
        }

        public Student GetStudentByName(string name)
        {
            return StudentsList.FirstOrDefault(student => student.Name == name) ??
                   throw new IsuException($"Invalid student with name - {name}");
        }

        public bool IsStudentInGroup(Student student)
        {
            return StudentsList.Contains(student);
        }

        public List<Student> GetStudents()
        {
            return StudentsList;
        }

        public int GetGroupSize()
        {
            return StudentsList.Count;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CourseNumber, GroupNumber, GroupName, StudentsList);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(Group other)
        {
            return CourseNumber
                       .Equals(other.CourseNumber) &&
                   GroupNumber == other.GroupNumber &&
                   GroupName == other.GroupName &&
                   StudentsList.Equals(other.StudentsList);
        }
    }
}