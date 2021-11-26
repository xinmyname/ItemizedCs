using Itemized.Models;
using System.Text.Json;

namespace Itemized.Infrastructure;

public class LootDatabase {

    private readonly IDictionary<string,Table> _propertyTables;
    private readonly Table _commonItemTable;
    private readonly Table _uniqueItemTable;

    public LootDatabase(Stream stream) {

        _propertyTables = new Dictionary<string,Table>();

        var doc = JsonDocument.Parse(stream);

        JsonElement propertiesElement = doc.RootElement.GetProperty("properties");

        foreach (JsonProperty objectProperty in propertiesElement.EnumerateObject()) {
            string tableName = objectProperty.Name;
            _propertyTables[tableName] = new Table(tableName, objectProperty.Value.EnumerateArray().Select(_ => _.GetString()));
        }

        JsonElement commonElement = doc.RootElement.GetProperty("common");
        _commonItemTable = new Table("common", commonElement.EnumerateArray().Select(_ => _.GetString()));

        JsonElement uniqueElement = doc.RootElement.GetProperty("unique");
        _uniqueItemTable = new Table("unique", uniqueElement.EnumerateArray().Select(_ => _.GetString()));
    }

    public Item MakeItem() {

        var contents = new List<int>();

        if (Random.Shared.Next(1000) == 999) {
            contents.Add(1);
            contents.Add(_uniqueItemTable.RandomId());
        } else {

            contents.Add(0);

            int id = _commonItemTable.RandomId();
            contents.Add(id);

            foreach (ItemProperty itemProperty in GetPropertiesFromItemTemplate(_commonItemTable.ValueFor(id))) {
                Table table = _propertyTables[itemProperty.Name];
                contents.Add(table.RandomId(itemProperty.Optional));
            }
        }

        return new Item(contents);
    }

    public string Describe(Item item, int quantity) {



        return $"{quantity}x {item}";
    }

    private IEnumerable<ItemProperty> GetPropertiesFromItemTemplate(string itemTemplate) {

        for (int start = itemTemplate.IndexOf('{'); start != -1; start = itemTemplate.IndexOf('{', start+1)) {
            int end = itemTemplate.IndexOf('}', start);
            yield return new ItemProperty(itemTemplate.Substring(start+1,end-start-1));
        }

    }
}