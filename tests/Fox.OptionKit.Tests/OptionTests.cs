//==================================================================================================
// Contains unit tests for the Option<T> struct core functionality.
// Tests Some, None, HasValue, IsNone, Value, and ValueOr operations.
//==================================================================================================

namespace Fox.OptionKit.Tests;

//======================================================================================================
/// <summary>
/// Contains unit tests for the <see cref="Option{T}"/> struct.
/// </summary>
//======================================================================================================
public sealed class OptionTests
{
    //==================================================================================================
    /// <summary>
    /// Tests that Some creates an option with a value.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Some_should_create_option_with_value()
    {
        var option = Option<int>.Some(42);

        option.HasValue.Should().BeTrue();
        option.IsNone.Should().BeFalse();
        option.Value.Should().Be(42);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that None creates an empty option.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void None_should_create_empty_option()
    {
        var option = Option<int>.None();

        option.HasValue.Should().BeFalse();
        option.IsNone.Should().BeTrue();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Some throws ArgumentNullException for null reference type.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Some_should_throw_for_null_reference_type()
    {
        Action act = () => Option<string>.Some(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Value throws InvalidOperationException for None.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Value_should_throw_for_none()
    {
        var option = Option<int>.None();

        Action act = () => { var value = option.Value; };

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot access value of None option.");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ValueOr returns the option value when present.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ValueOr_should_return_value_when_some()
    {
        var option = Option<int>.Some(42);

        var result = option.ValueOr(100);

        result.Should().Be(42);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ValueOr returns the default value when None.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ValueOr_should_return_default_when_none()
    {
        var option = Option<int>.None();

        var result = option.ValueOr(100);

        result.Should().Be(100);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ToString returns correct representation for Some.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToString_should_return_some_representation()
    {
        var option = Option<int>.Some(42);

        var result = option.ToString();

        result.Should().Be("Some(42)");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ToString returns correct representation for None.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToString_should_return_none_representation()
    {
        var option = Option<int>.None();

        var result = option.ToString();

        result.Should().Be("None");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that two Some options with equal values are equal.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_should_return_true_for_equal_some_options()
    {
        var option1 = Option<int>.Some(42);
        var option2 = Option<int>.Some(42);

        var result = option1.Equals(option2);

        result.Should().BeTrue();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that two None options are equal.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_should_return_true_for_none_options()
    {
        var option1 = Option<int>.None();
        var option2 = Option<int>.None();

        var result = option1.Equals(option2);

        result.Should().BeTrue();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Some and None options are not equal.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_should_return_false_for_some_and_none()
    {
        var option1 = Option<int>.Some(42);
        var option2 = Option<int>.None();

        var result = option1.Equals(option2);

        result.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that equality operator works correctly for equal options.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void EqualityOperator_should_return_true_for_equal_options()
    {
        var option1 = Option<int>.Some(42);
        var option2 = Option<int>.Some(42);

        var result = option1 == option2;

        result.Should().BeTrue();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that inequality operator works correctly for different options.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void InequalityOperator_should_return_true_for_different_options()
    {
        var option1 = Option<int>.Some(42);
        var option2 = Option<int>.Some(100);

        var result = option1 != option2;

        result.Should().BeTrue();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that GetHashCode returns same value for equal options.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void GetHashCode_should_return_same_value_for_equal_options()
    {
        var option1 = Option<int>.Some(42);
        var option2 = Option<int>.Some(42);

        var hash1 = option1.GetHashCode();
        var hash2 = option2.GetHashCode();

        hash1.Should().Be(hash2);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that GetHashCode returns zero for None options.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void GetHashCode_should_return_zero_for_none()
    {
        var option = Option<int>.None();

        var hash = option.GetHashCode();

        hash.Should().Be(0);
    }
}
