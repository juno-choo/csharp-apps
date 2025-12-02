using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LegacyApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("--- E-COMMERCE ORDER VALIDATOR (LEGACY) ---");
            
            var validator = new OrderValidator();
            var stopwatch = Stopwatch.StartNew();

            // SCRIPT: User places an order
            // We expect this to return 'true' but it needs to be FASTER.
            bool isSuccess = await validator.ValidateOrderAsync("Order#123");

            stopwatch.Stop();
            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine($"Order Valid: {isSuccess}");
            Console.WriteLine($"Total Validation Time: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
            
            // CONSTRAINTS CHECK
            if (stopwatch.Elapsed.TotalSeconds < 3.5)
            {
                Console.WriteLine("RESULT: [SUCCESS] System is optimized! Good job.");
            }
            else
            {
                Console.WriteLine("RESULT: [FAIL] System is too slow (> 3.5s). Optimization required.");
            }
        }
    }

    // -----------------------------------------------------------
    // CHALLENGE: OPTIMIZE THIS CLASS
    // -----------------------------------------------------------
    class OrderValidator
    {
        private readonly Database _db = new Database();

        public async Task<bool> ValidateOrderAsync(string orderId)
        {
            Console.WriteLine($"[{orderId}] Starting validation sequence...");

            // --- LEGACY CODE BLOCK (Refactor this section) ---
            // Current Behavior: Sequential (3s + 2s + 1s = 6s total)
            // Target Behavior: Parallel (Max(3,2,1) = 3s total)

            // 1. Check Fraud
            bool fraudCheck = _db.CheckFraudAsync(orderId);
            if (!fraudCheck) return false;

            // 2. Check Inventory
            bool inventoryCheck = await _db.CheckInventoryAsync(orderId);
            if (!inventoryCheck) return false;

            // 3. Check Address
            bool addressCheck = await _db.VerifyAddressAsync(orderId);
            if (!addressCheck) return false;

            await Task.WhenAll(fraudCheck, inventoryCheck, addressCheck);

            var fraud = await fraudCheck;
            var inventory = await inventoryCheck;
            var address = await addressCheck;

            // ------------------------------------------

            return true; // Only returns true if all above passed
        }
    }

    // --- MOCK DATABASE (DO NOT EDIT) ---
    // These methods simulate slow external API calls.
    class Database
    {
        public async Task<bool> CheckFraudAsync(string id)
        {
            Console.WriteLine("   -> Connecting to FBI Database...");
            await Task.Delay(3000); // 3s delay
            Console.WriteLine("   <- Fraud Check Complete: OK");
            return true;
        }

        public async Task<bool> CheckInventoryAsync(string id)
        {
            Console.WriteLine("   -> Checking Warehouse shelves...");
            await Task.Delay(2000); // 2s delay
            Console.WriteLine("   <- Inventory Check Complete: OK");
            return true;
        }

        public async Task<bool> VerifyAddressAsync(string id)
        {
            Console.WriteLine("   -> Pinging Maps API...");
            await Task.Delay(1000); // 1s delay
            Console.WriteLine("   <- Address Verify Complete: OK");
            return true;
        }
    }
}