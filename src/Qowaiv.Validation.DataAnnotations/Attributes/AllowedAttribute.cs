namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validates if the decorated item has a value that is specified in the allowed values.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[CLSCompliant(false)]
public sealed class AllowedAttribute<TValue> : SetOfAttribute<TValue>
{
    /// <summary>Initializes a new instance of the <see cref="AllowedAttribute{TValue}"/> class.</summary>
    /// <param name="values">
    /// Representations of the allowed values.
    /// </param>
    public AllowedAttribute(params object[] values) : base(values) { }

    /// <summary>Return true the value of <see cref="SetOfAttribute{TValue}.IsValid(object)"/>
    /// equals one of the values of the <see cref="SetOfAttribute{TValue}" />.
    /// </summary>
    protected override bool OnEqual => true;
}
