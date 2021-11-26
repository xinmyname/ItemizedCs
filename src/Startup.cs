using Itemized.Infrastructure;
using Itemized.Models;

var argQueue = new Queue<string>(Environment.GetCommandLineArgs().Skip(1));

if (argQueue.Count < 1) {
    Console.WriteLine("Usage: itemize <loot-json-path> {count:1}");
    return;
}

string lootDatabasePath = argQueue.Dequeue();

int count = argQueue.Any()
    ? int.Parse(argQueue.Dequeue())
    : 1;

using (var stream = new FileStream(lootDatabasePath, FileMode.Open)) {

    var db = new LootDatabase(stream);
    var inventory = new Inventory();

    for (int i = 0; i < count; i++)
        inventory.Add(db.MakeItem());
    
    foreach ((Item item, int quantity) in inventory.Slots()) {
        Console.WriteLine(db.Describe(item, quantity));
    }
}

