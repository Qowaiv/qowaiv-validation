namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Base <see cref="ValidationAttribute"/> for allowing or forbidding a set of values.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[CLSCompliant(false)]
public abstract class SetOfValuesAttribute : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="SetOfValuesAttribute"/> class.</summary>
    /// <param name="values">
    /// String representations of the values.
    /// </param>
    protected SetOfValuesAttribute(params string[] values)
        : base(() => QowaivValidationMessages.AllowedValuesAttribute_ValidationError)
    {
        Values = Guard.NotNull(values, nameof(values));
    }

    /// <summary>The result to return when the value of <see cref="IsValid(object)"/>
    /// equals one of the values of the <see cref="SetOfValuesAttribute"/>.
    /// </summary>
    protected abstract bool OnEqual { get; }

    /// <summary>Gets the values.</summary>
    public string[] Values { get; }

    /// <summary>Returns true if the value occurs to be forbidden, otherwise false.</summary>
    [Pure]
    public sealed override bool IsValid(object? value)
    {
        if (value == null) return true;
        else
        {
            var converter = TypeDescriptor.GetConverter(value.GetType());
            return OnEqual == Values.Exists(val => value.Equals(converter.ConvertFromString(val)));
        }
    }
}
