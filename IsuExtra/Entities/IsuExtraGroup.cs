using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class IsuExtraGroup : Group<IsuExtraStudent>
    {
        private readonly List<Lesson> _lessonsList;
        private readonly string _facultyName;

        public IsuExtraGroup(string groupName)
            : base(groupName)
        {
            _facultyName = Convert.ToString(groupName[0]);
            _lessonsList = new List<Lesson>();
        }

        public IsuExtraStudent FindStudent(IsuExtraStudent currentStudent)
        {
            if (currentStudent is null) throw new IsuExtraException("Invalid student data");
            if (!StudentsList.Contains(currentStudent)) throw new IsuExtraException("Student not registered");
            return StudentsList.FirstOrDefault(student => Equals(student, currentStudent));
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
    }
}