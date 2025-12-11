using System;
using System.Collections.Generic;

namespace GameChallenge
{
    // 1. Define the Delegate
    // TODO: public delegate ... AttackStrategy ..
    public delegate void AttackStrategy(string targetName);

    class Program
    {
        static void Main(string[] args)
        {
            var inventory = new List<IWeapon>();

            // 2. Create weapons using Lambdas for the logic
            // TODO: Instantiate a ModularWeapon ("Iron Sword") with a lambda that prints "Slashes..."
            var sword = new ModularWeapon("Iron Sword", (t) => Console.WriteLine($"Slashes {t}"));
            // TODO: Instantiate a ModularWeapon ("Magic Staff") with a lambda that prints "Casts fireball..."
            var staff = new ModularWeapon("Magic Staff", (t) => Console.WriteLine($"Casts fireball at {t}"));

            inventory.Add(sword);
            inventory.Add(staff);

            Console.WriteLine("--- Battle Start ---");

            foreach (var weapon in inventory)
            {
                weapon.Attack("the Orc");
            }
        }
    }

    // 3. Define the Interface
    public interface IWeapon
    {
        // TODO: void Attack(string target);
        void Attack(string target);
    }

    // 4. Implement the Class
    public class ModularWeapon : IWeapon
    {
        public string Name { get; set; }
        public AttackStrategy Target { get; set; }

        public ModularWeapon(string name, AttackStrategy target)
        {
            Name = name;
            // TODO: Assign the delegate
            Target = target;
        }

        public void Attack(string target)
        {
            // TODO: Invoke the delegate here!
            Console.WriteLine($"{Name} is preparing...");
            Target(target);
        }
    }
}