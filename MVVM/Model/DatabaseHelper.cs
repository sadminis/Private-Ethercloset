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
}