using Qowaiv.Financial;
using Qowaiv.Mathematics;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>'Validates if the item is not positive.</summary>
[AttributeUsage(AttributeTarget.Member, AllowMultiple = false)]
[Validates(typeof(sbyte))]
[Validates(typeof(short))]
[Validates(typeof(int))]
[Validates(typeof(long))]
[Validates(typeof(float))]
[Validates(typeof(double))]
[Validates(typeof(decimal))]
#if NET8_0_OR_GREATER
[Validates(typeof(Int128))]
#endif
[Validates(typeof(Amount))]
[Validates(typeof(Money))]
[Validates(typeof(Percentage))]
[Validates(typeof(Fraction))]
public sealed class NotPositiveAttribute() : SignAttribute(() => QowaivValidationMessages.NotPositiveAttribute_ValidationError)
{
    /// <inheritdoc />
    [Pure]
    protected override bool Is(int sign) => sign is not +1;
}
