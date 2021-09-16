using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Isu.Services;
using Isu.Tools;
using static Isu.Entities.CourseNumber;

namespace Isu.Entities
{
    public class IsuService : IIsuService
    {
        private int _maxGroupSize;
        public IsuService(int maxGroupSize)
        {
            Groups = new List<Group>();
            _maxGroupSize = maxGroupSize;
        }

        private List<Group> Groups { get; set; }
        private int Id { get;  set; } = 0;

        public Group AddGroup(string name)
        {
            var newGroup = new Group(name);

            if (Groups.Contains(newGroup))
            {
                throw new IsuException("Group is already created");
            }

            Groups.Add(newGroup);
            return newGroup;
            }

        public Student AddStudent(Group group, string name)
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
            return Groups
                .Select(@group => @group.GetStudentById(id))
                .FirstOrDefault();
        }

        public Student FindStudent(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new IsuException($"Invalid name, name - {name}");
            }

            return Groups
                .Select(group => group.GetStudentByName(name))
                .FirstOrDefault();
        }

        public List<Student> FindStudents(string groupName)
        {
            if (!string.IsNullOrWhiteSpace(groupName) && groupName.Length is <= 5 and > 0)
            {
                return Groups
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
            return Groups
                .Where(@group => @group.CourseNumber.Number == courseNumber.Number)
                .Select(@group => @group.StudentsList)
                .FirstOrDefault();
        }

        public Group FindGroup(string groupName)
        {
            return Groups
                .FirstOrDefault(group => @group.GroupName == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return Groups
                .Where(@group => @group.CourseNumber.Number == courseNumber.Number)
                .ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group group in Groups)
            {
                    group.RemoveStudent(student);
                    newGroup.AddStudent(student);
            }
        }
    }
}