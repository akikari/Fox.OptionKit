//==================================================================================================
// Demonstrates the usage of Fox.OptionKit library with practical examples.
// Shows Some, None, Map, Bind, Match, ValueOr, and ToOption operations.
//==================================================================================================

using Fox.OptionKit;

//======================================================================================================
// Basic usage: Some and None
//======================================================================================================
Console.WriteLine("=== Basic Usage ===");

var someValue = Option.Some(42);
Console.WriteLine($"someValue: {someValue}");
Console.WriteLine($"Has value: {someValue.HasValue}");
Console.WriteLine($"Value: {someValue.Value}");

var noneValue = Option.None<int>();
Console.WriteLine($"\nnoneValue: {noneValue}");
Console.WriteLine($"Is none: {noneValue.IsNone}");

//======================================================================================================
// ValueOr: Safe value access with default
//======================================================================================================
Console.WriteLine("\n=== ValueOr ===");

var value1 = someValue.ValueOr(0);
var value2 = noneValue.ValueOr(100);
Console.WriteLine($"someValue.ValueOr(0): {value1}");
Console.WriteLine($"noneValue.ValueOr(100): {value2}");

//======================================================================================================
// Map: Transform values
//======================================================================================================
Console.WriteLine("\n=== Map ===");

var mapped = Option.Some(10)
    .Map(x => x * 2)
    .Map(x => x + 5)
    .Map(x => $"Result: {x}");

Console.WriteLine($"Mapped chain: {mapped}");

var mappedNone = Option.None<int>()
    .Map(x => x * 2)
    .Map(x => x + 5);

Console.WriteLine($"Mapped none: {mappedNone}");

//======================================================================================================
// Bind: Monadic composition
//======================================================================================================
Console.WriteLine("\n=== Bind ===");

Option<int> Divide(int numerator, int denominator)
{
    return denominator == 0 ? Option.None<int>() : Option.Some(numerator / denominator);
}

var bindResult1 = Option.Some(100)
    .Bind(x => Divide(x, 2))
    .Bind(x => Divide(x, 5));

Console.WriteLine($"100 / 2 / 5: {bindResult1}");

var bindResult2 = Option.Some(100)
    .Bind(x => Divide(x, 0))
    .Bind(x => Divide(x, 5));

Console.WriteLine($"100 / 0 / 5: {bindResult2}");

//======================================================================================================
// Match: Pattern matching
//======================================================================================================
Console.WriteLine("\n=== Match ===");

var matchResult1 = Option.Some(42).Match(
    some: value => $"Found: {value}",
    none: () => "Not found");

Console.WriteLine($"Match Some: {matchResult1}");

var matchResult2 = Option.None<int>().Match(
    some: value => $"Found: {value}",
    none: () => "Not found");

Console.WriteLine($"Match None: {matchResult2}");

//======================================================================================================
// ToOption: Convert nullable to option
//======================================================================================================
Console.WriteLine("\n=== ToOption ===");

string? nullableString = "Hello";
var optionFromNullable = nullableString.ToOption();
Console.WriteLine($"From non-null string: {optionFromNullable}");

nullableString = null;
var optionFromNull = nullableString.ToOption();
Console.WriteLine($"From null string: {optionFromNull}");

int? nullableInt = 42;
var optionFromInt = nullableInt.ToOption();
Console.WriteLine($"From nullable int (42): {optionFromInt}");

nullableInt = null;
var optionFromNullInt = nullableInt.ToOption();
Console.WriteLine($"From nullable int (null): {optionFromNullInt}");

//======================================================================================================
// Real-world example: Safe dictionary lookup
//======================================================================================================
Console.WriteLine("\n=== Real-World Example ===");

var users = new Dictionary<int, string>
{
    { 1, "Alice" },
    { 2, "Bob" },
    { 3, "Charlie" }
};

Option<string> GetUser(int id)
{
    return users.TryGetValue(id, out var name) ? Option.Some(name) : Option.None<string>();
}

var user1 = GetUser(1)
    .Map(name => name.ToUpper())
    .ValueOr("UNKNOWN");

Console.WriteLine($"User 1: {user1}");

var user99 = GetUser(99)
    .Map(name => name.ToUpper())
    .ValueOr("UNKNOWN");

Console.WriteLine($"User 99: {user99}");

var greeting = GetUser(2).Match(
    some: name => $"Hello, {name}!",
    none: () => "Hello, stranger!");

Console.WriteLine($"Greeting: {greeting}");

Console.WriteLine("\n=== Demo Complete ===");
