using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Stream
    {
        private readonly uint _streamNumber;
        private readonly List<Student> _studentsList;
        private readonly List<Lesson> _lessonsList;

        public Stream(uint streamNumber)
        {
            _streamNumber = streamNumber;
            _lessonsList = new List<Lesson>();
            _studentsList = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            if (student is null)
            {
                throw new IsuExtraException($"Invalid student data - {student}");
            }

            _studentsList.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            if (student is null)
            {
                throw new IsuExtraException($"Invalid student data - {student}");
            }

            _studentsList.Remove(student);
        }

        public IReadOnlyList<Student> InformationAboutStudents() => _studentsList;
        public IReadOnlyList<Lesson> InformationAboutLessons() => _lessonsList;
        public void AddLesson(Lesson lesson) => _lessonsList.Add(lesson);

        public override int GetHashCode() => HashCode.Combine(_streamNumber);

        public override bool Equals(object obj) => obj is Stream stream && stream._streamNumber == _streamNumber;
    }
}