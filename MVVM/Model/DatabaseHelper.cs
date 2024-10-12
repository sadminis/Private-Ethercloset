using Private_Ethercloset.MVVM.Model;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Versioning;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

public class DatabaseHelper
{
    private string connectionString;
    private string databasePath = DirectoryManager.getDatabasePath();
    private const string DefaultIcon = "026107.png";//礼物盒

    private const int IconNameLength = 10; //6 digits + .png (4)

    private List<string> Dyes = new List<string>
            {
                "无染色",
                "----------------",
                "素雪白染剂",
                "苍白灰染剂",
                "古菩灰染剂",
                "石板灰染剂",
                "木炭灰染剂",
                "煤烟黑染剂",
                "----------------",
                "玫瑰粉染剂",
                "丁香紫染剂",
                "罗兰莓染剂",
                "卫月红染剂",
                "铁锈红染剂",
                "果酒红染剂",
                "珊瑚粉染剂",
                "鲜血红染剂",
                "鲑鱼粉染剂",
                "宝石红染剂",
                "樱桃粉染剂",
                "----------------",
                "日落橙染剂",
                "台地红染剂",
                "树皮棕染剂",
                "巧克力染剂",
                "铁锈棕染剂",
                "钴铁棕染剂",
                "软木棕染剂",
                "卢恩棕染剂",
                "奥猴棕染剂",
                "山羊棕染剂",
                "南瓜橙染剂",
                "橡果棕染剂",
                "果园棕染剂",
                "山栗棕染剂",
                "哥布林染剂",
                "页岩棕染剂",
                "鼹鼠棕染剂",
                "沃土棕染剂",
                "----------------",
                "骸骨白染剂",
                "黄沙棕染剂",
                "沙漠黄染剂",
                "蜂蜜黄染剂",
                "玉米黄染剂",
                "猛豹黄染剂",
                "奶油黄染剂",
                "日影黄染剂",
                "萄干棕染剂",
                "丝雀黄染剂",
                "香草黄染剂",
                "----------------",
                "泥沼绿染剂",
                "妖精绿染剂",
                "青柠绿染剂",
                "苔藓绿染剂",
                "牧草绿染剂",
                "橄榄绿染剂",
                "沼泽绿染剂",
                "苹果绿染剂",
                "仙人掌染剂",
                "猎人绿染剂",
                "口花绿染剂",
                "金龟绿染剂",
                "地神绿染剂",
                "深林绿染剂",
                "天上蓝染剂",
                "绿松蓝染剂",
                "魔花绿染剂",
                "----------------",
                "寒冰蓝染剂",
                "天空蓝染剂",
                "海雾蓝染剂",
                "孔雀蓝染剂",
                "罗海蓝染剂",
                "腐尸蓝染剂",
                "青磷蓝染剂",
                "靛青蓝染剂",
                "油墨蓝染剂",
                "盗龙蓝染剂",
                "东洲蓝染剂",
                "风暴蓝染剂",
                "虚空蓝染剂",
                "皇室蓝染剂",
                "午夜蓝染剂",
                "阴影蓝染剂",
                "深渊蓝染剂",
                "龙骑蓝染剂",
                "松石蓝染剂",
                "----------------",
                "薰衣草染剂",
                "忧郁紫染剂",
                "醋栗紫染剂",
                "鸢尾紫染剂",
                "葡萄紫染剂",
                "莲花粉染剂",
                "蜂鸟粉染剂",
                "仙子梅染剂",
                "帝王紫染剂",
                "丝雀黄染剂",
                "香草黄染剂",
                "----------------",
                "无瑕白染剂",
                "煤玉黑染剂",
                "柔彩粉染剂",
                "黑暗红染剂",
                "黑暗棕染剂",
                "柔彩绿染剂",
                "黑暗绿染剂",
                "柔彩蓝染剂",
                "黑暗蓝染剂",
                "柔彩紫染剂",
                "黑暗紫染剂",
                "----------------",
                "闪耀银染剂",
                "闪耀金染剂",
                "金属红染剂",
                "金属橙染剂",
                "金属黄染剂",
                "金属绿染剂",
                "金属蓝染剂",
                "金属靛染剂",
                "金属紫染剂",
                "炮铜黑染剂",
                "珍珠白染剂",
                "金属铜染剂"
            };


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

    public string GetItemNameByID(int ID)
    {
        if (ID <= 0)
        {
            return "";
        }

        string itemName = "";

        string query = "SELECT name FROM items WHERE id = @id LIMIT 1"; // Using LIMIT 1 for efficiency

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", ID);

                using (var reader = command.ExecuteReader())
                {
                    // Check if there is a result
                    if (reader.Read())
                    {
                        itemName = reader.GetString(0);
                    }
                }
            }


        }
        return itemName;
    }

    public string GetIconPathByID(int ID)
    {
        if (ID <= 0)
        {
            return Path.Combine(DirectoryManager.getIconsRootPath(), DefaultIcon);
        }

        string iconPath = "";

        // change icon during fuzzy search. 
        string query = "SELECT Icon FROM items WHERE id = @id LIMIT 1";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", ID);

                using (var reader = command.ExecuteReader())
                {
                    // Check if there is a result
                    if (reader.Read())
                    {
                        iconPath = reader["Icon"].ToString();
                    }
                }
            }


        }

        //if found, adjust to actual path. The last 10 chars should be the actual icon name
        if (!string.IsNullOrWhiteSpace(iconPath) && iconPath.Length >= IconNameLength)
        {
            return Path.Combine(DirectoryManager.getIconsRootPath(), iconPath.Substring(iconPath.Length - 10));
        }

        //else return default icon path (礼物盒
        return Path.Combine(DirectoryManager.getIconsRootPath(), DefaultIcon);
    }

    public string GetDyeNameByID(int DyeID)
    {
        if (DyeID < Dyes.Count)
            return Dyes[DyeID];
        return "Dye ID out of bound";
    }

    public string GetDyeIconByID(int DyeID)
    {
        //not implemented yet
        return Path.Combine(DirectoryManager.getIconsRootPath(), DefaultIcon);
    }
}