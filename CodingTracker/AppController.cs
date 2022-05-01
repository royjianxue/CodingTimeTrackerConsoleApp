using System.Data.SQLite;
using CodingTracker;

public class AppController
{
    public static void Insert(TableColumnHeaders headers)
    {
        using(var conn = Database.CreateConnection())
        {
            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO CodingTracker 
                                        (Date, Hour)
                                        VALUES 
                                        (@Date, @Hour)";
                cmd.Parameters.AddWithValue("@Date", headers.Date);
                cmd.Parameters.AddWithValue("@Hour", headers.Hours);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void GetTable()
    {
        List<TableColumnHeaders> tableData = new List<TableColumnHeaders>();

        using (var conn = Database.CreateConnection())
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM CodingTracker";
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tableData.Add(new TableColumnHeaders
                            {
                                Id = reader.GetInt32(0),
                                Date = reader.GetString(1),
                                Hours = reader.GetDouble(2),
                            });
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo Record found..");
                    }
                }
            }
        }
        TableVisualization.ShowTable(tableData);
    }

    internal static void GetHours()
    {
        using(var conn = Database.CreateConnection())
        {
            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT SUM(Hour) FROM CodingTracker";
                var hours = cmd.ExecuteScalar();
                double total = Convert.IsDBNull(hours) ? 0 : (double)hours;

                if (total == 0)
                {
                    Console.WriteLine("\nError, You probably do not have any hours yet..");
                }
                else
                {
                    Console.Write($"\nYou have accumulated a total of {total} hours in coding. Please keep it up!\n");
                }

            }
        }
    }

    internal static void Delete(TableColumnHeaders headers)
    {
        using(var conn = Database.CreateConnection())
        {
            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM CodingTracker WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", headers.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    internal static bool ValidateIDNumberExist(TableColumnHeaders headers)
    {
        using (var conn = Database.CreateConnection())
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM CodingTracker WHERE Id = @Id)";
                cmd.Parameters.AddWithValue("@Id", headers.Id);

                int checkQuery = Convert.ToInt32(cmd.ExecuteScalar());
                if (checkQuery == 0)
                {
                    Console.WriteLine("\n\nNo Record Exist Yet.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }

    internal static void Update(TableColumnHeaders headers)
    {
        using(var conn = Database.CreateConnection())
        {
            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"UPDATE CodingTracker SET Date = @Date, Hour = @Hour Where Id = @Id";
                cmd.Parameters.AddWithValue("@Date", headers.Date);
                cmd.Parameters.AddWithValue("@Hour", headers.Hours);
                cmd.Parameters.AddWithValue("@Id", headers.Id);                
                cmd.ExecuteNonQuery();
            }
        }
    }

 
}