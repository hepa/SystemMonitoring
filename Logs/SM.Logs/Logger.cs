using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SM.Logs.Implementations.EventViewLog;

namespace SM.Logs
{
    /// <summary>
    /// 
    /// </summary>
    public static class Logger
    {
        private static readonly List<Log> LogModules;

        static Logger()
        {
            LogModules = new List<Log> { new EventViewLog() };
        }

        public static void Trace(string message, [CallerFilePath] string file = "",
            [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            LogModules.ForEach(module => module.Trace(message, file, method, line));
        }

        public static void Debug(string message, [CallerFilePath] string file = "",
            [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            LogModules.ForEach(module => module.Debug(message, file, method, line));
        }

        public static void Info(string message, [CallerFilePath] string file = "",
            [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            LogModules.ForEach(module => module.Info(message, file, method, line));
        }

        public static void Warn(string message, [CallerFilePath] string file = "",
            [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            LogModules.ForEach(module => module.Warn(message, file, method, line));
        }

        public static void Error(string message, [CallerFilePath] string file = "",
            [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            LogModules.ForEach(module => module.Error(message, file, method, line));
        }

        public static void Fatal(string message, [CallerFilePath] string file = "",
            [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            LogModules.ForEach(module => module.Fatal(message, file, method, line));
        }
    }
}