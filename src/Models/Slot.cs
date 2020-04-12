using Itemize.Infrastructure;

namespace Itemize.Models {

    public class Slot {

        public int Quantity { get; set; }
        public Item Item { get; set; }

        public Slot(Item item) {
            Quantity = 1;
            Item = item;
        }

        public override string ToString() {

            if (this.Item == null)
                return "Nothing";

            string text = Item.ToString();

            // Replace optional text
            text = text.ClearOptionals();

            // Check for plural indicator
            if (text.Contains("^")) {

                // Check for just one item
                if (this.Quantity == 1) {

                    text = text.Replace("^", "");

                    return text.StartsWithAny(Slot.VowelPrefixes)
                        ? $"An {text}"
                        : $"A {text}";
                }

                // Handle more than one item
                text = text.Pluralized(this.Quantity);

                string quantityText = this.Quantity switch {
                    2 => "Two",
                    3 => "Three",
                    4 => "Four",
                    5 => "Five",
                    6 => "Six",
                    7 => "Seven",
                    8 => "Eight",
                    9 => "Nine",
                    _ => this.Quantity.ToString()
                };

                return $"{quantityText} {text}";
            } 
            
            // Else, capitalize first letter and return
            return text.Capitalized();
        }

        private static string[] VowelPrefixes = new string[]{
            "alum",
            "aqua",
            "azur",
            "ecru",
            "eggs",
            "Elve",
            "emer",
            "endu",
            "extr",
            "inca",
            "indi",
            "irid",
            "iron",
            "onyx",
            "opal",
            "Orci",
            "osmi",
            "Unde"
        };
    }
}