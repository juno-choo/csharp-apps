using System;
using System.IO;

namespace DelegateBasicExample
{

    class Program
    {
        delegate void LogDel(string message);

        static void Main(string[] args)
        {
            // Using instance method
            Log log = new Log();

            LogDel LogTextToScreenDel, LogTextToFileDel;
            LogTextToScreenDel = new LogDel(log.LogTextToScreen);
            LogTextToFileDel = new LogDel(log.LogTextToFile);

            LogDel multiLogDel = LogTextToScreenDel + LogTextToFileDel;

            System.Console.WriteLine("Please write your name: ");
            var name = Console.ReadLine();
            LogText(LogTextToFileDel, name);
        }

        static void LogText(LogDel logDel, string message)
        {
            logDel(message);
        }

    }

    public class Log
    {
        public void LogTextToScreen(string message)
        {
            Console.WriteLine($"Date: {DateTime.Now}, Message: {message}");
        }

        public void LogTextToFile(string message)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"), true))
            {
                writer.WriteLine($"Date: {DateTime.Now}, Message: {message}");
            }
        }
    }
    

}