namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Base <see cref="ValidationAttribute"/> for allowing or forbidding a set of values.</summary>
/// <typeparam name="TValue">
/// The type of the value.
/// </typeparam>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[CLSCompliant(false)]
public abstract class SetOfAttribute<TValue> : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="SetOfAttribute{TValue}"/> class.</summary>
    /// <param name="values">
    /// String representations of the values.
    /// </param>
    protected SetOfAttribute(params object[] values)
        : base(() => QowaivValidationMessages.AllowedValuesAttribute_ValidationError)
    {
        var converter = TypeDescriptor.GetConverter(typeof(TValue));
        Values = new HashSet<TValue>(values.Select(converter.ConvertFrom).OfType<TValue>());
    }

    /// <summary>The result to return when the value of <see cref="IsValid(object)"/>
    /// equals one of the values of the <see cref="SetOfValuesAttribute"/>.
    /// </summary>
    protected abstract bool OnEqual { get; }

    /// <summary>Gets the values.</summary>
    public IReadOnlyCollection<TValue> Values { get; }

    /// <summary>Returns true if the value is allowed.</summary>
    [Pure]
    public sealed override bool IsValid(object? value)
        => value is null
        || OnEqual == Values.Contains((TValue)value);
}
