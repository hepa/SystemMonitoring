using System;
using System.Diagnostics;
using SM.Contracts.Enum;

namespace SM.Logs.Implementations.EventViewLog
{
    public class EventViewLog : Log
    {
        public EventViewLog()
        {
            NewMessage += OnNewMessage;
        }

        private static void OnNewMessage(Entry entry)
        {
            const string application = "System Monitoring";
            var source = entry.File;
            var message = string.Format(@"{0} - {1}/{2}():{3}", entry.Message, entry.File, entry.Method, entry.Line);

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, application);
            }                

            switch (entry.Level)
            {
                case LogLevel.Fatal:
                case LogLevel.Error:
                    {
                        EventLog.WriteEntry(source, message, EventLogEntryType.Error);
                        break;
                    }
                case LogLevel.Warn:
                    {
                        EventLog.WriteEntry(source, message, EventLogEntryType.Warning);
                        break;
                    }
                case LogLevel.Info:
                    {
                        EventLog.WriteEntry(source, message, EventLogEntryType.Information);
                        break;
                    }
            }
        }
    }
}