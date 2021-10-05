using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Isu.Services;
using Isu.Tools;
using static Isu.Entities.CourseNumber;

namespace Isu.Entities
{
    public class IsuService : IIsuService
    {
        private readonly List<Group<Student>> _groups;
        private readonly int _maxGroupSize;
        public IsuService(int maxGroupSize)
        {
            _groups = new List<Group<Student>>();
            _maxGroupSize = maxGroupSize;
        }

        private int Id { get;  set; } = 0;

        public Group<Student> AddGroup(string name)
        {
            var newGroup = new Group<Student>(name);

            if (_groups.Contains(newGroup))
            {
                throw new IsuException("Group is already created");
            }

            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group<Student> group, string name)
        {
            if (group.GetGroupSize() > _maxGroupSize)
            {
                throw new IsuException("Can't add student to group, group is full");
            }

            ++Id;
            var student = new Student(name, Id);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _groups
                .Select(@group => @group.GetStudentById(id))
                .FirstOrDefault();
        }

        public Student FindStudent(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new IsuException($"Invalid name, name - {name}");
            }

            return _groups
                .Select(group => group.GetStudentByName(name))
                .FirstOrDefault();
        }

        public List<Student> FindStudents(string groupName)
        {
            if (!string.IsNullOrWhiteSpace(groupName) && groupName.Length is <= 5 and > 0)
            {
                return _groups
                    .Where(@group => @group.GroupName == groupName)
                    .Select(@group => @group.StudentsList)
                    .FirstOrDefault();
            }
            else
            {
                throw new IsuException($"Invalid group with name - {groupName}");
            }
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _groups
                .Where(@group => @group.CourseNumber.Number == courseNumber.Number)
                .Select(@group => @group.StudentsList)
                .FirstOrDefault();
        }

        public Group<Student> FindGroup(string groupName)
        {
            return _groups
                .FirstOrDefault(group => @group.GroupName == groupName);
        }

        public List<Group<Student>> FindGroups(CourseNumber courseNumber)
        {
            return _groups
                .Where(@group => @group.CourseNumber.Number == courseNumber.Number)
                .ToList();
        }

        public void ChangeStudentGroup(Student student, Group<Student> newGroup)
        {
            foreach (Group<Student> group in _groups)
            {
                    group.RemoveStudent(student);
                    newGroup.AddStudent(student);
            }
        }
    }
}