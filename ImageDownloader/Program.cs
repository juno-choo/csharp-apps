using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq; // Hint: Might be useful for Sum()
using System.Threading.Tasks;

namespace BatchProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("--- BULK IMAGE DOWNLOADER ---");
            
            // The input list
            List<string> imageIds = new List<string> { "img_01", "img_02", "img_03", "img_04", "img_05" };
            
            var processor = new ImageProcessor();
            var stopwatch = Stopwatch.StartNew();

            // ---------------------------------------------------------
            // CHANGE THIS LINE TO CALL YOUR NEW METHOD
            // ---------------------------------------------------------
            // int totalBytes = await processor.DownloadSequentiallyAsync(imageIds);
            int totalBytes = await processor.DownloadConcurrentlyAsync(imageIds);

            stopwatch.Stop();
            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine($"Total Bytes Downloaded: {totalBytes}");
            Console.WriteLine($"Time Taken: {stopwatch.Elapsed.TotalSeconds:F2} seconds");

            if (stopwatch.Elapsed.TotalSeconds < 2.5 && totalBytes == 2500) 
            {
                Console.WriteLine("RESULT: [SUCCESS] Fast and Accurate!");
            }
            else
            {
                Console.WriteLine("RESULT: [FAIL] Too slow or wrong sum.");
            }
        }
    }

    class ImageProcessor
    {
        private readonly MockNetwork _network = new MockNetwork();

        // --- THE SLOW WAY (Reference) ---
        public async Task<int> DownloadSequentiallyAsync(List<string> imageIds)
        {
            int totalSize = 0;
            foreach (var id in imageIds)
            {
                Console.WriteLine($"Processing {id}...");
                // This awaits immediately, pausing the loop every time.
                int size = await _network.DownloadImageAsync(id); 
                totalSize += size;
            }
            return totalSize;
        }

        // --- YOUR TURN: FINISH THIS METHOD ---
        public async Task<int> DownloadConcurrentlyAsync(List<string> imageIds)
        {
            Console.WriteLine("Starting Batch Download...");

            // 1. Create a List to hold your "Receipts" (Tasks)
            List<Task<int>> downloadTasks = new List<Task<int>>();

            // 2. Loop through the imageIds. 
            //    Inside the loop, call _network.DownloadImageAsync(id) 
            //    BUT DO NOT AWAIT IT. Add the task to your list.
            foreach (var id in imageIds) 
            {
                var task = _network.DownloadImageAsync(id);
                downloadTasks.Add(task);
            }

            // 3. Wait for all tasks to finish using Task.WhenAll
            int[] res = await Task.WhenAll(downloadTasks);

            // 4. Calculate the sum of the results (Total Bytes)
            return res.Sum();
        }
    }

    // --- MOCK NETWORK (DO NOT EDIT) ---
    class MockNetwork
    {
        public async Task<int> DownloadImageAsync(string id)
        {
            await Task.Delay(2000); // Takes 2 seconds per image
            return 500; // Each image is 500 bytes
        }
    }
}