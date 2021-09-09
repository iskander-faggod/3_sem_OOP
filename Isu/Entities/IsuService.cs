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
        private IsuService(Dictionary<Group, List<Student>> dictGroup)
        {
            DictGroup = dictGroup;
        }

        private IsuService()
        {
            DictGroup = new Dictionary<Group, List<Student>>();
        }

        public Dictionary<Group, List<Student>> DictGroup { get; }
        private int Id { get; set; } = 0;
        private int CountOfStudents { get; set; } = 25;
        public static IsuService CreateInstance()
        {
            return new IsuService();
        }

        public Group AddGroup(string name)
        {
            var studentsList = new List<Student>();
            var newGroup = new Group(name);

            if (DictGroup.ContainsKey(newGroup)) return newGroup;
            DictGroup.Add(newGroup, studentsList);

            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (DictGroup[group].Count > CountOfStudents)
            {
                throw new IsuException("Can't add student to group, group is full");
            }

            ++Id;
            var student = Student.CreateInstance(name, Id);
            List<Student> studentsList = DictGroup[group];
            group.AddStudent(student);
            if (studentsList.Contains(student)) return student;
            DictGroup[group].Add(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return DictGroup.Values.SelectMany(students => students.Where(student => student.Id == id)).FirstOrDefault();
        }

        public Student FindStudent(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new IsuException("Invalid name");
            }

            foreach (Student student in DictGroup.Values.SelectMany(students => students.Where(student => student.Name == name)))
            {
                return student;
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName) || groupName.Length is > 5 or <= 0)
            {
                throw new IsuException("Invalid group");
            }

            return DictGroup[new Group(groupName)];
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            foreach (Group @group in DictGroup.Keys)
            {
                if (Equals(@group.CourseNumber, courseNumber))
                {
                    List<Student> list = DictGroup[@group];
                    return list;
                }
            }

            return null;
        }

        public Group FindGroup(string groupName)
        {
            var currentGroup = new Group(groupName);
            foreach (Group group in DictGroup.Keys)
            {
                if (Equals(@group, currentGroup))
                {
                    return group;
                }
                else
                {
                    throw new IsuException("Group not find");
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            if (courseNumber.Number is <= 0 or > 4)
            {
                throw new IsuException("Invalid Course");
            }

            return DictGroup.Keys.Where(@group => Equals(@group.CourseNumber, courseNumber)).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (List<Student> students in from students in DictGroup.Values from currentStudent in students where Equals(currentStudent, student) select students)
            {
                if (!students.Contains(student))
                {
                    throw new IsuException("Student is not in this group");
                }

                students.Remove(student);
            }

            DictGroup[newGroup].Add(student);
        }
    }
}