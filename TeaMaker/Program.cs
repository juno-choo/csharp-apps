using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        await MakeTeaAsync();
    }

    static async Task<string> MakeTeaAsync()
    {
        Console.WriteLine("Starting to make tea...");
        var boilingTask = BoilWaterAsync();
        var boiledWater = await boilingTask;
        var tea = $"Pour {boiledWater} into cup with tea bag. Tea is ready.";
        Console.WriteLine(tea);
        return tea;
    }

    static string MakeTea()
    {
        var boiledWater = BoilWater();

        Console.WriteLine("take cup out");
        Console.WriteLine("put tea bags in cup");
        Console.WriteLine("pour boiled water into cup");
        
        return "Tea is ready";
    }

    static async Task<string> BoilWaterAsync()
    {
        Console.WriteLine("Starting to boil water...");
        await Task.Delay(3000); // Simulate time taken to boil water
        Console.WriteLine("Water boiling completed.");
        return "boiled water";
    }

    static string BoilWater()
    {
        Console.WriteLine("fill kettle with water");
        Console.WriteLine("put kettle on stove");
        Console.WriteLine("turn on stove");
        
        // This pauses execution for 3 seconds to simulate boiling
        Task.Delay(3000).GetAwaiter().GetResult();
        
        Console.WriteLine("water boiled");
        return "boiled water";
    }
}