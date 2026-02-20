# Contributing to Fox.OptionKit

Thank you for your interest in contributing to Fox.OptionKit! This document provides guidelines and instructions for contributing to the project.

## Code of Conduct

By participating in this project, you agree to maintain a respectful and inclusive environment for all contributors.

## How to Contribute

### Reporting Issues

If you find a bug or have a feature request:

1. Check if the issue already exists in the [GitHub Issues](https://github.com/akikari/Fox.OptionKit/issues)
2. If not, create a new issue with:
   - Clear, descriptive title
   - Detailed description of the problem or feature
   - Steps to reproduce (for bugs)
   - Expected vs actual behavior
   - Code samples if applicable
   - Environment details (.NET version, OS, etc.)

### Submitting Changes

1. **Fork the repository** and create a new branch from `main`
2. **Make your changes** following the coding guidelines below
3. **Write or update tests** for your changes
4. **Update documentation** if needed (README, XML comments)
5. **Ensure all tests pass** (`dotnet test`)
6. **Ensure build succeeds** (`dotnet build`)
7. **Submit a pull request** with:
   - Clear description of changes
   - Reference to related issues
   - Summary of testing performed

## Coding Guidelines

Fox.OptionKit follows strict coding standards. Please review the [Copilot Instructions](.github/copilot-instructions.md) for detailed guidelines.

### Key Standards

#### General
- **Language**: All code, comments, and documentation must be in English
- **Line Endings**: CRLF
- **Indentation**: 4 spaces (no tabs)
- **Namespaces**: File-scoped (`namespace MyNamespace;`)
- **Nullable**: Enabled
- **Language Version**: latest

#### Naming Conventions
- **Private Fields**: camelCase without underscore prefix (e.g., `value`, not `_value`)
- **Public Members**: PascalCase
- **Local Variables**: camelCase

#### Code Style
- Use expression-bodied members for simple properties and methods
- Use auto-properties where possible
- Prefer `var` only when type is obvious
- Maximum line length: 100 characters
- Add blank line after closing brace UNLESS next line is also `}`

#### Documentation
- **XML Comments**: Required for all public APIs
- **Language**: English
- **Decorators**: 98 characters width using `//======` (no space after prefix)
- **File Headers**: 3-line header (purpose + technical description + decorators)

Example:
```csharp
//==================================================================================================
// Represents an optional value that can either contain a value (Some) or be empty (None).
// Provides a type-safe alternative to nullable references and null checking.
//==================================================================================================

namespace Fox.OptionKit;

//==================================================================================================
/// <summary>
/// Represents an optional value that can either contain a value of type <typeparamref name="T"/> or be empty.
/// </summary>
/// <typeparam name="T">The type of the optional value.</typeparam>
//==================================================================================================
public readonly struct Option<T>
{
    private readonly T value;

    //==============================================================================================
    /// <summary>
    /// Gets a value indicating whether this option contains a value.
    /// </summary>
    //==============================================================================================
    public bool HasValue { get; }

    //==============================================================================================
    /// <summary>
    /// Creates an option containing the specified value.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <returns>An option containing the value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when value is null for reference types.</exception>
    //==============================================================================================
    public static Option<T> Some(T value) => new(value);
}
```

## Testing Requirements

- **Framework**: xUnit
- **Assertions**: FluentAssertions
- **Test Naming**: `MethodName_should_expected_behavior`
- **Coverage**: Aim for 100% coverage of new code
- **Test Structure**:
  - Arrange: Setup test data
  - Act: Execute the method under test
  - Assert: Verify expected behavior

Example:
```csharp
[Fact]
public void Some_should_create_option_with_value()
{
    // Arrange
    var value = 42;

    // Act
    var option = Option.Some(value);

    // Assert
    option.HasValue.Should().BeTrue();
    option.Value.Should().Be(value);
}

[Fact]
public void Map_should_transform_value_when_some()
{
    // Arrange
    var option = Option.Some(10);

    // Act
    var result = option.Map(x => x * 2);

    // Assert
    result.HasValue.Should().BeTrue();
    result.Value.Should().Be(20);
}
```

## Architecture Principles

Fox.OptionKit follows functional programming principles and clean code practices:

- **Immutability**: Option<T> is a readonly struct
- **Type Safety**: No null references, explicit optional values
- **Composition**: Map, Bind for functional composition
- **Simplicity**: Minimal API surface, easy to understand
- **Zero Dependencies**: No external dependencies beyond base library

### Design Guidelines

- **No Null Values**: Option<T> cannot contain null for reference types
- **Explicit State**: HasValue/IsNone make state explicit
- **Functional Composition**: Map/Bind follow functor/monad laws
- **Pattern Matching**: Match provides exhaustive case handling
- **Extension-Friendly**: ToOption extensions for easy conversion

## Project Structure

```
Fox.OptionKit/
├── src/
│   └── Fox.OptionKit/              # Core package
│       ├── Option.cs               # Main Option<T> struct
│       ├── OptionFactory.cs        # Static factory class
│       ├── OptionExtensions.cs     # Extension methods
│       ├── GlobalSuppressions.cs   # Code analysis suppressions
│       └── README.md               # Package documentation
├── tests/
│   └── Fox.OptionKit.Tests/        # Unit tests
│       ├── OptionTests.cs          # Core functionality tests
│       ├── OptionMatchTests.cs     # Match operation tests
│       ├── OptionMapBindTests.cs   # Functional composition tests
│       ├── OptionFactoryTests.cs   # Factory method tests
│       └── OptionExtensionsTests.cs # Extension tests
├── samples/
│   └── Fox.OptionKit.Demo/         # Demo application
│       └── Program.cs              # Usage examples
└── assets/
    └── icon.png                    # Package icon
```

## Pull Request Process

1. **Update tests**: Ensure your changes are covered by tests
2. **Update documentation**: Keep README and XML comments up to date
3. **Follow coding standards**: Use provided `.editorconfig` and copilot instructions
4. **Keep commits clean**: 
   - Use clear, descriptive commit messages
   - Squash commits if needed before merging
5. **Update CHANGELOG.md**: Add entry under `[Unreleased]` section
6. **Ensure CI passes**: All tests must pass and build must succeed

### Commit Message Format

Use clear, imperative commit messages:

```
Add TryGetValue method for safe value extraction

- Implement TryGetValue(out T value) method
- Add unit tests for TryGetValue
- Update documentation and examples
```

## Feature Requests

When proposing new features, please consider:

1. **Scope**: Does this fit the minimalist nature of Fox.OptionKit?
2. **Complexity**: Does this add unnecessary complexity?
3. **Dependencies**: Does this require new external dependencies? (Should be avoided)
4. **Breaking Changes**: Will this break existing code?
5. **Use Cases**: What real-world scenarios does this address?
6. **Functional Purity**: Does this maintain functional programming principles?

Fox.OptionKit aims to be lightweight, dependency-free, and focused on the Option pattern. Features should align with functional programming principles and type-safe optional value handling.

## Development Setup

### Prerequisites
- .NET 8 SDK or later
- Visual Studio 2022+ or Rider (recommended)
- Git

### Getting Started

1. Clone the repository:
```bash
git clone https://github.com/akikari/Fox.OptionKit.git
cd Fox.OptionKit
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the solution:
```bash
dotnet build
```

4. Run tests:
```bash
dotnet test
```

5. Run the sample application:
```bash
dotnet run --project samples/Fox.OptionKit.Demo/Fox.OptionKit.Demo.csproj
```

6. Create NuGet package:
```bash
dotnet pack src/Fox.OptionKit/Fox.OptionKit.csproj -c Release
```

## Questions?

If you have questions about contributing, feel free to:
- Open a [GitHub Discussion](https://github.com/akikari/Fox.OptionKit/discussions)
- Create an issue labeled `question`
- Reach out to the maintainers

## License

By contributing to Fox.OptionKit, you agree that your contributions will be licensed under the MIT License.

Thank you for contributing to Fox.OptionKit! 🎉
