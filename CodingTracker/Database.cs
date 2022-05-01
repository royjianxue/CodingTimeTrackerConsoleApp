using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Configuration;

namespace CodingTracker
{
    public class Database
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Name"].ConnectionString;
        public static SQLiteConnection CreateConnection()
        {   
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open(); 
            return conn;
        }
        public static void CreateTable()
        {
            using(var conn = CreateConnection())
            {
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        @"
                            CREATE TABLE IF NOT EXISTS CodingTracker 
                            (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Date TEXT,
                                Hour DOUBLE
                            )
                             ";
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
