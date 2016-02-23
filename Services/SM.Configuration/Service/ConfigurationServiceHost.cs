using System.Data.SQLite;
using MainDatabase;
using SM.Core.Services;

namespace SM.Configuration.Service
{
    public class ConfigurationServiceHost : SMServiceHost
    {
        public override void OnStartUp()
        {
            MainDB.CreateDB();
            var sqLiteCommand = MainDB.Current.CreateCommand();
            sqLiteCommand.CommandText = @"CREATE TABLE `System` (
	                `SystemID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                `VirtualMemoryCommited`	INTEGER,
	                `VirtualMemoryAvailable`	INTEGER,
	                `VirtualMemoryLoad`	INTEGER,
	                `PhysicalMemoryUsed`	INTEGER,
	                `PhysicalMemoryAvailable`	INTEGER,
	                `PhysicalMemoryLoad`	INTEGER
                    )";
            var row = sqLiteCommand.ExecuteNonQuery();
            sqLiteCommand.CommandText =
                "INSERT INTO `System`(`SystemID`,`VirtualMemoryCommited`,`VirtualMemoryAvailable`,`VirtualMemoryLoad`,`PhysicalMemoryUsed`,`PhysicalMemoryAvailable`,`PhysicalMemoryLoad`) VALUES (1,1,2,3,4,5,6);";
            row = sqLiteCommand.ExecuteNonQuery();
        }
    }
}