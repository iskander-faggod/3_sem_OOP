using System;
using Isu.Classes;

namespace Isu
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var isu = IsuService.CreateInstance();

            var course1 = CourseNumber.CreateInstance(1);
            var course2 = CourseNumber.CreateInstance(2);

            var groupM3101 = new Group(1,  course1);
            var groupM3202 = new Group(2,  course2);

            isu.AddGroup("M3101");
            isu.AddGroup("M3202");

            isu.AddStudent(groupM3101, "Kirill");

            var student = isu.GetStudent(1);
            Console.WriteLine(student.Name);
        }
    }
}
