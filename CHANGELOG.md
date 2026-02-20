# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

_No unreleased changes yet._

## [1.0.0] - 2026-02-20

### Added

#### Fox.OptionKit (Core Library)
- `Option<T>` struct for type-safe optional values
- `Some(T value)` static factory method for creating options with values
- `None()` static factory method for creating empty options
- `HasValue` and `IsNone` properties for checking option state
- `Value` property for accessing the contained value (throws if empty)
- `ValueOr(T defaultValue)` method for safe value access with fallback
- `Match<TResult>(Func<T, TResult> some, Func<TResult> none)` for pattern matching
- `Map<TResult>(Func<T, TResult> mapper)` for value transformation
- `Bind<TResult>(Func<T, Option<TResult>> binder)` for monadic composition
- `ToString()` override with "Some(value)" and "None" representations
- `Equals()`, `GetHashCode()` implementations for proper value equality
- `==` and `!=` operators for option comparison
- `Option` static class with generic factory methods `Some<T>()` and `None<T>()`
- `ToOption()` extension methods for converting nullable types to options
- Support for both nullable reference types and nullable value types
- Zero external dependencies (dependency-free implementation)
- Full XML documentation for all public APIs
- Global suppressions for CA1716 (Option keyword in F#)

#### Documentation
- Comprehensive README.md with usage examples
- API reference documentation
- Real-world examples (dictionary lookup, safe operations)
- Installation and quick start guide
- MIT License (2025)

#### Samples
- Demo application demonstrating all features:
  - Basic Some/None usage
  - ValueOr for safe defaults
  - Map for chaining transformations
  - Bind for monadic operations
  - Match for pattern matching
  - ToOption for nullable conversion
  - Real-world dictionary lookup example

#### Tests
- 135 comprehensive unit tests (100% passing across all target frameworks)
- OptionTests: Core functionality tests
  - Some/None creation
  - Value access and ValueOr
  - ToString representations
  - Equality and hash code
  - Operators
- OptionMatchTests: Pattern matching tests
  - Some/None branch execution
  - Null argument validation
  - Type transformation
- OptionMapBindTests: Functional composition tests
  - Map transformations
  - Bind operations
  - Chaining behavior
  - Short-circuit on None
- OptionFactoryTests: Static factory tests
  - Option.Some<T>() behavior
  - Option.None<T>() behavior
  - Reference and value type support
- OptionExtensionsTests: Extension method tests
  - ToOption for nullable references
  - ToOption for nullable value types
  - Chaining with Map/Bind

#### Build & CI/CD
- Multi-targeting: .NET 8.0, .NET 9.0, .NET 10.0
- NuGet package metadata with icon and README embedding
- Symbol packages (.snupkg) for debugging support
- Artifacts output to `artifacts` directory
- Generated XML documentation file
- Global suppressions configuration

### Initial Release
- Production-ready code quality
- All nullable reference types enabled
- Follows Microsoft coding conventions and project-specific guidelines
- Comprehensive XML documentation in English
- Full test coverage with xUnit and FluentAssertions
- CRLF line endings and UTF-8 encoding
- File-scoped namespaces
- Region-organized code structure

