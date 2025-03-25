using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validates if the decorated enum has a value that is defined.</summary>
/// <typeparam name="TEnum">
/// The type of the enum.
/// </typeparam>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[Validates(GenericArgument = true)]
public class DefinedOnlyAttribute<TEnum> : ValidationAttribute
    where TEnum : struct, Enum
{
    /// <summary>Initializes a new instance of the <see cref="DefinedOnlyAttribute{TEnum}"/> class.</summary>
    public DefinedOnlyAttribute()
        : base(() => QowaivValidationMessages.AllowedValuesAttribute_ValidationError) { }

    /// <summary>If false, combinations of defined single values, that are not defined explicitly themselves, are allowed for flag enums.</summary>
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
    public override bool IsValid(object? value) => value switch
    {
        null => true,
        TEnum val => !OnlyAllowDefinedFlagsCombinations && AllFlags is { } all
            ? all.HasFlag(val)
            : EnumIsDefined(val),
        _ => throw UnsupportedType.ForAttribute<DefinedOnlyAttribute<TEnum>>(value.GetType()),
    };

    private static readonly TEnum? AllFlags = GetAllFlags();

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TEnum? GetAllFlags()
        => typeof(TEnum).GetCustomAttributes<FlagsAttribute>().Any()
        ? GetEnumValues().Cast<dynamic>().Aggregate((e1, e2) => e1 | e2)
        : null;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool EnumIsDefined(TEnum val)
#if NETSTANDARD2_0
        => Enum.IsDefined(typeof(TEnum), val);
#else
        => Enum.IsDefined(val);
#endif

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TEnum[] GetEnumValues()
#if NETSTANDARD2_0
        => [.. Enum.GetValues(typeof(TEnum)).Cast<TEnum>()];
#else
        => Enum.GetValues<TEnum>();
#endif
}
