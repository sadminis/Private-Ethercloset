using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
namespace GlamourManager
{
    public class SQLiteManager
    {
        private readonly string _connectionString;

        public SQLiteManager()
        {
            // Initialize the connection string
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.db");
            _connectionString = $"Data Source={dbPath}";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                CREATE TABLE IF NOT EXISTS Items (
                    Id INTEGER PRIMARY KEY,
                    Icon TEXT NOT NULL,
                    Name TEXT NOT NULL,
                    Url TEXT NOT NULL
                )
            ";
                command.ExecuteNonQuery();
            }
        }

        public void InsertItem(string name)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO Items (Name)
                VALUES ($name)
            ";
                command.Parameters.AddWithValue("$name", name);
                command.ExecuteNonQuery();
            }
        }
    }
}