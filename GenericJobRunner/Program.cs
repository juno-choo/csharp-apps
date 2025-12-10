using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fileNames = new List<string> { "report.pdf", "image.png", "data.csv", "backup.zip" };
            
            // 1. Initialize the generic runner
            JobRunner<string> runner = new JobRunner<string>(fileNames);

            // 2. Subscribe to the event (Using Lambda or Method)
            // TODO: Hook up runner.JobCompleted here...
            runner.JobCompleted += (fileName) =>
            {
                Console.WriteLine($"Job completed for {fileName}!");
            };

            Console.WriteLine("--- Starting Batch Processing ---");

            // 3. Run the async batch
            await runner.RunBatchAsync();

            Console.WriteLine("--- All Jobs Finished ---");
        }
    }

    public class JobRunner<T>
    {
        private List<T> _jobs;

        // TODO: Define the 'JobCompleted' event here
        // Hint: public event Action<T> JobCompleted; 
        public event Action<T> JobCompleted;
        
        public JobRunner(List<T> jobs)
        {
            _jobs = jobs;
        }

        public async Task RunBatchAsync()
        {
            List<Task> tasks = new List<Task>();

            foreach (var job in _jobs)
            {
                // TODO: Add a task to the list that calls ProcessSingleJobAsync(job)
                tasks.Add(ProcessSingleJobAsync(job));
            }

            // TODO: Await all tasks to ensure we don't exit early
            await Task.WhenAll(tasks);
        }

        private async Task ProcessSingleJobAsync(T job)
        {
            // Simulate work
            await Task.Delay(1000); // 1 second delay

            // TODO: Raise the JobCompleted event here
            JobCompleted?.Invoke(job);
        }
    }
}