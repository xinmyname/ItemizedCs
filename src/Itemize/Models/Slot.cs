namespace Itemize.Models
{
    public class Slot
    {
        public int Quantity { get; set; }
        public Item Item { get; set; }

        public Slot(Item item)
        {
            Quantity = 1;
            Item = item;
        }

        public override string ToString()
        {
            string quantityText;

            string text = Quantity == 1 
                ? $"{Item}" 
                : $"{Item}s" ;

            switch (Quantity)
            {
                case 1: quantityText = "An"; break;
                case 0: quantityText = "No"; break;
                case 2: quantityText = "Two"; break;
                case 3: quantityText = "Three"; break;
                case 4: quantityText = "Four"; break;
                case 5: quantityText = "Five"; break;
                case 6: quantityText = "Six"; break;
                case 7: quantityText = "Seven"; break;
                case 8: quantityText = "Eight"; break;
                case 9: quantityText = "Nine"; break;
                default: quantityText = Quantity.ToString(); break;
            }

            return $"{quantityText} {text}";
        }
    }
}