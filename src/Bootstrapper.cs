using System;
using System.Collections.Generic;
using Itemize.Infrastructure;
using Itemize.Models;

namespace Itemize {

    internal class Bootstrapper {

        private static int Main(string[] args) {

            int numItems = 1;

            var argQueue = new Queue<string>(args);

            while (argQueue.Count > 0) {

                string arg = argQueue.Dequeue();

                switch (arg) {
                    case "/?":
                    case "/h":
                    case "-?":
                    case "-h":
                    case "--help":
                        return ShowHelp();
                    default:
                        numItems = Int32.Parse(arg);
                        break;
                }
            }

            var inventory = new Inventory();
            var itemFactory = new ItemFactory();

            for (int i = 0; i < numItems; i++)
                inventory.AddItem(itemFactory.MakeItem());

            Console.WriteLine("You have:");
            Console.WriteLine();

            foreach (Slot slot in inventory.Slots)
                Console.WriteLine($"  {slot}");

            return 0;
        }

        private static int ShowHelp() {
            
            Console.WriteLine("Usage: itemize [count]");
            Console.WriteLine();
            Console.WriteLine("  count   number of items to add");
            return 1;
        }
    }
}
