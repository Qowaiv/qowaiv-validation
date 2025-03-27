namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that the value can not be in the future.</summary>
[AttributeUsage(AttributeTarget.Member, AllowMultiple = false)]
[Validates(typeof(DateTime))]
[Validates(typeof(DateTimeOffset))]
[Validates(typeof(Date))]
#if NET8_0_OR_GREATER
[Validates(typeof(DateOnly))]
#endif
[Validates(typeof(Year))]
public sealed class NotInFutureAttribute() : ClockAttribute(() => QowaivValidationMessages.NotInFuture)
{
    /// <inheritdoc />
    [Pure]
    protected override bool ValidateCompare(int compare) => compare <= 0;
}
