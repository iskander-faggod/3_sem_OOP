using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Service
{
    public class IsuService
    {
        private readonly List<IsuExtraGroup> _listGroups;
        private readonly List<Ognp> _listOgnps;
        private int _studentsId = 0;

        public IsuService()
        {
            _listGroups = new List<IsuExtraGroup>();
            _listOgnps = new List<Ognp>();
        }

        public IsuExtraGroup AddGroup(string name)
        {
            var newGroup = new IsuExtraGroup(name);

            if (_listGroups.Contains(newGroup))
            {
                throw new IsuExtraException("Group is already created");
            }

            _listGroups.Add(newGroup);
            return newGroup;
        }

        public IsuExtraStudent AddStudent(IsuExtraGroup @group, string name)
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
            var currentStudent = new IsuExtraStudent(name, group, _studentsId);
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

        public void RegisterStudentOnOgnp(IsuExtraStudent student, Ognp ognp, Stream stream)
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

        public void UnregisterStudentOnOgnp(IsuExtraStudent student, Ognp ognp)
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

        public List<IsuExtraGroup> InformationAboutGroupsWithCurrentCourse(uint courseNumber) =>
            _listGroups.FindAll(group => @group.CourseNumber.Number == courseNumber);

        public IReadOnlyList<Stream> InformationAboutStreamsWithCurrentCourse(Ognp ognp)
        {
            if (ognp is null) throw new IsuExtraException("Invalid ognp data");
            return ognp.InformationAboutStreams.ToList();
        }

        public IReadOnlyList<Stream> InformationAboutStudentsWithCurrentOgnp(Ognp ognp) => ognp.InformationAboutStreams;

        public IReadOnlyList<IsuExtraStudent> InformationAboutStudentsWithoutOgnp(IsuExtraGroup @group)
        {
            if (group is null) throw new IsuExtraException("Invalid group data");
            return @group.StudentsList
                .Cast<IsuExtraStudent>()
                .Where(student => student.InformationAboutStudentOgnps().Count == 0)
                .ToList();
        }

        public bool CheckForTheConflictsInSchedule(IsuExtraGroup @group, Stream stream, Lesson oLesson)
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