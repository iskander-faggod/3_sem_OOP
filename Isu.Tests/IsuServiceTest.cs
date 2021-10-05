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
        public IsuService IsuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            IsuService = new IsuService(25);
        }
        
        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group<Student> m3101 = IsuService.AddGroup("M3101");
            Student newStudent1 = IsuService.AddStudent(m3101, "Iskander");
            Student newStudent2= IsuService.AddStudent(m3101, "Sasha");
            Student newStudent3 = IsuService.AddStudent(m3101, "Misha");
            
            Assert.True(m3101.StudentsList.Contains(newStudent1));
            Assert.True(m3101.StudentsList.Contains(newStudent2));
            Assert.True(m3101.StudentsList.Contains(newStudent3));
        }
        
        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group<Student> @group = IsuService.AddGroup("M3201");
                for (int i = 0; i < 28; i++)
                {
                    IsuService.AddStudent(group, "crash");
                }
            });
        }
        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group<Student> group = IsuService.AddGroup("M3222201");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group<Student> oldGroup = IsuService.AddGroup("M3108");
            Group<Student> newGroup = IsuService.AddGroup("M3201");
            Student newStudent = IsuService.AddStudent(oldGroup, "NewIskander");
            IsuService.AddStudent(oldGroup, "NewIskander");
            IsuService.ChangeStudentGroup(newStudent, newGroup);
            Assert.AreEqual(newGroup.GetStudentByName("NewIskander"), newStudent);
        }
    }
}