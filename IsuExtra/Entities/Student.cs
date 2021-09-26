using System;
using System.Collections.Generic;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Student
    {
        private List<Ognp> _ognpList;

        public Student(string name, int id, Group @group)
        {
            if (id < 1)
            {
                throw new IsuExtraException($"Invalid id, id - {id}");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new IsuExtraException($"Invalid name, name - {name}");
            }

            Name = name;
            Id = id;
            Group = @group;
            _ognpList = new List<Ognp>();
        }

        private string Name { get; }
        private int Id { get; }

        private Group Group { get; }

        public void EnrollmentOnOgnp(Ognp ognp, Stream stream)
        {
            if (ognp is null || stream is null)
            {
                throw new IsuExtraException("Invalid data");
            }

            if (ognp.GetFacultyName() == Group.GetFacultyName())
            {
                throw new IsuExtraException("Student can't be enroll on his faculty course");
            }

            if (_ognpList.Contains(ognp))
            {
                throw new IsuExtraException("Student already enrolled on a course");
            }

            _ognpList.Add(ognp);
            ognp.InformationAboutStream(stream).AddStudent(this);
        }

        public void UnEnrollmentOnOgnp(Ognp ognp)
        {
            if (ognp is null)
            {
                throw new IsuExtraException("Invalid data");
            }

            if (!_ognpList.Contains(ognp))
            {
                throw new IsuExtraException($"Student is not enrolled on a course - {ognp}");
            }

            _ognpList.Remove(ognp);
        }

        public override int GetHashCode() => HashCode.Combine(Name, Id, Group);

        public override bool Equals(object obj)
        {
            if (obj is Student student)
            {
                return student.Id == Id
                       && student.Name == Name
                       && student.Group == Group;
            }

            return false;
        }
    }
}