using System;

namespace Banks.Tools
{
    public class TransferToAnotherAccountExcpetion : Exception
    {
        public TransferToAnotherAccountExcpetion()
        {
        }

        public TransferToAnotherAccountExcpetion(string message)
            : base(message)
        {
        }

        public TransferToAnotherAccountExcpetion(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}