using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validates if the decorated enum has a value that is defined.</summary>
/// <typeparam name="TEnum">
/// The type of the enum.
/// </typeparam>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class DefinedOnlyAttribute<TEnum> : ValidationAttribute
    where TEnum : struct, Enum
{
    /// <summary>Initializes a new instance of the <see cref="DefinedOnlyAttribute{TEnum}"/> class.</summary>
    public DefinedOnlyAttribute()
        : base(() => QowaivValidationMessages.AllowedValuesAttribute_ValidationError) => Do.Nothing();

    /// <summary>If true, combinations of defined single values, that are not defined explicitly themselves, are allowed for flag enums.</summary>
    /// <remarks>
    /// When enabled, the logic falls back on <see cref="Enum.IsDefined(Type, object)"/>,
    /// because that is stricter than our implementation.
    /// </remarks>
    public bool OnlyAllowDefinedFlagsCombinations { get; init; }

    /// <summary>Returns true if the value is null or is defined for the enum, otherwise false.</summary>
    /// <exception cref="InvalidCastException">
    /// If the type of the value is not an enum.
    /// </exception>
    [Pure]
    public override bool IsValid(object? value)
    {
        // Might be a nullable enum, we just don't know.
        if (value is null)
        {
            return true;
        }
        else
        {
            var val = (TEnum)value;

            if (!OnlyAllowDefinedFlagsCombinations && AllFlags is { } all)
            {
                return all.HasFlag(val);
            }
            else
            {
                return Enum.IsDefined(typeof(TEnum), val);
            }
        }
    }

    private static readonly TEnum? AllFlags = GetAllFlags();

    [Pure]
    private static TEnum? GetAllFlags()
        => typeof(TEnum).GetCustomAttributes<FlagsAttribute>().Any()
        ? Enum.GetValues(typeof(TEnum))
            .Cast<dynamic>()
            .Aggregate((e1, e2) => e1 | e2)
        : null;
}
