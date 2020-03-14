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

    public enum Kind {
        Weapon
    }

    public static class KindExtensions {

        public static string GetDescription(this Kind self) {
            switch (self) {
                case Kind.Weapon: return "weapon";
            }

            return string.Empty;
        }

        public static int GetCount(this Kind self) {
            return System.Enum.GetValues(self.GetType()).Length;
        }

//        public static Kind OneAtRandom(this Kind self) {

//        }
    }
}