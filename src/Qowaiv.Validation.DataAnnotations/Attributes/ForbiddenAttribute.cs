namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validates if the decorated item has a value that is specified in the forbidden values.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[CLSCompliant(false)]
[Validates(GenericArgument = true)]
public sealed class ForbiddenAttribute<TValue> : SetOfAttribute<TValue>
{
    /// <summary>Initializes a new instance of the <see cref="ForbiddenAttribute{TValue}"/> class.</summary>
    /// <param name="values">
    /// Representations of the forbidden values.
    /// </param>
    public ForbiddenAttribute(params object[] values) : base(values) { }

    /// <summary>Return false if the value of <see cref="SetOfAttribute{TValue}.IsValid(object)" />
    /// equals one of the values of the <see cref="ForbiddenAttribute{TValue}" />.
    /// </summary>
    protected override bool OnEqual => false;
}
