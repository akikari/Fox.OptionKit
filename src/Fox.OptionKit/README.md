# Fox.OptionKit

A minimalist, dependency-free `Option<T>` implementation for C#. Fox.OptionKit is a lightweight, clean, and well-documented solution for type-safe handling of optional values.

## Key Features

- ✅ **Dependency-free** - No external dependencies
- ✅ **Lightweight** - Minimal overhead
- ✅ **Type-safe** - Type-safe alternative to null values
- ✅ **Well-documented** - Comprehensive XML documentation
- ✅ **Functional** - Map, Bind, Match operations
- ✅ **.NET 8+ compatible** - Modern .NET support

## Installation

```bash
dotnet add package Fox.OptionKit
```

## Basic Usage

### Some and None

```csharp
using Fox.OptionKit;

// Create an option with a value
var some = Option.Some(42);
Console.WriteLine(some.HasValue); // True
Console.WriteLine(some.Value);    // 42

// Create an empty option
var none = Option.None<int>();
Console.WriteLine(none.IsNone);   // True
```

### ValueOr - Safe Value Access

```csharp
var value1 = Option.Some(42).ValueOr(0);    // 42
var value2 = Option.None<int>().ValueOr(0); // 0
```

### Map - Value Transformation

```csharp
var result = Option.Some(10)
    .Map(x => x * 2)
    .Map(x => x + 5)
    .Map(x => $"Result: {x}");

Console.WriteLine(result); // Some(Result: 25)
```

### Bind - Monadic Composition

```csharp
Option<int> Divide(int numerator, int denominator)
{
    return denominator == 0 
        ? Option.None<int>() 
        : Option.Some(numerator / denominator);
}

var result = Option.Some(100)
    .Bind(x => Divide(x, 2))
    .Bind(x => Divide(x, 5));

Console.WriteLine(result); // Some(10)
```

### Match - Pattern Matching

```csharp
var message = Option.Some(42).Match(
    some: value => $"Found: {value}",
    none: () => "Not found");

Console.WriteLine(message); // "Found: 42"
```

### ToOption - Convert from Nullable

```csharp
string? nullableString = "Hello";
var option = nullableString.ToOption();

int? nullableInt = null;
var noneOption = nullableInt.ToOption();
```

## Real-World Example

```csharp
var users = new Dictionary<int, string>
{
    { 1, "Alice" },
    { 2, "Bob" }
};

Option<string> GetUser(int id)
{
    return users.TryGetValue(id, out var name) 
        ? Option.Some(name) 
        : Option.None<string>();
}

var userName = GetUser(1)
    .Map(name => name.ToUpper())
    .ValueOr("UNKNOWN");

Console.WriteLine(userName); // "ALICE"
```

## API Reference

### Option<T> struct

#### Properties
- `bool HasValue` - True if the option contains a value
- `bool IsNone` - True if the option is empty
- `T Value` - The optional value (throws if empty)

#### Methods
- `static Option<T> Some(T value)` - Create an option with a value
- `static Option<T> None()` - Create an empty option
- `T ValueOr(T defaultValue)` - Get value or default
- `TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)` - Pattern matching
- `Option<TResult> Map<TResult>(Func<T, TResult> mapper)` - Value transformation
- `Option<TResult> Bind<TResult>(Func<T, Option<TResult>> binder)` - Monadic bind

### Option Static Class

- `Option<T> Some<T>(T value)` - Create an option with a value
- `Option<T> None<T>()` - Create an empty option

### OptionExtensions

- `Option<T> ToOption<T>(this T? value)` - Convert nullable reference type
- `Option<T> ToOption<T>(this T? value) where T : struct` - Convert nullable value type

## License

MIT License

## Related Packages

- **Fox.ResultKit** - Result<T, TError> pattern implementation
- **Fox.ChainKit** - Chain of Responsibility framework

## Contributing

Contributions are welcome! Please open an issue or pull request in the GitHub repository.

## Support

If you like Fox.OptionKit, please give it a ⭐ on the GitHub repository!
