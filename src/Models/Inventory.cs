using System.Collections.Generic;

namespace Itemize.Models
{
    public class Inventory
    {
        private readonly Dictionary<Descriptor, Slot> _slots;

        public IEnumerable<Slot> Slots => _slots.Values;

        public Inventory()
        {
            _slots = new Dictionary<Descriptor, Slot>();
        }

        public void AddItem(Item item)
        {
            if (_slots.ContainsKey(item.Descriptor))
            {
                Slot slot = _slots[item.Descriptor];
                slot.Quantity += 1;
            }
            else
                _slots[item.Descriptor] = new Slot(item);
        }
    }
}