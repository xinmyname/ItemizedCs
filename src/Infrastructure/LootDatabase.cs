using Itemized.Models;
using System.Text;
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

        string rawDescription = RawDescription(item);
        string description = Pluralize(rawDescription, quantity).Trim();

        string quantityText;
        
        if (quantity == 1) {
            quantityText = "aeiouAEIOU".Contains(description[0])
                ? "An"
                : "A";
        } else {
            quantityText = quantity switch {
                2 => "Two",
                3 => "Three",
                4 => "Four",
                5 => "Five",
                6 => "Six",
                7 => "Seven",
                8 => "Eight",
                9 => "Nine",
                _ => $"{quantity}"
            };
        }

        return $"{quantityText} {description} [{item}]";
    }

    public string Pluralize(string text, int quantity) {

        if (quantity == 1)
            return text.Replace("^", "");

        var pluralText = new StringBuilder();

        for (int i = 0; i < text.Length; i++) {

            char ch = text[i];

            if (ch == '^') {
                int start = i+1;
                int end = start;

                while (end < text.Length && !Char.IsWhiteSpace(text[end]))
                    end += 1;

                i = end-1;

                string item = text.Substring(start,end-start);

                pluralText.Append(item.Pluralize());

            } else
                pluralText.Append(ch);
        }

        return pluralText.ToString();
    }

    public string RawDescription(Item item) {

        var indices = new Queue<int>(item);

        bool isUnique = indices.Dequeue() == 1;

        if (isUnique)
            return _uniqueItemTable.ValueFor(indices.Dequeue());
        
        var description = new StringBuilder();
        string itemTemplate = _commonItemTable.ValueFor(indices.Dequeue());

        for (int i = 0; i < itemTemplate.Length; i++) {
            
            int start = i;
            char ch = itemTemplate[i];

            if (ch == '{') {

                int end = itemTemplate.IndexOf('}', start);
                var itemProperty = new ItemProperty(itemTemplate.Substring(start+1,end-start-1));

                i = end;

                int index = indices.Dequeue();

                if (index == 0)
                    continue;

                Table table = _propertyTables[itemProperty.Name];

                description.Append(table.ValueFor(index));

            } else {
                description.Append(ch);
            }            
        }

        description.Replace("  ", " ");

        return description.ToString();
    }

    private IEnumerable<ItemProperty> GetPropertiesFromItemTemplate(string itemTemplate) {

        for (int start = itemTemplate.IndexOf('{'); start != -1; start = itemTemplate.IndexOf('{', start+1)) {
            int end = itemTemplate.IndexOf('}', start);
            yield return new ItemProperty(itemTemplate.Substring(start+1,end-start-1));
        }

    }
}