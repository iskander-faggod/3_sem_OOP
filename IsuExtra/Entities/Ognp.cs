using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Ognp
    {
        private readonly List<Stream> _streamsList;
        private readonly string _facultyName;
        public Ognp(string facultyName)
        {
            _facultyName = facultyName;
            _streamsList = new List<Stream>();
        }

        public IReadOnlyList<Stream> InformationAboutStreams => _streamsList;

        public void AddNewStream(Stream stream)
        {
            if (stream is null)
            {
                throw new IsuExtraException("Invalid stream");
            }

            _streamsList.Add(stream);
        }

        public Stream InformationAboutStream(Stream currentStream)
        {
            if (currentStream is null)
            {
                throw new IsuExtraException("Invalid stream");
            }

            return _streamsList.FirstOrDefault(stream => Equals(stream, currentStream));
        }

        public string GetFacultyName() => _facultyName;

        public override int GetHashCode() => HashCode.Combine(_facultyName);

        public override bool Equals(object obj) => obj is Ognp ognp && ognp._facultyName == _facultyName;
    }
}