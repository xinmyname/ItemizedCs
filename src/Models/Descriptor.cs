using System;

namespace Itemize.Models {

    public class Descriptor {
        
        private static readonly Lazy<Descriptor> LazyDefault =
            new Lazy<Descriptor>(() => new Descriptor());

        public static Descriptor Default => LazyDefault.Value;
    }
}