using System.Collections.Generic;
using System.Linq;
using Isu.Tools;
using IsuExtra.Entities;
using IsuExtra.Tools;
using Microsoft.VisualBasic;

namespace IsuExtra.Service
{
    public class IsuService
    {
        private readonly List<Group> _listGroups;
        private readonly List<Ognp> _listOgnps;
        private int _studentsId = 0;

        public IsuService()
        {
            _listGroups = new List<Group>();
            _listOgnps = new List<Ognp>();
        }

        public void AddGroup(Group @group)
        {
            if (group is null)
            {
                throw new IsuExtraException("Invalid group data");
            }

            _listGroups.Add(group);
        }

        public Student AddStudent(Group @group, string name)
        {
            if (group is null)
            {
                throw new IsuExtraException("Invalid group data");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new IsuExtraException("Invalid student name data");
            }

            _studentsId++;
            var currentStudent = new Student(name, group);
            group.AddStudent(currentStudent);
            return currentStudent;
        }

        public Ognp AddOgnp(string facultyName)
        {
            if (string.IsNullOrEmpty(facultyName))
            {
                throw new IsuExtraException($"Invalid facultyName - {facultyName}");
            }

            if (_listOgnps.Any(currOgnp => currOgnp.GetFacultyName() == facultyName))
            {
                throw new IsuExtraException("Ognp is already created");
            }

            return new Ognp(facultyName);
        }

        public void RegistratedOgnpOnStream(Ognp ognp, Stream stream)
        {
            if (ognp is null)
            {
                throw new IsuExtraException("Invalid ognp data");
            }

            ognp.AddNewStream(stream);
        }

        public void RegisterStudentOnOgnp(Student student, Ognp ognp, Stream stream)
        {
            if (student is null)
            {
                throw new IsuExtraException("Invalid student data");
            }

            if (ognp is null)
            {
                throw new IsuExtraException("Invalid ognp data");
            }

            if (stream is null)
            {
                throw new IsuExtraException("Invalid stream data");
            }

            student.EnrollmentOnOgnp(ognp, stream);
        }

        public void UnregisterStudentOnOgnp(Student student, Ognp ognp)
        {
            if (student is null)
            {
                throw new IsuExtraException("Invalid student data");
            }

            if (ognp is null)
            {
                throw new IsuExtraException("Invalid ognp data");
            }

            student.UnEnrollmentOnOgnp(ognp);
        }

        public List<Group> InformationAboutGroupsWithCurrentCourse(uint courseNumber) =>
            _listGroups.FindAll(group => @group.GetCourseNumber().Number == courseNumber);

        public IReadOnlyList<Stream> InformationAboutStreamsWithCurrentCourse(Ognp ognp)
        {
            if (ognp is null) throw new IsuExtraException("Invalid ognp data");
            return ognp.InformationAboutStreams.ToList();
        }

        public IReadOnlyList<Stream> InformationAboutStudentsWithCurrentOgnp(Ognp ognp) => ognp.InformationAboutStreams;

        public IReadOnlyList<Student> InformationAboutStudentsWithoutOgnp(Group @group)
        {
            if (group is null) throw new IsuExtraException("Invalid group data");
            return group.InformationAboutStudents()
                .Where(student => student.InformationAboutStudentOgnps().Count == 0)
                .ToList();
        }

        public bool CheckForTheConflictsInSchedule(Group @group, Stream stream, Lesson oLesson)
        {
            if (oLesson is null) throw new IsuExtraException("Invalid ognp value");
            if (group is null) throw new IsuExtraException("Invalid group value");
            if (stream is null) throw new IsuExtraException("Invalid stream value");
            return stream.InformationAboutLessons()
                .All(lesson => (lesson.BeginLessonTime > oLesson.EndLessonTime) ||
                               (lesson.EndLessonTime < oLesson.BeginLessonTime));
        }
    }
}