# Fox.OptionKit

[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-512BD4)](https://dotnet.microsoft.com/)
[![Build and Test](https://img.shields.io/github/actions/workflow/status/akikari/Fox.OptionKit/build-and-test.yml?branch=main&label=build%20and%20test&color=darkgreen)](https://github.com/akikari/Fox.OptionKit/actions/workflows/build-and-test.yml)
[![NuGet](https://img.shields.io/nuget/v/Fox.OptionKit.svg)](https://www.nuget.org/packages/Fox.OptionKit/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Fox.OptionKit?label=downloads&color=darkgreen)](https://www.nuget.org/packages/Fox.OptionKit/)
[![License: MIT](https://img.shields.io/badge/license-MIT-orange.svg)](https://opensource.org/licenses/MIT)
[![codecov](https://img.shields.io/codecov/c/github/akikari/Fox.OptionKit?color=darkgreen&label=codecov)](https://codecov.io/gh/akikari/Fox.OptionKit)

A minimalist, dependency-free `Option<T>` implementation for C#. Type-safe optional values with functional programming patterns.

## Why Fox.OptionKit?

Fox.OptionKit provides a clean, lightweight implementation of the Option pattern that brings functional programming safety to C# without any dependencies:

- **Zero Dependencies** - No external dependencies, not even Microsoft.Extensions.*
- **Type-Safe** - Eliminate NullReferenceExceptions with explicit optional types
- **Functional** - Map, Bind, Match operations for clean composition
- **Lightweight** - Minimal overhead, simple API, fast compilation
- **Well-Documented** - Comprehensive XML documentation for IntelliSense
- **Testable** - Immutable struct with predictable behavior

## Installation

```bash
dotnet add package Fox.OptionKit
```

## Quick Start

### 1. Create Options

```csharp
using Fox.OptionKit;

// Create Some (value present)
var some = Option.Some(42);
var someString = Option.Some("Hello");

// Create None (value absent)
var none = Option.None<int>();

// From nullable types
string? nullable = GetNullableString();
Option<string> option = nullable.ToOption();
```

### 2. Use Pattern Matching

```csharp
// Match pattern
var result = option.Match(
    some: value => $"Found: {value}",
    none: () => "Not found");

// Check state
if (option.HasValue)
{
    Console.WriteLine($"Value: {option.Value}");
}

// Get value or default
var value = option.ValueOr("default");
```

### 3. Functional Composition

```csharp
// Map: transform value if present
var doubled = Option.Some(21)
    .Map(x => x * 2);  // Some(42)

// Bind: chain operations that return Option
var userId = Option.Some("user@example.com")
    .Bind(email => FindUserByEmail(email))
    .Bind(user => GetUserId(user));

// Combine operations
var greeting = Option.Some("World")
    .Map(name => $"Hello, {name}!")
    .ValueOr("Hello, stranger!");
```

## Key Features

### Type-Safe Optional Values

Eliminate null reference exceptions with explicit optional types:

```csharp
// Instead of nullable reference
string? name = GetName();  // Might be null
Console.WriteLine(name.Length);  // NullReferenceException!

// Use Option<T>
Option<string> name = GetName().ToOption();
Console.WriteLine(name.Match(
    some: n => $"Length: {n.Length}",
    none: () => "No name"));
```

### Functional Composition

Chain operations cleanly with Map and Bind:

```csharp
// Map: transform value inside Option
var result = Option.Some("hello")
    .Map(s => s.ToUpper())        // Some("HELLO")
    .Map(s => s.Length);          // Some(5)

// Bind: chain operations that return Option
var user = Option.Some("user@example.com")
    .Bind(email => FindUser(email))      // Option<User>
    .Bind(user => GetUserProfile(user)); // Option<Profile>
```

### Pattern Matching

Handle both Some and None cases explicitly:

```csharp
var message = config.GetValue("ApiKey").Match(
    some: key => $"API Key configured: {key}",
    none: () => "API Key missing - using defaults");

// Or use HasValue/IsNone
if (maybeUser.HasValue)
{
    ProcessUser(maybeUser.Value);
}
```

### Null Safety

Convert nullable types to Options:

```csharp
// From nullable reference types
string? nullableString = GetString();
Option<string> option = nullableString.ToOption();

// From nullable value types
int? nullableInt = GetInt();
Option<int> option = nullableInt.ToOption();

// Safe dictionary access
var value = dictionary.TryGetValue("key", out var v) 
    ? Option.Some(v) 
    : Option.None<string>();
```

## Architecture

Fox.OptionKit is built around a simple, immutable `Option<T>` struct:

```
Option<T>
├── Some(value)    → HasValue = true,  IsNone = false
└── None()         → HasValue = false, IsNone = true
```

**Core operations:**
- `Some(T)` / `None()` - Create options
- `Match<TResult>` - Pattern matching
- `Map<TResult>` - Transform value (Functor)
- `Bind<TResult>` - Chain operations (Monad)
- `ValueOr(T)` - Safe value extraction

## Use Cases

Fox.OptionKit is ideal for:

- **API Responses** - Return optional data without null checks
- **Configuration Values** - Handle missing config gracefully
- **Database Queries** - Represent "not found" explicitly
- **Parsing Operations** - Return Some(result) or None for invalid input
- **Domain Models** - Model optional properties type-safely
- **Service Results** - Eliminate null returns from service methods

## Performance

Fox.OptionKit is designed for zero-overhead abstraction:

- **Zero Allocations** - `Option<T>` is a readonly struct, no heap allocations
- **Zero Dependencies** - No external dependencies means minimal assembly size
- **Inline-Friendly** - Simple methods can be inlined by JIT compiler
- **No Boxing** - Value types stay on the stack throughout the pipeline

## Comparison

| Feature | Fox.OptionKit | LanguageExt | Optional | C# Nullable |
|---------|---------------|-------------|----------|-------------|
| Zero Dependencies | ✅ | ❌ | ✅ | ✅ (built-in) |
| Struct (value type) | ✅ | ❌ (class) | ✅ | ✅ |
| Map/Bind/Match | ✅ | ✅ | ✅ | ❌ |
| Extension Methods | ✅ | ✅ | ✅ | ❌ |
| Static Factory | ✅ | ✅ | ✅ | N/A |
| Null Safety | ✅ | ✅ | ✅ | ⚠️ (partial) |
| Learning Curve | Low | High | Low | None |
| Multi-targeting | .NET 8-10 | .NET 6+ | .NET Standard | Built-in |

## 📖 Sample Application

A comprehensive sample application is available in the repository demonstrating:

- ✅ Basic Option creation (Some/None)
- ✅ Pattern matching with Match
- ✅ Functional composition with Map and Bind
- ✅ Value extraction with ValueOr
- ✅ Nullable conversion with ToOption
- ✅ Real-world examples (dictionary lookups, safe access)

**Run the sample:**

```bash
cd samples/Fox.OptionKit.Demo
dotnet run
```

**Explore:**
- View [Program.cs](samples/Fox.OptionKit.Demo/Program.cs) for complete examples

## 📚 Documentation

- [Core Package Documentation](src/Fox.OptionKit/README.md)
- [Sample Application](samples/Fox.OptionKit.Demo)
- [Contributing Guidelines](CONTRIBUTING.md)
- [Changelog](CHANGELOG.md)

## 🎯 Design Principles

1. **Zero Dependencies** - No external dependencies, not even Microsoft.Extensions.*
2. **Type-Safe** - Explicit optional values, no null references
3. **Immutable** - Readonly struct, all operations return new instances
4. **Simple** - Small API surface, easy to learn and use
5. **Functional** - Map, Bind, Match operations following functor/monad laws
6. **Performance** - Zero allocations, inline-friendly, minimal overhead

## 🔧 Requirements

- .NET 8.0 or higher
- C# 12 or higher (for modern language features)
- Nullable reference types enabled (recommended)

## 🤝 Contributing

**Fox.OptionKit is intentionally lightweight and feature-focused.** The goal is to remain a simple library with zero dependencies for the Option pattern.

### What We Welcome

- ✅ **Bug fixes** - Issues with existing functionality
- ✅ **Documentation improvements** - Clarifications, examples, typo fixes
- ✅ **Performance optimizations** - Without breaking API compatibility
- ✅ **New features** - Following existing patterns and SOLID principles

### What We Generally Do Not Accept

- ❌ Any external dependencies (this library must remain dependency-free)
- ❌ Large feature additions that increase complexity significantly
- ❌ Breaking API changes without strong justification

If you want to propose a significant change, please open an issue first to discuss whether it aligns with the project's philosophy.

### Build Policy

The project enforces a **strict build policy** to ensure code quality:

- ❌ **No errors allowed** - Build must be error-free
- ❌ **No warnings allowed** - All compiler warnings must be resolved
- ❌ **No messages allowed** - Informational messages must be suppressed or addressed

All pull requests must pass this requirement.

### Code Style

- Follow the existing code style (see `.github/copilot-instructions.md`)
- Use file-scoped namespaces
- Enable nullable reference types
- Add XML documentation for public APIs
- Write unit tests for new features (xUnit + FluentAssertions)
- Use expression-bodied members for simple properties
- Auto-properties preferred over backing fields

### How to Contribute

1. Fork the repository
2. Create a feature branch from `main`
3. Follow the coding standards in `.github/copilot-instructions.md`
4. Ensure all tests pass (`dotnet test`)
5. Submit a pull request

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed guidelines.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## 👤 Author

**Károly Akácz**

- GitHub: [@akikari](https://github.com/akikari)
- Repository: [Fox.OptionKit](https://github.com/akikari/Fox.OptionKit)

## 📊 Project Status

[![NuGet Version](https://img.shields.io/nuget/v/Fox.OptionKit.svg)](https://www.nuget.org/packages/Fox.OptionKit/)

See [CHANGELOG.md](CHANGELOG.md) for version history.

## 🔗 Related Projects

- [Fox.ResultKit](https://github.com/akikari/Fox.ResultKit) - Lightweight Result pattern library for Railway Oriented Programming
- [Fox.ChainKit](https://github.com/akikari/Fox.ChainKit) - Chain of Responsibility pattern implementation
- [Fox.ConfigKit](https://github.com/akikari/Fox.ConfigKit) - Type-safe configuration validation library for .NET

## 📞 Support

For issues, questions, or feature requests, please open an issue in the [GitHub repository](https://github.com/akikari/Fox.OptionKit/issues).
