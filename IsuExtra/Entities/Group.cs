using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Group
    {
        private const int MaxGroupNumber = 14;
        private const int MaxCourseNumber = 4;
        private const int MaxGroupLength = 5;
        private readonly List<Student> _studentsList;
        private readonly List<Lesson> _lessonsList;
        private readonly CourseNumber _courseNumber;
        private readonly uint _groupNumber;
        private readonly string _groupName;
        private readonly string _facultyName;

        public Group(string groupName, List<Lesson> lessonsList)
        {
            _groupName = groupName;
            _facultyName = Convert.ToString(groupName[0]);
            _groupNumber = Convert.ToUInt16(groupName.Substring(3, 2));
            _courseNumber = new CourseNumber(Convert.ToUInt16(groupName.Substring(2, 1)));

            if (_groupNumber > MaxGroupNumber)
            {
                throw new IsuExtraException($"Invalid group number, group number - {_groupNumber}");
            }

            if (_courseNumber.Number > MaxCourseNumber)
            {
                throw new IsuExtraException($"Invalid course number, course number - {_courseNumber}");
            }

            if (string.IsNullOrWhiteSpace(groupName) || groupName.Length != MaxGroupLength)
            {
                throw new IsuExtraException($"Invalid group, with name - {groupName}");
            }

            _studentsList = new List<Student>();
            _lessonsList = new List<Lesson>();
        }

        public void AddStudent(Student student)
        {
            if (_studentsList.Count > MaxGroupLength)
            {
                throw new IsuExtraException($"Group is full - {_studentsList.Count}");
            }

            if (student is null)
            {
                throw new IsuExtraException($"Invalid student data");
            }

            _studentsList.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            if (student is null)
            {
                throw new IsuExtraException($"Invalid student data");
            }

            _studentsList.Remove(student);
        }

        public Student FindStudent(Student currentStudent)
        {
            return _studentsList.FirstOrDefault(student => Equals(student, currentStudent));
        }

        public void AddLesson(Lesson lesson)
        {
            if (lesson is null)
            {
                throw new IsuExtraException("Invalid lesson data");
            }

            _lessonsList.Add(lesson);
        }

        public void RemoveLesson(Lesson lesson)
        {
            if (lesson is null)
            {
                throw new IsuExtraException("Invalid lesson data");
            }

            _lessonsList.Remove(lesson);
        }

        public string GetFacultyName() => _facultyName;
        public IReadOnlyList<Lesson> InformationAboutLessons() => _lessonsList;
        public IReadOnlyList<Student> InformationAboutStudents() => _studentsList;
        public CourseNumber GetCourseNumber() => _courseNumber;

        public override int GetHashCode()
        {
            return HashCode.Combine(_courseNumber, _groupNumber, _groupName);
        }

        public override bool Equals(object obj)
        {
            if (obj is Group @group)
            {
                return @group._courseNumber == _courseNumber
                       && @group._groupName == _groupName
                       && @group._groupNumber == _groupNumber;
            }

            return false;
        }
    }
}