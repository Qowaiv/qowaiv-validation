namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validates if the decorated item has a value that is specified in the forbidden values.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[CLSCompliant(false)]
public sealed class ForbiddenAttribute<TValue> : SetOfAttribute<TValue>
{
    /// <summary>Initializes a new instance of the <see cref="ForbiddenAttribute{TValue}"/> class.</summary>
    /// <param name="values">
    /// String representations of the forbidden values.
    /// </param>
    public ForbiddenAttribute(params object[] values)
        : base(values) => Do.Nothing();

    /// <summary>Return false if the value of <see cref="SetOfValuesAttribute.IsValid(object)"/>
    /// equals one of the values of the <see cref="ForbiddenAttribute{TValue}"/>.
    /// </summary>
    protected override bool OnEqual => false;
}
