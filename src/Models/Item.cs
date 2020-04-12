using System;
using System.Collections.Generic;
using Itemize.Infrastructure;

namespace Itemize.Models {

    public class Item {

        public Descriptor Descriptor { get; }

        public Item() {

            var kind = Kind.OneAtRandom();

            this.Descriptor = new Descriptor(new [] { kind.RawValue } );

            if (kind == Kind.Weapon)
                MakeWeapon();
        }

        public override string ToString() {

            DescriptorIterator it = this.Descriptor.Iterator;

            Kind kind = it.NextKind();

            if (kind == Kind.Weapon)
                return DescribeWeapon(it);

            return "item";
        }

        public void MakeWeapon() {
            
            var rnd = new Random();

            int style = rnd.Next(2);

            this.Descriptor.Append(style);
            this.Descriptor.Append(Item.Weights.OneIndexAtRandom(RandomNilStrategy.EqualChance));

            switch (style) {
                case 0:
                    this.Descriptor.Append(Item.Races.OneIndexAtRandom(RandomNilStrategy.EqualChance));
                    break;
                case 1:
                    this.Descriptor.Append(Item.Materials.OneIndexAtRandom(RandomNilStrategy.EqualChance));
                    break;
            }

            this.Descriptor.Append(Item.Weapons.OneIndexAtRandom());
        }

        public string DescribeWeapon(DescriptorIterator iterator) {

            int style = iterator.Next();

            switch (style) {
                case 0: {
                    string weight = iterator.NextOptionalItem(Item.Weights);
                    string race = iterator.NextOptionalItem(Item.Races);
                    string weapon = iterator.NextOptionalItem(Item.Weapons);
                    return $"{weight} {race} {weapon}";
                }

                case 1: {
                    string weight = iterator.NextOptionalItem(Item.Weights);
                    string material = iterator.NextOptionalItem(Item.Materials);
                    string weapon = iterator.NextOptionalItem(Item.Weapons);
                    return $"{weight} {material} {weapon}";
                }
            }

            return "";
        }





        private static string[] Weapons = new string[]{
            "^dagger",
            "^knife",
            "^axe",
            "short ^sword",
            "^broadsword",
            "long ^sword",
            "^katana",
            "^saber",
            "^club",
            "^mace",
            "morning ^star",
            "^flail",
            "^quarterstaff",
            "^polearm",
            "^spear",
            "^bow",
            "^crossbow"
        };

        private static string[] Armors = new string[]{
            "armor ^set",
            "gauntlets",
            "^helm",
            "boots"
        };
        
        private static string[] MonsterParts = new string[]{
            "^hide",
            "^fur",
            "^tusk",
            "^horn",
            "^tooth",
            "^bone"
        };
        
        private static string[] Tools = new string[]{
            "^saw",
            "^axe",
            "scissors",
            "^hammer",
            "^wrench",
            "pliers"
        };
        
        private static string[] Clothes = new string[]{
            "^shirt",
            "trousers",
            "shorts",
            "capris",
            "^skirt",
            "^robe",
            "^hood",
            "^glove",
            "^dress",
            "^jacket",
            "^vest",
            "pajamas",
            "^scarf",
            "^coat",
            "^cap",
            "^cape",
            "^mask",
            "^headband"
        };
        
        private static string[] Utensils = new string[]{
            "^fork",
            "^spoon",
            "^knife"
        };
        
        private static string[] Races = new string[]{
            "Elven",
            "Orcish",
            "Dwarven",
            "Gnomish",
            "Demonic",
            "Undead"
        };
        
        private static string[] Aspects = new string[]{
            "shimmering",
            "sparkling",
            "glittering",
            "incandescent",
            "glowing",
            "dirty",
            "dingy",
            "shabby",
            "faded",
            "bright",
            "flawless",
            "translucent",
            "cloudy"
        };
        
        private static string[] Characteristics = new string[]{
            "healing",
            "pain",
            "agony",
            "hunger",
            "strength",
            "agility",
            "stamina",
            "intellect",
            "teleportation",
            "protection",
            "invisibility",
            "speed",
            "slowness",
            "heaviness",
            "lightness",
            "blundering",
            "clumsiness",
            "dexterity",
            "sleepiness",
            "hate",
            "amore",
            "vigor",
            "itching",
            "accuracy",
            "cowardice",
            "inebriation",
            "sobriety",
            "endurance",
            "persuasion",
            "polymorphism",
            "blindness"
        };
        
        private static string[] Gems = new string[]{
            "ruby",
            "diamond",
            "quartz",
            "emerald",
            "jade",
            "opal",
            "onyx",
            "pearl",
            "sapphire",
            "topaz",
            "turquoise",
            "cubit zirconia"
        };
        
        private static string[] Lengths = new string[]{
            "short",
            "medium",
            "long"
        };
        
        private static string[] Sizes = new string[]{
            "very small",
            "small",
            "medium",
            "large",
            "extra large",
            "extremely large"
        };
        
        private static string[] Weights = new string[]{
            "weightless",
            "very light",
            "light",
            "heavy",
            "very heavy",
            "extremely heavy"
        };
        
        private static string[] Colors = new string[]{
            "white",
            "azure",
            "blue",
            "aquamarine",
            "crimson",
            "red",
            "brown",
            "golden",
            "green",
            "gray",
            "lavendar",
            "pink",
            "indigo",
            "green",
            "cream",
            "eggshell",
            "beige",
            "ecru",
            "turquoise",
            "tan",
            "teal",
            "yellow",
            "purple",
            "magenta",
            "cornflower blue"
        };
        
        private static string[] MetalElements = new string[]{
            "aluminum",
            "titanium",
            "vanadium",
            "chromium",
            "manganese",
            "iron",
            "cobalt",
            "nickel",
            "copper",
            "zinc",
            "gallium",
            "yttrium",
            "zirconium",
            "niobium",
            "molybdenum",
            "ruthenium",
            "rhodium",
            "palladium",
            "silver",
            "cadmium",
            "indium",
            "tin",
            "hafnium",
            "tantalum",
            "tungsten",
            "rhenium",
            "osmium",
            "iridium",
            "platinum",
            "gold",
            "thallium",
            "lead",
            "bismuth",
            "polonium",
            "thorium",
            "uranium",
            "plutonium"
        };
        
        private static string[] Metals = new string[]{
            "aluminum",
            "titanium",
            "iron",
            "cast iron",
            "silver",
            "gold",
            "rose gold",
            "white gold",
            "platinum",
            "copper",
            "steel",
            "brass",
            "nickel",
            "zinc",
            "tungsten",
            "palladium",
            "tin",
            "bronze",
            "pewter",
            "sterling silver"
        };
        
        private static string[] Materials = new string[]{
            "wooden",
            "copper",
            "brass",
            "bronze",
            "silver",
            "gold",
            "quartz",
            "glass",
            "rubber",
            "bone"
        };
        
        private static string[] Topics = new string[]{
            "Philosophy",
            "Metaphysics",
            "Magic",
            "Animals",
            "Plants",
            "Mushrooms",
            "Insects",
            "Machines",
            "Mathematics",
            "Statistics",
            "Logic",
            "Geology",
            "Astronomy",
            "Meteorology",
            "Alchemy",
            "History",
            "The Dead",
            "Business Administration",
            "Law",
            "Medicene",
            "Herbs",
            "Spices",
            "Herbs and Spices",
            "Illustrated Recipes",
            "Art",
            "Architecture",
            "Mystery",
            "Known Felons",
            "Known Time Travelers",
            "Kings",
            "Queens",
            "Gardening",
            "Engineering",
            "Monsters",
            "Wizardry"
        };
        
        private static string[] Mails = new string[]{
            "fur",
            "leather",
            "bone",
            "scale mail",
            "plate mail",
            "chain mail",
            "banded mail"
        };
            
        private static string[] Enchantments = new string[]{
            "blessed",
            "cursed"
        };

        private static string[] Masses = new string[]{
            "^milligram",
            "^gram",
            "^kilogram"
        };

        public class Kind {
            
            public static Kind Weapon = Next();
//            public static Kind Armor = Next();
//            public static Kind MonsterPart = Next();

            public static IDictionary<int, Kind> AllKinds { get; private set;}

            public static Kind OneAtRandom() {
                var rnd = new Random();
                int rawValue = rnd.Next(AllKinds.Values.Count);
                return AllKinds.Values.Get(rawValue);
            }

            public int RawValue { get; private set;}

            public static Kind From(int rawValue) {
                return AllKinds.Values.Get(rawValue);
            }

            private static int NextRawValue = 0;

            private static Kind Next() {
                int rawValue = NextRawValue++;
                var kind = new Kind { RawValue = rawValue };
                if (AllKinds == null)
                    AllKinds = new Dictionary<int, Kind>();
                AllKinds.Add(rawValue, kind);
                return kind;
            }
        }
    }
}