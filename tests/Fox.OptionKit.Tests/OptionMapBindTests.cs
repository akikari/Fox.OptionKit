//==================================================================================================
// Contains unit tests for the Option<T> Map and Bind operations.
// Tests functional composition and monadic operations.
//==================================================================================================

namespace Fox.OptionKit.Tests;

//======================================================================================================
/// <summary>
/// Contains unit tests for the <see cref="Option{T}.Map"/> and <see cref="Option{T}.Bind"/> methods.
/// </summary>
//======================================================================================================
public sealed class OptionMapBindTests
{
    //==================================================================================================
    /// <summary>
    /// Tests that Map transforms the value when present.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Map_should_transform_value_when_some()
    {
        var option = Option<int>.Some(42);

        var result = option.Map(value => value * 2);

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(84);
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Map returns None when option is empty.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Map_should_return_none_when_none()
    {
        var option = Option<int>.None();

        var result = option.Map(value => value * 2);

        result.HasValue.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Map can change the type of the option.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Map_should_change_option_type()
    {
        var option = Option<int>.Some(42);

        var result = option.Map(value => value.ToString());

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("42");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Map throws ArgumentNullException when mapper is null.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Map_should_throw_when_mapper_is_null()
    {
        var option = Option<int>.Some(42);

        Action act = () => option.Map<string>(null!);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("mapper");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Map can be chained multiple times.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Map_should_support_chaining()
    {
        var option = Option<int>.Some(10);

        var result = option
            .Map(value => value * 2)
            .Map(value => value + 5)
            .Map(value => value.ToString());

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("25");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Bind transforms the value when present.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Bind_should_transform_value_when_some()
    {
        var option = Option<int>.Some(42);

        var result = option.Bind(value => Option<string>.Some(value.ToString()));

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("42");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Bind returns None when option is empty.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Bind_should_return_none_when_none()
    {
        var option = Option<int>.None();

        var result = option.Bind(value => Option<string>.Some(value.ToString()));

        result.HasValue.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Bind can return None from the binder function.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Bind_should_support_binder_returning_none()
    {
        var option = Option<int>.Some(42);

        var result = option.Bind(value => Option<string>.None());

        result.HasValue.Should().BeFalse();
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Bind throws ArgumentNullException when binder is null.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Bind_should_throw_when_binder_is_null()
    {
        var option = Option<int>.Some(42);

        Action act = () => option.Bind<string>(null!);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("binder");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Bind can be chained multiple times.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Bind_should_support_chaining()
    {
        var option = Option<int>.Some(10);

        var result = option
            .Bind(value => value > 5 ? Option<int>.Some(value * 2) : Option<int>.None())
            .Bind(value => value < 100 ? Option<string>.Some(value.ToString()) : Option<string>.None());

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("20");
    }

    //==================================================================================================
    /// <summary>
    /// Tests that Bind short-circuits when encountering None.
    /// </summary>
    //==================================================================================================
    [Fact]
    public void Bind_should_short_circuit_on_none()
    {
        var option = Option<int>.Some(3);
        var executionCount = 0;

        var result = option
            .Bind(value => value > 5 ? Option<int>.Some(value * 2) : Option<int>.None())
            .Bind(value =>
            {
                executionCount++;
                return Option<string>.Some(value.ToString());
            });

        result.HasValue.Should().BeFalse();
        executionCount.Should().Be(0);
    }
}
