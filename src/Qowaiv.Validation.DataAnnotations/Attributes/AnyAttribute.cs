namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that a field should at least have one item in its collection.</summary>
[AttributeUsage(AttributeTarget.Member, AllowMultiple = false)]
[Validates(typeof(IEnumerable))]
public class AnyAttribute : RequiredAttribute
{
    /// <summary>Initializes a new instance of the <see cref="AnyAttribute"/> class.</summary>
    public AnyAttribute()
    {
        ErrorMessageResourceType = typeof(QowaivValidationMessages);
        ErrorMessageResourceName = nameof(QowaivValidationMessages.MandatoryAttribute_ValidationError);
    }

    /// <summary>Returns true if the value is not null and the collection
    /// has any item, otherwise false.
    /// </summary>
    [Pure]
    public override bool IsValid(object? value) => this.Validates(()
        => value is IEnumerable enumerable
        ? enumerable.GetEnumerator().MoveNext()
        : base.IsValid(value));
}
