using System;
using System.Runtime.Serialization;

namespace Isu.Tools
{
    public class IsuException : Exception
    {
        public IsuException()
        {
            throw new NotImplementedException();
        }

        public IsuException(string message)
            : base(message)
        {
            throw new NotImplementedException(message);
        }

        public IsuException(string message, Exception innerException)
            : base(message, innerException)
        {
            throw new Exception(message);
        }

        protected IsuException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}