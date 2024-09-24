// See https://aka.ms/new-console-template for more information
using System;
using GlamourManager;

class Program
{
    static async Task Main(string[] args)
    {
        SQLiteManager sqliteManager = new();
        bool result = await sqliteManager.MajorUpdateDatabase();
        Console.WriteLine(result);
    }
}