namespace Itemize.Models {

    public class Item {

        public Descriptor Descriptor { get; }

        public Item() {
            Descriptor = Descriptor.Default;
        }

        public override string ToString() {
            return "item";
        }
    }
}