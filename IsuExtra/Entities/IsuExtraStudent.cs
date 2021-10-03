using System;
using System.Collections.Generic;
using Isu.Entities;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class IsuExtraStudent : Student
    {
        private const int _maxSubjectCount = 2;
        private List<Ognp> _ognpList;
        private IsuExtraGroup _group;

        public IsuExtraStudent(string name, IsuExtraGroup @group, int id)
            : base(name, id)
        {
            _group = @group;
            _ognpList = new List<Ognp>();
        }

        public void EnrollmentOnOgnp(Ognp ognp, Stream stream)
        {
            if (ognp is null || stream is null) throw new IsuExtraException("Invalid data");
            if (ognp.GetFacultyName() == _group.GetFacultyName())
                throw new IsuExtraException("Student can't be enroll on his faculty course");
            if (_ognpList.Contains(ognp)) throw new IsuExtraException("Student already enrolled on a course");
            if (_ognpList.Count == _maxSubjectCount)
                throw new IsuExtraException("Student can be enrolled only on two subjects");

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

        public IsuExtraGroup InformationAboutStudentGroup() => _group;
        public IReadOnlyList<Ognp> InformationAboutStudentOgnps() => _ognpList;
        public string InformationAboutStudentName() => Name;
    }
}