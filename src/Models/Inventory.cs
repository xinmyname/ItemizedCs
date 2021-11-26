namespace Itemized.Models;

public class Inventory {

    private readonly IDictionary<Item,int> _slots;

    public Inventory() {
        _slots = new Dictionary<Item,int>();
    }

    public void Add(Item item, int quantity = 1) {
        
        if (_slots.ContainsKey(item)) {
            if (!item.IsUnique) {
                int currentQuantity = _slots[item];
                _slots[item] = currentQuantity + quantity;
            }
        } else {
            _slots[item] = quantity;
        }
    }

    public IEnumerable<(Item,int)> Slots() {
        return _slots.Select(_ => (_.Key, _.Value));
    }
}