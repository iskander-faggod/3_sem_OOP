using System;

namespace Banks.Tools
{
    public class NonRevertableCommandExecption : Exception
    {
        public NonRevertableCommandExecption()
        {
        }

        public NonRevertableCommandExecption(string message)
            : base(message)
        {
        }

        public NonRevertableCommandExecption(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}