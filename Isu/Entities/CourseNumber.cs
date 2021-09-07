#nullable enable
namespace Isu.Classes
{
    public class CourseNumber
    {
        private CourseNumber(uint number)
        {
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
            if (obj is null)
            {
                return false;
            }

            var a = (CourseNumber)obj;
            return a.Number == this.Number;
        }
    }
}