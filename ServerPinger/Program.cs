using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticeApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ips = new List<string> { "192.168.1.1", "8.8.8.8", "127.0.0.1", "10.0.0.5" };

            // 1. Setup the tester
            PingTester<string> pinger = new PingTester<string>(ips);

            // 2. Subscribe to the event
            // Twist: Your lambda needs two parameters now!
            // pinger.PingCompleted += (server, status) => ...
            pinger.PingCompleted += (server, status) =>
            {
                Console.WriteLine($"{status}: {server}");
            };

            Console.WriteLine("--- Pinging Servers ---");

            // 3. Run
            await pinger.RunPingsAsync();

            Console.WriteLine("--- Done ---");
        }
    }

    public class PingTester<T>
    {
        private List<T> _servers;
        private Random _rnd = new Random();

        // TODO: Define the event 'PingCompleted' (Action<T, string>)
        public event Action<T, string> PingCompleted;

        public PingTester(List<T> servers)
        {
            _servers = servers;
        }

        public async Task RunPingsAsync()
        {
            // TODO: Create the list of tasks, loop, and Task.WhenAll
            var tasks = new List<Task>();

            foreach (var server in _servers)
            {
                tasks.Add(TestSingleServerAsync(server));
            }

            await Task.WhenAll(tasks);
        }

        private async Task TestSingleServerAsync(T server)
        {
            // Simulate variable network lag
            int delay = _rnd.Next(100, 2000); 
            await Task.Delay(delay);

            // TODO: Invoke the event with BOTH 'server' and "Success"
            PingCompleted?.Invoke(server, "Success");
        }
    }
}