using System;

namespace SM.Contracts.Exceptions
{
    public class SMException : Exception
    {
        public int ErrorCode { get; set; }

        public SMException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public SMException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}