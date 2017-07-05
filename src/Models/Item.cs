namespace Itemize.Models {

    public class Item {

        public Descriptor Descriptor { get; }

        public Item() {
            Descriptor = new Descriptor();
        }

        public override string ToString() {
            return "item";
        }
    }
}