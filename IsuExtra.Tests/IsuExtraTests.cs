using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities;
using IsuExtra.Service;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddNewStudent_True()
        {
            IsuExtraGroup group = _isuService.AddGroup("M3201");
            IsuExtraStudent student = _isuService.AddStudent(group, "Iskander Kudashev");
            _isuService.AddStudent(group, student.InformationAboutStudentName());
            Assert.True(group.StudentsList.Contains(student));
        }

        [Test]
        public void RegisterStudentOnOgnp()
        {
            IsuExtraGroup group = _isuService.AddGroup("M3201");
            IsuExtraStudent student = _isuService.AddStudent(group, "Iskander Kudashev");
            _isuService.AddStudent(group, student.InformationAboutStudentName());

            var ognp = new Ognp("B");
            var stream = new Stream(1);
            _isuService.RegistratedOgnpOnStream(ognp, stream);
            _isuService.RegisterStudentOnOgnp(student, ognp, stream);
            Assert.True(ognp.InformationAboutStreams.Contains(stream));
            Assert.True(stream.InformationAboutStudents().Contains(student));
        }

        [Test]
        public void UnregisterStudentOnOgnp()
        {
            IsuExtraGroup group = _isuService.AddGroup("M3201");
            IsuExtraStudent student = _isuService.AddStudent(group, "Iskander Kudashev");
            _isuService.AddStudent(group, student.InformationAboutStudentName());

            var ognp = new Ognp("B");
            var stream = new Stream(1);
            _isuService.RegistratedOgnpOnStream(ognp, stream);
            _isuService.RegisterStudentOnOgnp(student, ognp, stream);
            Assert.True(ognp.InformationAboutStreams.Contains(stream));
            Assert.True(stream.InformationAboutStudents().Contains(student));
            _isuService.UnregisterStudentOnOgnp(student, ognp);
            Assert.False(student.InformationAboutStudentOgnps().Contains(ognp));
        }

        [Test]
        public void GetInformationAboutStreamsByCourseNumber()
        {
            IsuExtraGroup group = _isuService.AddGroup("M3201");
            IsuExtraStudent student = _isuService.AddStudent(group, "Iskander Kudashev");
            _isuService.AddStudent(group, student.InformationAboutStudentName());

            var ognp = new Ognp("B");
            var stream = new Stream(1);
            _isuService.RegistratedOgnpOnStream(ognp, stream);
            _isuService.RegisterStudentOnOgnp(student, ognp, stream);
            IReadOnlyList<Stream> some = _isuService.InformationAboutStreamsWithCurrentCourse(ognp);
            Assert.AreNotEqual(some, null);
        }

        [Test]
        public void GetInformationAboutStudentsByGroup()
        {
            IsuExtraGroup group = _isuService.AddGroup("M3201");
            IsuExtraStudent student = _isuService.AddStudent(group, "Iskander Kudashev");
            _isuService.AddStudent(group, student.InformationAboutStudentName());

            var ognp = new Ognp("B");
            var stream = new Stream(1);
            _isuService.RegistratedOgnpOnStream(ognp, stream);
            _isuService.RegisterStudentOnOgnp(student, ognp, stream);
            IReadOnlyList<Stream> some = _isuService.InformationAboutStudentsWithCurrentOgnp(ognp);
            Assert.AreNotEqual(_isuService.InformationAboutStudentsWithCurrentOgnp(ognp), null);
        }

        [Test]
        public void GetInformationsAboutStudentsWithoutEnrollmentOnOgnp_StudentsListWithOutStudent()
        {
            IsuExtraGroup group = _isuService.AddGroup("M3201");
            IsuExtraStudent student = _isuService.AddStudent(group, "Iskander Kudashev");
            IsuExtraStudent student2 = _isuService.AddStudent(group, "Mikhail Libchenko");
            IsuExtraStudent student3 = _isuService.AddStudent(group, "Sasha Miroshichenko");

            var ognp = new Ognp("B");
            var stream = new Stream(1);
            _isuService.RegistratedOgnpOnStream(ognp, stream);
            _isuService.RegisterStudentOnOgnp(student, ognp, stream);
            IReadOnlyList<IsuExtraStudent> some = _isuService.InformationAboutStudentsWithoutOgnp(group);
            Assert.True(some.Contains(student2));
            Assert.True(some.Contains(student3));
        }

        [Test]
        public void GetInformationAboutConflictsInSchedule()
        {
            var group = new IsuExtraGroup("M3201");
            IsuExtraStudent student = _isuService.AddStudent(group, "Iskander Kudashev");
            IsuExtraStudent student2 = _isuService.AddStudent(group, "Mikhail Libchenko");
            IsuExtraStudent student3 = _isuService.AddStudent(group, "Sasha Miroshichenko"); 
            _isuService.AddStudent(group, student.InformationAboutStudentName());
            _isuService.AddStudent(group, student2.InformationAboutStudentName());
            _isuService.AddStudent(group, student3.InformationAboutStudentName());

            var ognp = new Ognp("B");
            var stream = new Stream(1);
            _isuService.RegistratedOgnpOnStream(ognp, stream);
            _isuService.RegisterStudentOnOgnp(student, ognp, stream);

            DateTime date1Begin = new DateTime(2021, 9, 28, 8, 20, 0);
            DateTime date1End = date1Begin.AddHours(1.5);
            DateTime date2Begin = new DateTime(2021, 9, 28, 11, 40, 0);
            DateTime date2End = date2Begin.AddHours(1.5);
            DateTime date3Begin = new DateTime(2021, 9, 28, 9, 00, 0);
            DateTime date3End = date3Begin.AddHours(1.5);
            DateTime date4Begin = new DateTime(2021, 9, 28, 15, 20, 0);
            DateTime date4End = date3Begin.AddHours(1.5);
            var lesson1 = new Lesson(date1Begin, date1End, 151, "Fredi Cats", "OOP");
            var lesson2 = new Lesson(date2Begin, date2End, 466, "Alexandr Mayatin", "OS");
            var lesson3 = new Lesson(date3Begin, date3End, 337, "Noname professor", "SOS");
            var lesson4 = new Lesson(date4Begin, date4End, 100, "Noname professor", "HELLO");

            stream.AddLesson(lesson1);
            stream.AddLesson(lesson2);
            Assert.False(_isuService.CheckForTheConflictsInSchedule(group, stream, lesson3));
            Assert.True(_isuService.CheckForTheConflictsInSchedule(group, stream, lesson4));
        }
    }
}