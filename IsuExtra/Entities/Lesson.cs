using System;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        private readonly int _lectureRoomNumber;
        private readonly string _teacherName;
        private readonly string _lessonName;

        public Lesson(DateTime lessonBegin, DateTime lessonEnd, int lectureRoomNumber, string teacherName, string lessonName)
        {
            BeginLessonTime = lessonBegin;
            EndLessonTime = lessonEnd;
            _lectureRoomNumber = lectureRoomNumber;
            _teacherName = teacherName;
            _lessonName = lessonName;
        }

        public DateTime BeginLessonTime { get; }
        public DateTime EndLessonTime { get; }

        public override int GetHashCode()
        {
            return HashCode.Combine(_lessonName, _teacherName, _lectureRoomNumber, BeginLessonTime, EndLessonTime);
        }

        public override bool Equals(object obj)
        {
            if (obj is Lesson lesson)
            {
                return lesson._lessonName == _lessonName
                       && lesson._teacherName == _teacherName
                       && lesson._lectureRoomNumber == _lectureRoomNumber
                       && lesson.BeginLessonTime == BeginLessonTime
                       && lesson.EndLessonTime == EndLessonTime;
            }

            return false;
        }
    }
}