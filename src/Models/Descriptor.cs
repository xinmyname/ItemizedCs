using System;
using System.Collections.Generic;
using System.Linq;

namespace Itemize.Models {

    public class Descriptor {

        private IList<int> _contents;

        public Descriptor() {
            _contents = new List<int>();
        }        

        public Descriptor(string that) {
            _contents = new List<int>(that.Split("-").Select(x => x == "?" ? -1 : int.Parse(x)));
        }

        public Descriptor(IEnumerable<int> that) {
            _contents = new List<int>(that);
        }

        public override string ToString() {
            return String.Join('-', _contents.Select(x => x < 0 ? "?" : x.ToString()));
        }

        public void Append(int? index) {
            if (index == null)
                _contents.Add(-1);
            else
                _contents.Add(index.Value);
        }

        public int Count => _contents.Count;

        public int? this[int pos]
        {
            get {
                int index = _contents[pos];
                if (index < 0)
                    return null;
                return index;
            }
            set {
                if (value == null)
                    _contents[pos] = -1;

                _contents[pos] = value.Value;
            }
        }

        public DescriptorIterator Iterator {
            get { return new DescriptorIterator(this); }
        }

        public override int GetHashCode() {
            int result = 0;

            foreach (int index in _contents)
                result = (result * 397) ^ index;

            return result;
        }

        public override bool Equals(object obj) {
            var that = obj as Descriptor;

            if (that == null)
                return false;

            if (ReferenceEquals(this, that))
                return true;
            
            if (this.Count != that.Count)
                return false;

            for (int p = 0; p < _contents.Count; p++) {
                if (this[p] != that[p])
                    return false;
            }
            
            return true;
        }
    }

    public class DescriptorIterator {

        public class ReferenceLostException : ApplicationException {}
        public class NoMoreItemsException : ApplicationException {}

        private readonly WeakReference<Descriptor> _descriptor;
        private int _position = 0;

        public DescriptorIterator(Descriptor descriptor) {
            _descriptor = new WeakReference<Descriptor>(descriptor);
        }

        public int? Next() {
            Descriptor descriptor;

            if (!_descriptor.TryGetTarget(out descriptor))
                throw new ReferenceLostException();

            if (_position >= descriptor.Count)
                throw new NoMoreItemsException();

            int? index = descriptor[_position];

            _position += 1;

            return index;
        }
    }
}

