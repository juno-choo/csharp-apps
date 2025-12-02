using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace PaymentApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("--- FRAGILE PAYMENT PROCESSOR ---");
            
            // Users: "user_01" has money. "user_02" is broke (Will Fail).
            List<string> users = new List<string> 
            { 
                "user_01", "user_02", "user_03", "user_04", "user_05" 
            };
            
            var processor = new PaymentProcessor();

            List<string> failedUsers = await processor.ProcessBatchAsync(users);

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine($"Failed Transactions: {failedUsers.Count}");
            foreach (var user in failedUsers)
            {
                Console.WriteLine($" - Alert: Payment failed for {user}");
            }
            
            // VERIFICATION
            if (failedUsers.Count == 2 && failedUsers.Contains("user_02") && failedUsers.Contains("user_04"))
            {
                Console.WriteLine("RESULT: [SUCCESS] You caught the specific errors correctly!");
            }
            else
            {
                Console.WriteLine("RESULT: [FAIL] Did not identify the correct failures.");
            }
        }
    }

    class PaymentProcessor
    {
        private readonly MockBankApi _bank = new MockBankApi();

        public async Task<List<string>> ProcessBatchAsync(List<string> userIds)
        {
            Console.WriteLine("Starting Batch Payments...");
            List<string> failedIds = new List<string>();


            var listOfTasks = new List<Task<bool>>();
            foreach (var id in userIds)
            {
                listOfTasks.Add(TryPayAsync(id));              
            }

            bool[] res = await Task.WhenAll(listOfTasks);

            for (int i = 0; i < res.Length; ++i)
            {
                if (res[i] == false)
                {
                    failedIds.Add(userIds[i]);
                }
            }
            return failedIds;
        }
        
        // Helper to catch exceptions
        private async Task<bool> TryPayAsync(string userId) 
        { 
            try 
            {
                await _bank.ProcessPaymentAsync(userId);
                return true;
            }
            // Catch block will trigger if we counter an exception from the bank API
            catch 
            {
                return false;
            }
        }

    }

    // --- MOCK BANK (DO NOT EDIT) ---
    // Simulates a bank that crashes for specific users.
    class MockBankApi
    {
        public async Task ProcessPaymentAsync(string userId)
        {
            await Task.Delay(1000); // Simulate processing time

            // Hardcoded failures for specific users
            if (userId == "user_02" || userId == "user_04")
            {
                throw new InvalidOperationException("Insufficient Funds!");
            }
            
            Console.WriteLine($"   -> Payment Processed for {userId}");
        }
    }
}