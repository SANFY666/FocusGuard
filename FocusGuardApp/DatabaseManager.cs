using System;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Data;

namespace FocusGuardApp
{
    public static class DatabaseManager
    {
        // база данних
        private static string dbName = "FocusGuardDB.sqlite";
        private static string connectionString = $"Data Source={dbName}";

        // метод для створення масиву
        public static void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Sessions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        SessionDate TEXT NOT NULL,
                        Category TEXT NOT NULL,
                        ActivityName TEXT NOT NULL,
                        DurationMinutes INTEGER NOT NULL
                    );";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // нова завершена активність
        public static void AddSession(string category, string activityName, int durationMinutes)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = @"
                    INSERT INTO Sessions (SessionDate, Category, ActivityName, DurationMinutes) 
                    VALUES (@date, @category, @activityName, @duration);";

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@activityName", activityName);
                    command.Parameters.AddWithValue("@duration", durationMinutes);

                    command.ExecuteNonQuery();
                }
            }
        }

        // таблиця
        public static DataTable GetAllSessions()
        {
            DataTable table = new DataTable();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // SQL-запит
                string selectQuery = @"
                    SELECT 
                        SessionDate AS 'Дата та час', 
                        Category AS 'Категорія', 
                        ActivityName AS 'Активність', 
                        DurationMinutes AS 'Час (хв)' 
                    FROM Sessions 
                    WHERE DurationMinutes > 0
                    ORDER BY Id DESC;";

                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            return table;
        }

        // метод для повного очищення історії
        public static void ClearAllSessions()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Sessions;";
                using (var command = new SqliteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}