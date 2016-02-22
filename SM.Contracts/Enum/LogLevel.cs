using System;

namespace SM.Contracts.Enum
{
    [Flags]
    public enum LogLevel
    {
        /// <summary>
        /// The message weight.
        /// </summary>        
        None = 0x0,
        Fatal = 0x1,
        Error = 0x2,
        Warn = 0x4,
        Info = 0x8,
        Debug = 0x10,
        Trace = 0x20
    }
}