using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = IsuService.CreateInstance();
        }
        
        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group m3101 = _isuService.AddGroup("M3101");
            Student newStudent = _isuService.AddStudent(m3101, "Iskander");
            List<Student> studentsList = _isuService.DictGroup[m3101];
            Student student = _isuService.DictGroup
                .SelectMany(students => students.Value)
                .First(student => Equals(student, newStudent));
            Assert.True(Equals(student, newStudent));
            Assert.True(studentsList.Contains(student));
        }
        
        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group @group = _isuService.AddGroup("M3201");
                for (int i = 0; i < 27; i++)
                {
                    _isuService.AddStudent(group, "crash");
                }
            });
        }
        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3222201");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group oldGroup = _isuService.AddGroup("M3108");
            Group newGroup = _isuService.AddGroup("M3201");
            Student newStudent = _isuService.FindStudent("Iskander");
            _isuService.AddStudent(oldGroup, "Iskander");
            _isuService.ChangeStudentGroup(newStudent, newGroup);
            Assert.False(_isuService.DictGroup[oldGroup].Contains(newStudent));
        }
    }
}