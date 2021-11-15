using System;

namespace Banks.Tools
{
    public class BankCommandExecutionExecption : Exception
    {
        public BankCommandExecutionExecption()
        {
        }

        public BankCommandExecutionExecption(string message)
            : base(message)
        {
        }

        public BankCommandExecutionExecption(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}