using System.Collections;

namespace Itemized.Models;

public class Item : IEnumerable<int> {

    private int[] _contents;

    public Item(IEnumerable<int> contents) {
        _contents = contents.ToArray();
    }

    public Item(string contents) {
        _contents = contents.Split(':').Select(_ => int.Parse(_)).ToArray();
    }

    public int this[int index] {
        get {
            if (index >= _contents.Length)
                return -1;

            return _contents[index];
        }
    }

    public bool IsUnique {
        get { return _contents.First() != 0; }
    }

    public override bool Equals(object? obj) {

        if (obj is not Item other)
            return false;

        return Equals(other);
    }

    public bool Equals(Item other) {

        if (other == null)
            return false;

        if (_contents.Length != other._contents.Length)
            return false;

        for (int i = 0; i < _contents.Length; i++) {
            if (_contents[i] != other[i])
                return false;
        }

        return true;
    }

    public override int GetHashCode() {
        int hash = 17;
        foreach (int index in _contents)
            hash = hash * 19 + index;
        return hash;
    }

    public override string? ToString() {
        return string.Join(':', _contents);
    }

    public IEnumerator<int> GetEnumerator() {
        foreach (int index in _contents)
            yield return index;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return this.GetEnumerator();
    }
}
