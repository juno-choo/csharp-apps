/*
Context: You are building the backend for a smart home dashboard. When the user wakes up, the screen needs to display Weather, Stock Prices, and Top News immediately.

Your Goal: Write a complete C# Console Application from scratch that fetches these three pieces of data simultaneously and displays a final report.

The Constraints & Parameters
1. The "Fake" API Methods You must implement these three static async methods. Use Task.Delay to simulate network lag.

FetchWeatherAsync:

Simulated Delay: 2000ms (2 seconds)

Returns: A string (e.g., "Sunny, 25°C")

FetchStockPriceAsync:

Simulated Delay: 1000ms (1 second)

Returns: A double (e.g., 240.55)

FetchNewsAsync:

Simulated Delay: 3000ms (3 seconds)

Returns: An array or list of strings string[] (e.g., {"Tech is booming", "AI takes over"})

2. The Main Execution

Start a Stopwatch to track time.

Kick off all three tasks at the same time.

Wait for all of them to complete.

Capture the results (Store the returned weather string, the stock price double, and the news array).

Print a final report similar to this:
Dashboard Loaded! (Total Time: 3.01s)
-------------------------------------
Weather: Sunny, 25°C
Stock: $240.55
News:
 - Tech is booming
 - AI takes over

 The Rules
The total time printed must be close to 3 seconds (the length of the longest task), not 6 seconds.
You must handle the correct return types (e.g., Task<double>).
*/
using System;
using System.Threading.Tasks;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting dashboard load...");
        var stopwatch = Stopwatch.StartNew();
        var weatherTask = FetchWeatherAsync();
        var stockTask = FetchStockPriceAsync();
        var newsTask = FetchNewsAsync();
        // await Task.WhenAll(weatherTask, stockTask, newsTask);
        var weather = await weatherTask;
        var stock = await stockTask;
        var news = await newsTask;
        stopwatch.Stop();
        Console.WriteLine($"Dashboard Loaded! (Total Time: {stopwatch.Elapsed.TotalSeconds:F2}s)");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"Weather: {weather}");
        Console.WriteLine($"Stock: ${stock:F2}");
        Console.WriteLine("News:");
        foreach (var headline in news)
        {            Console.WriteLine($" - {headline}");
        }
    }

    static async Task<string> FetchWeatherAsync()
    {
    await Task.Delay(2000); // Simulate network delay
    return "Sunny, 25°C";
    }

    static async Task<double> FetchStockPriceAsync()
    {
        await Task.Delay(1000); // Simulate network delay
        return 240.55;
    }

    static async Task<string[]> FetchNewsAsync()
    {
        await Task.Delay(3000); // Simulate network delay
        return new string[] { "Tech is booming", "AI takes over" };
    }
}

