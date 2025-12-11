using System;
using System.Collections.Generic;

namespace QuestChallenge
{
    // 1. Define the Delegate (Must return bool!)
    // TODO: public delegate ... QuestCondition ...
    public delegate bool QuestCondition(Player player);

    class Program
    {
        static void Main(string[] args)
        {
            Player p = new Player { Gold = 0, Level = 1, Location = "Town" };

            // 2. Create a Quest with a Lambda for the condition
            // Condition: Return true ONLY if p.Gold >= 10
            var goldQuest = new Quest("Gather 10 Gold", (player) => 
            {
                // TODO: Return the boolean check
                return (player.Gold >= 10);
            });

            Console.WriteLine($"--- Started Quest: {goldQuest.Title} ---");

            // Simulation Loop
            while (!goldQuest.CheckIsComplete(p))
            {
                Console.WriteLine($"Current Gold: {p.Gold}. working...");
                p.Gold += 2; // Earn gold
                System.Threading.Thread.Sleep(500);
            }

            Console.WriteLine("🎉 Quest Completed!");
        }
    }

    public class Player
    {
        public int Gold { get; set; }
        public int Level { get; set; }
        public string Location { get; set; }
    }

    // 3. The Interface
    public interface IQuest
    {
        string Title { get; }
        bool CheckIsComplete(Player p);
    }

    // 4. The Class
    public class Quest : IQuest
    {
        public string Title { get; private set; }
        public QuestCondition Condition { get; set; }
        // TODO: Property to hold the delegate

        
        public Quest(string title, QuestCondition condition)
        {
            Title = title;
            // TODO: Assign delegate
            Condition = condition;
        }

        public bool CheckIsComplete(Player p)
        {
            // TODO: Invoke the delegate and return its result!
            return (Condition(p));
        }
    }
}