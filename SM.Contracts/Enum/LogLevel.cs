using System;

namespace SM.Contracts.Enum
{
    [Flags]
    public enum LogLevel
    {
        /// <summary>
        /// The message weight.
        /// </summary>        
        None = 0,
        Fatal = 1,
        Error = 1 << 1,
        Warn = 1 << 2,
        Info = 1 << 3,
        Debug = 1 << 4,
        Trace = 1 << 5

    }
}