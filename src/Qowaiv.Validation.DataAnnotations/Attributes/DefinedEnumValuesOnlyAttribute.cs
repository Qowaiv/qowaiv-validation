using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validates if the decorated enum has a value that is a defined.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[Obsolete("Use DefinedOnlyAttribute<TEnum> instead.")]
public class DefinedEnumValuesOnlyAttribute : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="DefinedEnumValuesOnlyAttribute"/> class.</summary>
    public DefinedEnumValuesOnlyAttribute()
        : base(() => QowaivValidationMessages.AllowedValuesAttribute_ValidationError) => Do.Nothing();

    /// <summary>If true, for flag enums, also combinations of defined single values are allowed, that are not defined themselves explicitly.</summary>
    /// <remarks>
    /// When enabled, the logic falls back on <see cref="Enum.IsDefined(Type, object)"/>, as
    /// that is stricter than, our implementation.
    /// </remarks>
    public bool OnlyAllowDefinedFlagsCombinations { get; set; }

    /// <summary>Returns true if the value is defined for the enum, otherwise false.</summary>
    /// <exception cref="ArgumentException">
    /// If the type of the value is not an enum.
    /// </exception>
    [Pure]
    public override bool IsValid(object? value)
    {
        // Might be a nullable enum, we just don't know.
        if (value is null) return true;
        else
        {
            var enumType = value.GetType();

            if (!OnlyAllowDefinedFlagsCombinations && enumType.IsEnum && enumType.GetCustomAttributes<FlagsAttribute>().Any())
            {
                dynamic dyn = value;
                var max = Enum.GetValues(enumType)
                    .Cast<dynamic>()
                    .Aggregate((e1, e2) => e1 | e2);

                return (max & dyn) == dyn;
            }
            return Enum.IsDefined(enumType, value);
        }
    }
}
