namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validates if the decorated item has a value that is specified in the allowed values.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[CLSCompliant(false)]
public sealed class AllowedValuesAttribute : SetOfValuesAttribute
{
    /// <summary>Initializes a new instance of the <see cref="AllowedValuesAttribute"/> class.</summary>
    /// <param name="values">
    /// String representations of the allowed values.
    /// </param>
    public AllowedValuesAttribute(params string[] values)
        : base(values) => Do.Nothing();

    /// <summary>Return true the value of <see cref="SetOfValuesAttribute.IsValid(object)"/>
    /// equals one of the values of the <see cref="AllowedValuesAttribute"/>.
    /// </summary>
    protected override bool OnEqual => true;
}
