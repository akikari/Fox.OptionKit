//==================================================================================================
// Defensive code tests for Option<T> to achieve 100% code coverage.
// Tests private constructor null checks and object.Equals override behavior.
//==================================================================================================

namespace Fox.OptionKit.Tests;

//======================================================================================================
/// <summary>
/// Tests for defensive code paths in Option{T} to ensure 100% code coverage.
/// </summary>
//======================================================================================================
public sealed class OptionDefensiveTests
{
    //==================================================================================================
    /// <summary>
    /// Tests that the private constructor throws ArgumentNullException for null reference types.
    /// This covers the defensive null check: if (value is null) throw ...
    /// </summary>
    //==================================================================================================
    [Fact]
    public void PrivateConstructor_should_throw_ArgumentNullException_for_null_reference_type()
    {
        var constructor = typeof(Option<string>).GetConstructor(
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
            null,
            [typeof(string)],
            null);

        var act = () => constructor!.Invoke([null!]);

        act.Should().Throw<System.Reflection.TargetInvocationException>()
            .WithInnerException<ArgumentNullException>()
            .WithMessage("*Cannot create Some option with null value*");
    }

    //==================================================================================================
    /// <summary>
    /// Tests the object.Equals(object?) override with a null argument.
    /// This ensures the defensive code path 'obj is Option{T} other' is covered.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_object_should_return_false_for_null()
    {
        var option = Option.Some(42);

        var result = option.Equals(null);

        result.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests the object.Equals(object?) override with a non-Option type.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_object_should_return_false_for_different_type()
    {
        var option = Option.Some(42);

        var result = option.Equals("not an option");

        result.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests the object.Equals(object?) override with another Option of the same value.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_object_should_return_true_for_equal_option_boxed()
    {
        var option1 = Option.Some(42);
        object option2 = Option.Some(42);

        var result = option1.Equals(option2);

        result.Should().BeTrue();
    }

    //==================================================================================================
    /// <summary>
    /// Tests the object.Equals(object?) override with another Option of different value.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_object_should_return_false_for_different_option_value()
    {
        var option1 = Option.Some(42);
        object option2 = Option.Some(99);

        var result = option1.Equals(option2);

        result.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests the object.Equals(object?) override with None vs Some.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_object_should_return_false_for_none_vs_some()
    {
        var none = Option.None<int>();
        object some = Option.Some(42);

        var result = none.Equals(some);

        result.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests the object.Equals(object?) override with two None instances.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Equals_object_should_return_true_for_two_none_instances()
    {
        var none1 = Option.None<int>();
        object none2 = Option.None<int>();

        var result = none1.Equals(none2);

        result.Should().BeTrue();
    }
}
