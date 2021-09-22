using System;
using Isu.Tools;

#nullable enable
namespace Isu.Entities
{
    public class CourseNumber
    {
        private CourseNumber(uint number)
        {
            if (number is <= 0 or > 4)
            {
                throw new IsuException($"Invalid Course, course number - {number}");
            }

            Number = number;
        }

        public uint Number { get; }

        public static CourseNumber CreateInstance(uint number)
        {
            return new CourseNumber(number);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is CourseNumber courseNumber)
            {
                return courseNumber.Number == Number;
            }

            return false;
        }
    }
}