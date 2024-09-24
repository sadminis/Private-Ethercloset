// See https://aka.ms/new-console-template for more information
using System;
using GlamourManager;

class Program
{
    static async Task Main(string[] args)
    {
        SQLiteManager sqliteManager = new();
        ApiClient apiClient = new();
        List<FfxivItem> allItems = new();
        allItems = await apiClient.getAllItems();
        Console.WriteLine(allItems[100].Name);
    }
}