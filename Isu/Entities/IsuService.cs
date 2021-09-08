using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;

namespace Isu.Entities
{
    public class IsuService : IIsuService
    {
        private int _id = 0;
        private int _countOfStudents = 25;

        private IsuService(List<Student> listStudents, List<Group> listGroup, Dictionary<Group, List<Student>> dictGroup)
        {
            this.DictGroup = dictGroup;
        }

        private IsuService()
        {
            this.DictGroup = new Dictionary<Group, List<Student>>();
        }

        public Dictionary<Group, List<Student>> DictGroup { get; }
        public static IsuService CreateInstance(List<Student> listStudents, List<Group> listGroup, Dictionary<Group, List<Student>> dictGroup)
        {
            return new IsuService(listStudents, listGroup, dictGroup);
        }

        public static IsuService CreateInstance()
        {
            return new IsuService();
        }

        // Done
        public Group AddGroup(string name)
        {
            if (string.IsNullOrEmpty(name) || (name.Length != 5))
            {
                throw new IsuException("Invalid group name");
            }

            if (!uint.TryParse(name[2].ToString(), out uint courseNumber))
            {
                throw new IsuException("Invalid course number");
            }

            if (!uint.TryParse(name.Substring(3, 2), out uint groupNumber))
            {
                throw new IsuException("Invalid group number");
            }

            var newGroup = Group.CreateInstance(groupNumber, CourseNumber.CreateInstance(courseNumber));
            var studentsList = new List<Student>();

            if (DictGroup.ContainsKey(newGroup)) return newGroup;
            DictGroup.Add(newGroup, studentsList);

            return newGroup;
        }

        // Done
        public Student AddStudent(Group group, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IsuException("Invalid student name");
            }

            if (@group.Number.Number is > 4 or 0)
            {
                throw new IsuException("Invalid group");
            }

            if (DictGroup[group].Count > _countOfStudents)
            {
                throw new IsuException("Can't add student to group, group is full");
            }

            ++_id;
            var student = Student.CreateInstance(name, _id);
            List<Student> studentsList = DictGroup[group];
            if (studentsList.Contains(student)) return student;
            DictGroup[group].Add(student);
            return student;
        }

        // Done
        public Student GetStudent(int id)
        {
            if (id < 1)
            {
                throw new IsuException("Invalid id");
            }

            var currentStudent = Student.CreateInstance("name", id);
            foreach (Student student in DictGroup.Values.SelectMany(students => students.Where(student => student.Id == id)))
            {
                currentStudent.Id = student.Id;
                currentStudent.Name = student.Name;
            }

            return currentStudent;
        }

        public Student FindStudent(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new IsuException("Invalid name");
            }

            var currentStudent = Student.CreateInstance(name, 0);
            foreach (Student student in DictGroup.Values.SelectMany(students => students.Where(student => student.Name == name)))
            {
                currentStudent.Id = student.Id;
                currentStudent.Name = student.Name;
            }

            return currentStudent;
        }

        public List<Student> FindStudents(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName) || groupName.Length is > 5 or <= 0)
            {
                throw new IsuException("Invalid group");
            }

            if (!uint.TryParse(groupName[2].ToString(), out uint courseNumber))
            {
                throw new IsuException("Invalid Group course");
            }

            string groupNameString = groupName.Substring(3, 2);
            if (!uint.TryParse(groupNameString, out uint groupNumber))
            {
                throw new IsuException("Invalid Group name");
            }

            var currentGroup = Group.CreateInstance(groupNumber, CourseNumber.CreateInstance(courseNumber));
            foreach (Group @group in DictGroup.Keys.Where(@group => @group.GroupNumber == groupNumber || Equals(@group.Number, CourseNumber.CreateInstance(courseNumber))))
            {
                currentGroup.Number = CourseNumber.CreateInstance(courseNumber);
                currentGroup.GroupNumber = groupNumber;
            }

            return DictGroup[currentGroup];
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            if (courseNumber.Number is <= 0 or > 4)
            {
                throw new IsuException("Invalid Course");
            }

            return (from @group in DictGroup.Keys where Equals(@group.Number, courseNumber) select DictGroup[@group]).FirstOrDefault();
        }

        public Group FindGroup(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName) || groupName.Length != 5)
            {
                throw new IsuException("Invalid group");
            }

            if (!uint.TryParse(groupName[2].ToString(), out uint courseNumber))
            {
                throw new IsuException("Invalid Group course");
            }

            string currentGroupName = groupName.Substring(3, 2);
            if (!uint.TryParse(currentGroupName, out uint groupNumber))
            {
                throw new IsuException("Invalid Group name");
            }

            var currentGroup = Group.CreateInstance(groupNumber, CourseNumber.CreateInstance(courseNumber));
            foreach (Group @group in DictGroup.Keys.Where(@group => Equals(@group.Number, CourseNumber.CreateInstance(courseNumber)) || @group.GroupNumber == groupNumber))
            {
                currentGroup.Number = CourseNumber.CreateInstance(courseNumber);
                currentGroup.GroupNumber = groupNumber;
            }

            return currentGroup;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            if (courseNumber.Number is <= 0 or > 4)
            {
                throw new IsuException("Invalid Course");
            }

            return DictGroup.Keys.Where(@group => Equals(@group.Number, courseNumber)).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (student.Id < 0 || student.Id > _id)
            {
                throw new IsuException("Invalid Student");
            }

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