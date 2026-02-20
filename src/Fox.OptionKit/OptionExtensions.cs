//==================================================================================================
// Provides extension methods for converting values to Option<T>.
// Enables fluent API for creating options from existing values.
//==================================================================================================

namespace Fox.OptionKit;

//======================================================================================================
/// <summary>
/// Provides extension methods for creating <see cref="Option{T}"/> instances.
/// </summary>
//======================================================================================================
public static class OptionExtensions
{
    //==================================================================================================
    /// <summary>
    /// Converts a value to an option. Returns None if the value is null; otherwise, returns Some.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <returns>An option containing the value if not null; otherwise, an empty option.</returns>
    //==================================================================================================
    public static Option<T> ToOption<T>(this T? value) where T : class
    {
        return value is null ? Option<T>.None() : Option<T>.Some(value);
    }

    //==================================================================================================
    /// <summary>
    /// Converts a nullable value type to an option. Returns None if null; otherwise, returns Some.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The nullable value to convert.</param>
    /// <returns>An option containing the value if not null; otherwise, an empty option.</returns>
    //==================================================================================================
    public static Option<T> ToOption<T>(this T? value) where T : struct
    {
        return value.HasValue ? Option<T>.Some(value.Value) : Option<T>.None();
    }
}
