//==================================================================================================
// Contains unit tests for the Option static factory class.
// Tests Some<T> and None<T> factory methods.
//==================================================================================================

namespace Fox.OptionKit.Tests;

//======================================================================================================
/// <summary>
/// Contains unit tests for the <see cref="Option"/> static class.
/// </summary>
//======================================================================================================
public sealed class OptionFactoryTests
{
    //==================================================================================================
    /// <summary>
    /// Tests that Option.Some creates an option with a value.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Some_should_create_option_with_value()
    {
        var option = Option.Some(42);

        option.HasValue.Should().BeTrue();
        option.Value.Should().Be(42);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Option.None creates an empty option.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void None_should_create_empty_option()
    {
        var option = Option.None<int>();

        option.HasValue.Should().BeFalse();
        option.IsNone.Should().BeTrue();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Option.Some throws ArgumentNullException for null reference type.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Some_should_throw_for_null_reference_type()
    {
        Action act = () => Option.Some<string>(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Option.Some works with reference types.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Some_should_work_with_reference_types()
    {
        var option = Option.Some("Hello");

        option.HasValue.Should().BeTrue();
        option.Value.Should().Be("Hello");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Option.Some works with value types.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Some_should_work_with_value_types()
    {
        var option = Option.Some(42);

        option.HasValue.Should().BeTrue();
        option.Value.Should().Be(42);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Option.None works with reference types.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void None_should_work_with_reference_types()
    {
        var option = Option.None<string>();

        option.HasValue.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Option.None works with value types.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void None_should_work_with_value_types()
    {
        var option = Option.None<int>();

        option.HasValue.Should().BeFalse();
    }
}
