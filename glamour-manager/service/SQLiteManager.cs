using Lucene.Net.Search;
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

        public async Task<bool> MajorUpdateDatabase()
        {
            ApiClient apiClient = new();
            List<FfxivItem> allItems = new();
            allItems = await apiClient.getAllItems();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        INSERT INTO Items (Id, Icon, Name, Url)
                        VALUES ($id, $icon, $name, $url)
                    ";
                    foreach (FfxivItem item in allItems)
                    {
                        Console.WriteLine($"id: {item.Id}, icon: {item.Icon}, name: {item.Name}, url: {item.Url}");
                        command.Parameters.AddWithValue("$id", item.Id);
                        command.Parameters.AddWithValue("$icon", item.Icon);
                        command.Parameters.AddWithValue("$name", item.Name);
                        command.Parameters.AddWithValue("$url", item.Url);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }


                    transaction.Commit();
                }
            }

            return true;
        }

        private void InsertItem(FfxivItem item)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO Items (Id, Icon, Name, Url)
                VALUES ($id, $icon, $name, $url)
            ";
                command.Parameters.AddWithValue("$id", item.Id);
                command.Parameters.AddWithValue("icon", item.Icon);
                command.Parameters.AddWithValue("$name", item.Name);
                command.Parameters.AddWithValue("$url", item.Url);
                command.ExecuteNonQuery();
            }
        }
    }
}