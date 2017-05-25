using Itemize.Models;

namespace Itemize.Infrastructure
{
    public interface IMakeItems
    {
        Item MakeItem();
    }

    public class ItemFactory : IMakeItems
    {
        public Item MakeItem()
        {
            return new Item();
        }
    }
}