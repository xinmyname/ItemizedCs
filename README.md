# ItemizedCs

This is the C# version of Itemized. The program adds items to inventory and then prints out how many items you have. You can specify how many items to add by passing a number as an argument. If you don't specify any items, you will receive one item. 

Yes, it's very simple. That's the idea.

## Example

```
$> dotnet run 
You have:
  An item
$> dotnet run 4
You have:
  Four items
$> dotnet run 42
You have: 
  42 items
```

## Prerequisites
- .NET Core 2.0 Preview 1

## Notes

- Builds from CLI, Visual Studio Code or Visual Studio 2017.
- Add RuntimeIdentifier to .csproj file if you want an executable

## The Good Parts
- Easy to modularlize
- Feature rich (tuples, clousures, generics, etc.)
- Large standard library

## Airing of Grievances
- Difficult to create a native executable
- Very large deployment size, due to large standard library
- Slow startup time
