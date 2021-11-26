namespace Itemized.Infrastructure;

public class Table {
 
    public string Name { get; private set; }
    
    private readonly IDictionary<int,string> _values;

    public Table(string name, IEnumerable<string?> values) {
        
        Name = name;

        _values = new Dictionary<int,string>();

        int nextId = 1;

        foreach (string? value in values) {
            
            if (value == null)
                continue;

            _values[nextId] = value;
            nextId += 1;           
        }
    }

    public string ValueFor(int id) {
        return _values[id];
    }

    public int RandomId(bool optional = false) {

        int index = optional
            ? Random.Shared.Next(_values.Keys.Count+1)
            : Random.Shared.Next(_values.Keys.Count);

        if (index == _values.Keys.Count)
            return 0;

        return _values.Keys.ElementAt(index);
    }
}