using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDatabase
{
    public static class MainDB
    {
        private static readonly string DB_NAME = "MainDB";

        private static SQLiteConnection _dbConnection;

        public static SQLiteConnection Connection
        {
            get
            {
                if (_dbConnection != null)
                {
                    return _dbConnection;
                }
                else
                {
                    _dbConnection = CreateConnection(GetConnectionString());
                    return _dbConnection;
                }
            }
        }

        public static SQLiteConnection Current
        {
            get
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                return Connection;
            }
        }

        public static void CreateDB()
        {
            SQLiteConnection.CreateFile(string.Format("{0}.sqlite", DB_NAME));            
        }

        private static SQLiteConnection CreateConnection(string connectionString)
        {            
            return new SQLiteConnection(connectionString); 
        }

        private static string GetConnectionString()
        {
            return string.Format("Data Source={0}.sqlite;Version=3;", DB_NAME);
        }
    }
}
