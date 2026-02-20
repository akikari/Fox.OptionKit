//==================================================================================================
// Contains unit tests for the OptionExtensions class.
// Tests ToOption extension methods for reference and value types.
//==================================================================================================

namespace Fox.OptionKit.Tests;

//======================================================================================================
/// <summary>
/// Contains unit tests for the <see cref="OptionExtensions"/> class.
/// </summary>
//======================================================================================================
public sealed class OptionExtensionsTests
{
    //==================================================================================================
    /// <summary>
    /// Tests that ToOption creates Some for non-null reference type.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToOption_should_create_some_for_non_null_reference_type()
    {
        string value = "Hello";

        var option = value.ToOption();

        option.HasValue.Should().BeTrue();
        option.Value.Should().Be("Hello");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ToOption creates None for null reference type.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToOption_should_create_none_for_null_reference_type()
    {
        string? value = null;

        var option = value.ToOption();

        option.HasValue.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ToOption creates Some for nullable value type with value.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToOption_should_create_some_for_nullable_with_value()
    {
        int? value = 42;

        var option = value.ToOption();

        option.HasValue.Should().BeTrue();
        option.Value.Should().Be(42);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ToOption creates None for nullable value type without value.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToOption_should_create_none_for_nullable_without_value()
    {
        int? value = null;

        var option = value.ToOption();

        option.HasValue.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ToOption works with complex reference types.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToOption_should_work_with_complex_reference_types()
    {
        var value = new { Name = "Test", Value = 42 };

        var option = value.ToOption();

        option.HasValue.Should().BeTrue();
        option.Value.Name.Should().Be("Test");
        option.Value.Value.Should().Be(42);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ToOption can be chained with Map.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToOption_should_support_chaining_with_map()
    {
        string value = "Hello";

        var result = value.ToOption()
            .Map(s => s.Length)
            .Map(len => len * 2);

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(10);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that ToOption handles null gracefully in a chain.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void ToOption_should_handle_null_in_chain()
    {
        string? value = null;

        var result = value.ToOption()
            .Map(s => s.Length)
            .Map(len => len * 2);

        result.HasValue.Should().BeFalse();
    }
}
