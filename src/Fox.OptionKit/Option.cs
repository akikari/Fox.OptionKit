//==================================================================================================
// Represents an optional value that can either contain a value (Some) or be empty (None).
// Provides a type-safe alternative to nullable references and null checking.
//==================================================================================================

namespace Fox.OptionKit;

//======================================================================================================
/// <summary>
/// Represents an optional value that can either contain a value of type <typeparamref name="T"/> or be empty.
/// </summary>
/// <typeparam name="T">The type of the optional value.</typeparam>
//======================================================================================================
public readonly struct Option<T>
{
    #region Fields

    private readonly T value;

    #endregion

    #region Constructors

    //==================================================================================================
    /// <summary>
    /// Initializes a new instance of the <see cref="Option{T}"/> struct with a value.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <exception cref="ArgumentNullException">Thrown when value is null for reference types.</exception>
    //==================================================================================================
    private Option(T value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Cannot create Some option with null value.");
        }

        this.value = value;
        HasValue = true;
    }

    #endregion

    #region Properties

    //==================================================================================================
    /// <summary>
    /// Gets a value indicating whether this option contains a value.
    /// </summary>
    //==================================================================================================
    public bool HasValue { get; }

    //==================================================================================================
    /// <summary>
    /// Gets a value indicating whether this option is empty.
    /// </summary>
    //==================================================================================================
    public bool IsNone => !HasValue;

    //==================================================================================================
    /// <summary>
    /// Gets the value contained in this option.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the option is empty.</exception>
    //==================================================================================================
    public T Value
    {
        get
        {
            if (!HasValue)
            {
                throw new InvalidOperationException("Cannot access value of None option.");
            }

            return value;
        }
    }

    #endregion

    #region Public Methods

    //==================================================================================================
    /// <summary>
    /// Creates an option containing the specified value.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <returns>An option containing the value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when value is null for reference types.</exception>
    //==================================================================================================
    public static Option<T> Some(T value) => new(value);

    //==================================================================================================
    /// <summary>
    /// Creates an empty option.
    /// </summary>
    /// <returns>An empty option.</returns>
    //==================================================================================================
    public static Option<T> None() => default;

    //==================================================================================================
    /// <summary>
    /// Gets the value if present, or returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value to return if the option is empty.</param>
    /// <returns>The option value if present; otherwise, the default value.</returns>
    //==================================================================================================
    public T ValueOr(T defaultValue) => HasValue ? value : defaultValue;

    //==================================================================================================
    /// <summary>
    /// Executes one of two functions based on whether the option contains a value.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="some">The function to execute if the option contains a value.</param>
    /// <param name="none">The function to execute if the option is empty.</param>
    /// <returns>The result of the executed function.</returns>
    /// <exception cref="ArgumentNullException">Thrown when some or none is null.</exception>
    //==================================================================================================
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);

        return none is null ? throw new ArgumentNullException(nameof(none)) : HasValue ? some(value) : none();
    }

    //==================================================================================================
    /// <summary>
    /// Transforms the option value using the specified function.
    /// </summary>
    /// <typeparam name="TResult">The type of the transformed value.</typeparam>
    /// <param name="mapper">The function to transform the value.</param>
    /// <returns>An option containing the transformed value if present; otherwise, an empty option.</returns>
    /// <exception cref="ArgumentNullException">Thrown when mapper is null.</exception>
    //==================================================================================================
    public Option<TResult> Map<TResult>(Func<T, TResult> mapper)
    {
        return mapper is null
            ? throw new ArgumentNullException(nameof(mapper))
            : HasValue ? Option<TResult>.Some(mapper(value)) : Option<TResult>.None();
    }

    //==================================================================================================
    /// <summary>
    /// Transforms the option value using a function that returns an option.
    /// </summary>
    /// <typeparam name="TResult">The type of the transformed value.</typeparam>
    /// <param name="binder">The function to transform the value.</param>
    /// <returns>The result of the transformation if present; otherwise, an empty option.</returns>
    /// <exception cref="ArgumentNullException">Thrown when binder is null.</exception>
    //==================================================================================================
    public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> binder)
    {
        return binder is null ? throw new ArgumentNullException(nameof(binder)) : HasValue ? binder(value) : Option<TResult>.None();
    }

    //==================================================================================================
    /// <summary>
    /// Returns a string representation of the option.
    /// </summary>
    /// <returns>A string indicating whether the option contains a value and what that value is.</returns>
    //==================================================================================================
    public override string ToString() => HasValue ? $"Some({value})" : "None";

    //==================================================================================================
    /// <summary>
    /// Determines whether the specified object is equal to the current option.
    /// </summary>
    /// <param name="obj">The object to compare with the current option.</param>
    /// <returns>true if the specified object is equal to the current option; otherwise, false.</returns>
    //==================================================================================================
    public override bool Equals(object? obj) => obj is Option<T> other && Equals(other);

    //==================================================================================================
    /// <summary>
    /// Determines whether the specified option is equal to the current option.
    /// </summary>
    /// <param name="other">The option to compare with the current option.</param>
    /// <returns>true if the specified option is equal to the current option; otherwise, false.</returns>
    //==================================================================================================
    public bool Equals(Option<T> other)
    {
        if (HasValue != other.HasValue)
        {
            return false;
        }

        return !HasValue || EqualityComparer<T>.Default.Equals(value, other.value);
    }

    //==================================================================================================
    /// <summary>
    /// Returns the hash code for this option.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    //==================================================================================================
    public override int GetHashCode()
    {
        return HasValue ? EqualityComparer<T>.Default.GetHashCode(value!) : 0;
    }

    //==================================================================================================
    /// <summary>
    /// Determines whether two options are equal.
    /// </summary>
    /// <param name="left">The first option to compare.</param>
    /// <param name="right">The second option to compare.</param>
    /// <returns>true if the options are equal; otherwise, false.</returns>
    //==================================================================================================
    public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);

    //==================================================================================================
    /// <summary>
    /// Determines whether two options are not equal.
    /// </summary>
    /// <param name="left">The first option to compare.</param>
    /// <param name="right">The second option to compare.</param>
    /// <returns>true if the options are not equal; otherwise, false.</returns>
    //==================================================================================================
    public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);

    #endregion
}
