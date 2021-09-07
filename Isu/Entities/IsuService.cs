using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Isu.Services;

namespace Isu.Classes
{
    public class IsuService : IIsuService
    {
        private readonly Dictionary<Group, List<Student>> _dictGroup;
        private int _id = 0;

        private IsuService(List<Student> listStudents, List<Group> listGroup, Dictionary<Group, List<Student>> dictGroup)
        {
            this._dictGroup = dictGroup;
        }

        private IsuService()
        {
            this._dictGroup = new Dictionary<Group, List<Student>>();
        }

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
            Debug.Assert(name != null, nameof(name) + " != null");
            if (name == null && string.IsNullOrEmpty(name) && (name.Length != 5))
            {
                throw new System.NotImplementedException("Invalid group name");
            }

            if (!uint.TryParse(name[2].ToString(), out uint courseNumber))
            {
                throw new System.NotImplementedException("Invalid course number");
            }

            if (!uint.TryParse(name.Substring(3, 2), out uint groupNumber))
            {
                throw new System.NotImplementedException("Invalid group number");
            }

            var newGroup = Group.CreateInstance(groupNumber, CourseNumber.CreateInstance(courseNumber));
            var studentsList = new List<Student>();

            if (_dictGroup.ContainsKey(newGroup)) return newGroup;
            _dictGroup.Add(newGroup, studentsList);

            return newGroup;
        }

        // Done
        public Student AddStudent(Group group, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.NotImplementedException("Invalid student name");
            }

            if ((@group.GroupNumber > 20) || @group.Number.Number is > 4 or 0)
            {
                throw new System.NotImplementedException("Invalid group");
            }

            if (_dictGroup[group].Count > 25)
            {
                throw new System.NotImplementedException("Can't add student to group, group is full");
            }

            ++_id;
            var student = Student.CreateInstance(name, _id);
            List<Student> studentsList = _dictGroup[group];
            if (studentsList.Contains(student)) return student;
            _dictGroup[group].Add(student);
            return student;
        }

        // Done
        public Student GetStudent(int id)
        {
            if (id < 1)
            {
                throw new System.NotImplementedException("Invalid id");
            }

            var currentStudent = Student.CreateInstance("name", id);
            foreach (Student student in _dictGroup.Values.SelectMany(students => students.Where(student => student.Id == id)))
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
                throw new System.NotImplementedException("Invalid name");
            }

            var currentStudent = Student.CreateInstance(name, 0);
            foreach (Student student in _dictGroup.Values.SelectMany(students => students.Where(student => student.Name == name)))
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
                throw new NotImplementedException("Invalid group");
            }

            if (!uint.TryParse(groupName[2].ToString(), out uint courseNumber))
            {
                throw new NotImplementedException("Invalid Group course");
            }

            string groupNameString = groupName.Substring(3, 2);
            if (!uint.TryParse(groupNameString, out uint groupNumber))
            {
                throw new NotImplementedException("Invalid Group name");
            }

            var currentGroup = Group.CreateInstance(groupNumber, CourseNumber.CreateInstance(courseNumber));
            foreach (Group @group in _dictGroup.Keys.Where(@group => @group.GroupNumber == groupNumber || Equals(@group.Number, CourseNumber.CreateInstance(courseNumber))))
            {
                currentGroup.Number = CourseNumber.CreateInstance(courseNumber);
                currentGroup.GroupNumber = groupNumber;
            }

            return _dictGroup[currentGroup];
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            if (courseNumber.Number is <= 0 or > 4)
            {
                throw new NotImplementedException("Invalid Course");
            }

            return (from @group in _dictGroup.Keys where Equals(@group.Number, courseNumber) select _dictGroup[@group]).FirstOrDefault();
        }

        public Group FindGroup(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName) || groupName.Length != 5)
            {
                throw new NotImplementedException("Invalid group");
            }

            if (!uint.TryParse(groupName[2].ToString(), out uint courseNumber))
            {
                throw new NotImplementedException("Invalid Group course");
            }

            string currentGroupName = groupName.Substring(3, 2);
            if (!uint.TryParse(currentGroupName, out uint groupNumber))
            {
                throw new NotImplementedException("Invalid Group name");
            }

            var currentGroup = Group.CreateInstance(groupNumber, CourseNumber.CreateInstance(courseNumber));
            foreach (Group @group in _dictGroup.Keys.Where(@group => Equals(@group.Number, CourseNumber.CreateInstance(courseNumber)) || @group.GroupNumber == groupNumber))
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
                throw new NotImplementedException("Invalid Course");
            }

            return _dictGroup.Keys.Where(@group => Equals(@group.Number, courseNumber)).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (student.Id < 0 || student.Id > _id)
            {
                throw new NotImplementedException("Invalid Student");
            }

            foreach (List<Student> students in from students in _dictGroup.Values from currentStudent in students where Equals(currentStudent, student) select students)
            {
                if (!students.Contains(student))
                {
                    throw new NotImplementedException("Student is not in this group");
                }

                students.Remove(student);
            }

            _dictGroup[newGroup].Add(student);
        }
    }
}