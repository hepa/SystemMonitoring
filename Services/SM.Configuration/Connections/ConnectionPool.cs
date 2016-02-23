using System;
using System.Collections.Generic;
using SM.Core.Extensions;

namespace SM.Configuration.Connections
{
    public abstract class ConnectionPool<T>
    {
        private static Dictionary<T, DateTime> availableConnections = new Dictionary<T, DateTime>();
        private static Dictionary<T, DateTime> usedConnection = new Dictionary<T, DateTime>();

        public abstract T Create();
        public abstract bool Validate(T connection);

        public abstract void Close();

        internal readonly int TimeoutInMinutes = 60;

        public T GetConnection()
        {
            lock (availableConnections)
            {
                if (!availableConnections.IsEmpty())
                {
                    foreach (var entry in availableConnections)
                    {
                        var connection = entry.Key;
                        var creationDate = entry.Value;

                        if (Validate(connection) && !IsTimedOut(creationDate))
                        {
                            availableConnections.Remove(connection);
                            usedConnection.Add(connection, creationDate);

                            return connection;
                        }

                        availableConnections.Remove(connection);
                        Close();
                    }
                }

                var conn = Create();
                usedConnection.Add(conn, DateTime.UtcNow);

                return conn;
            }
        }

        private bool IsTimedOut(DateTime creationTime)
        {
            return creationTime.AddMinutes(TimeoutInMinutes) < DateTime.UtcNow;
        }
    }
}