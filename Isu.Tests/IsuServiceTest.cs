using System.Linq;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService = IsuService.CreateInstance();

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = null;
        }
        
        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group m3101 = _isuService.AddGroup("M3101");
            Student newStudent = _isuService.AddStudent(m3101, "Iskander");

            if (m3101 != null && newStudent != null && Equals(newStudent, _isuService.FindStudent(newStudent.Name)) && _isuService.FindStudents("M3101").All(student => Equals(student, newStudent)))
            {
                Assert.Fail("AddStudentToGroup_StudentHasGroupAndGroupContainsStudent");
            }
        }
        
        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group @group = _isuService.AddGroup("M3201");
                for (int i = 0; i < 26; i++)
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
                Group group = _isuService.AddGroup("XUY01");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group oldGroup = _isuService.AddGroup("M3108");
                Group newGroup = _isuService.AddGroup("M3201");
                _isuService.AddStudent(oldGroup, "Iskander");
                _isuService.ChangeStudentGroup(_isuService.FindStudent("Iskander"), newGroup);
            });
        }
    }
}