namespace Itemized.Models;

public class ItemProperty {

    public string Name { get; private set; }
    public bool Optional { get; private set; }

    public ItemProperty(string propertyReference) {
        
        Optional = propertyReference.EndsWith('?');

        Name = Optional
            ? propertyReference.Substring(0, propertyReference.Length-1)
            : propertyReference;
    }
}