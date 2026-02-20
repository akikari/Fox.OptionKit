//==================================================================================================
// Provides static factory methods for creating Option<T> instances.
// Offers a convenient API for constructing Some and None options.
//==================================================================================================

namespace Fox.OptionKit;

//======================================================================================================
/// <summary>
/// Provides static factory methods for creating <see cref="Option{T}"/> instances.
/// </summary>
//======================================================================================================
public static class Option
{
    //==================================================================================================
    /// <summary>
    /// Creates an option containing the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to wrap.</param>
    /// <returns>An option containing the value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when value is null for reference types.</exception>
    //==================================================================================================
    public static Option<T> Some<T>(T value) => Option<T>.Some(value);

    //==================================================================================================
    /// <summary>
    /// Creates an empty option of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the option.</typeparam>
    /// <returns>An empty option.</returns>
    //==================================================================================================
    public static Option<T> None<T>() => Option<T>.None();
}
