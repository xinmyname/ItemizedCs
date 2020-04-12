using System;
using System.Collections.Generic;

namespace Itemize.Infrastructure
{
    public enum RandomNilStrategy {
        Never,
        EqualChance
    }

    public static class CollectionExtensions {

        public static T Get<T>(this ICollection<T> self, int index) {

            var enumerator = self.GetEnumerator();

            while (enumerator.MoveNext() && index > -1) {

                if (index == 0)
                    return enumerator.Current;

                index--;
            }

            return default(T);
        }

        public static T OneAtRandom<T>(this ICollection<T> self, RandomNilStrategy strategy = RandomNilStrategy.Never) {

            int? index = self.OneIndexAtRandom(strategy);

            if (index.HasValue)
                return self.Get(index.Value);
            
            return default(T);
        }

        public static int? OneIndexAtRandom<T>(this ICollection<T> self, RandomNilStrategy strategy = RandomNilStrategy.Never) {

            var rnd = new Random();

            switch (strategy) {
                
                case RandomNilStrategy.Never:
                    return rnd.Next(self.Count);

                case RandomNilStrategy.EqualChance:
                    int idx = rnd.Next(self.Count+1);                    
                    if (idx < self.Count)
                        return rnd.Next(self.Count);
                    break;
            }

            return null;
        }
    }
}
