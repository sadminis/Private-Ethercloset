using Private_Ethercloset.MVVM.Model;
using System.Data.SQLite;
using System.Runtime.Versioning;

public class DatabaseHelper
{
    private string connectionString;
    private string databasePath = DirectoryManager.getDatabasePath();
    public DatabaseHelper()
    {
        connectionString = $"Data Source={databasePath};Version=3;";
    }

    public SQLiteConnection GetConnection()
    {
        return new SQLiteConnection(connectionString);
    }

    public int GetItemIdByName(string name)
    {
        // Declare the variable to hold the item id
        if (string.IsNullOrEmpty(name)) return 0;

        int itemId = 0;

        string query = "SELECT id FROM items WHERE name = @name LIMIT 1"; // Using LIMIT 1 for efficiency

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);

                using (var reader = command.ExecuteReader())
                {
                    // Check if there is a result
                    if (reader.Read())
                    {
                        itemId = reader.GetInt32(0); // Get the first column (id)
                    }
                }
            }
        }

        return itemId; // Return the found id or null if not found
    }
}