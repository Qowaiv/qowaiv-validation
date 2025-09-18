namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Base <see cref="ValidationAttribute"/> for allowing or forbidding a set of values.</summary>
/// <typeparam name="TValue">
/// The type of the value.
/// </typeparam>
[AttributeUsage(AttributeTarget.Member, AllowMultiple = false)]
[CLSCompliant(false)]
public abstract class SetOfAttribute<TValue> : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="SetOfAttribute{TValue}"/> class.</summary>
    /// <param name="values">
    /// Representations of the values.
    /// </param>
    protected SetOfAttribute(params object[] values)
        : base(() => QowaivValidationMessages.AllowedValuesAttribute_ValidationError)
    {
        var all = new HashSet<TValue>();

        var converter = TypeConverter is not null
            ? Guard.IsInstanceOf<TypeConverter>(Activator.CreateInstance(TypeConverter))
            : null;

        converter ??= TypeDescriptor.GetConverter(typeof(TValue));

        foreach (var value in values)
        {
            if (value is TValue typed)
            {
                all.Add(typed);
            }
            else if (converter.ConvertFrom(value) is TValue converted)
            {
                all.Add(converted);
            }
        }
        Values = all;
    }

    /// <summary>Specify a custom type converter to convert the values to convert.</summary>
    public Type? TypeConverter { get; init; }

    /// <summary>The result to return when the value of <see cref="IsValid(object)" />
    /// equals one of the values of the <see cref="SetOfAttribute{TValue}" />.
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
