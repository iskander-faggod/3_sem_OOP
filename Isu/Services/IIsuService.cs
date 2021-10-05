using System.Collections.Generic;
using Isu.Entities;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group<Student> AddGroup(string name);
        Student AddStudent(Group<Student> group, string name);

        Student GetStudent(int id);
        Student FindStudent(string name);
        List<Student> FindStudents(string groupName);
        List<Student> FindStudents(CourseNumber courseNumber);

        Group<Student> FindGroup(string groupName);
        List<Group<Student>> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group<Student> newGroup);
    }
}