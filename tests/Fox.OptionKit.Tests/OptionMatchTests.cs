//==================================================================================================
// Contains unit tests for the Option<T> Match operation.
// Tests pattern matching with Some and None cases.
//==================================================================================================

namespace Fox.OptionKit.Tests;

//======================================================================================================
/// <summary>
/// Contains unit tests for the <see cref="Option{T}.Match"/> method.
/// </summary>
//======================================================================================================
public sealed class OptionMatchTests
{
    //==================================================================================================
    /// <summary>
    /// Tests that Match executes the some function when option has a value.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Match_should_execute_some_function_when_has_value()
    {
        var option = Option<int>.Some(42);

        var result = option.Match(
            some: value => value * 2,
            none: () => 0);

        result.Should().Be(84);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Match executes the none function when option is empty.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Match_should_execute_none_function_when_empty()
    {
        var option = Option<int>.None();

        var result = option.Match(
            some: value => value * 2,
            none: () => 0);

        result.Should().Be(0);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Match throws ArgumentNullException when some function is null.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Match_should_throw_when_some_is_null()
    {
        var option = Option<int>.Some(42);

        Action act = () => option.Match(
            some: null!,
            none: () => 0);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("some");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Match throws ArgumentNullException when none function is null.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Match_should_throw_when_none_is_null()
    {
        var option = Option<int>.Some(42);

        Action act = () => option.Match(
            some: value => value * 2,
            none: null!);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("none");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Match can return different types.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Match_should_support_different_return_types()
    {
        var option = Option<int>.Some(42);

        var result = option.Match(
            some: value => $"Value: {value}",
            none: () => "No value");

        result.Should().Be("Value: 42");
    }
}
