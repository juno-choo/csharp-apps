using System;
using System.IO;

namespace DelegateBasicExample
{

    class Program
    {
        delegate void LogDel(string message);

        static void Main(string[] args)
        {
            LogDel logDelegate = new LogDel(LogTextToFile);

            System.Console.WriteLine("Please write your name: ");
            var name = Console.ReadLine();
            logDelegate(name);
        }

        static void LogTextToScreen(string message)
        {
            Console.WriteLine($"Date: {DateTime.Now}, Message: {message}");
        }
        
        static void LogTextToFile(string message)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"), true))
            {
                writer.WriteLine($"Date: {DateTime.Now}, Message: {message}");
            }
        }
    }
}