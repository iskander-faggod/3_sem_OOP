#nullable enable
using System;

namespace Isu.Classes
{
    public class Group
    {
        public Group(uint groupNumber, CourseNumber courseNumber)
        {
            GroupNumber = groupNumber;
            Number = courseNumber;
        }

        public CourseNumber Number { get; set; }
        public uint GroupNumber { get; set; }

        public static Group CreateInstance(uint groupNumber, CourseNumber courseNumber)
        {
            return new Group(groupNumber, courseNumber);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            var a = (Group)obj;
            return a.GroupNumber == this.GroupNumber && a.Number.Equals(this.Number);
        }

        public override int GetHashCode() => HashCode.Combine(Number, GroupNumber);
    }
}