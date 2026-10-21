using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validates if the decorated enum has a value that is a defined.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[Validates(typeof(Enum))]
public class DefinedEnumValuesOnlyAttribute()
    : ValidationAttribute(() => QowaivValidationMessages.AllowedValuesAttribute_ValidationError)
{
    /// <summary>If true, only explicitly defined values of flag enums are allowed; combinations of defined values that are not defined themselves are rejected.</summary>
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
        else if (value.GetType() is { IsEnum: true } enumType)
        {
            if (OnlyAllowDefinedFlagsCombinations || !enumType.GetCustomAttributes<FlagsAttribute>().Any())
            {
                return Enum.IsDefined(enumType, value);
            }
            else
            {
                dynamic dyn = value;
                return (Mask(enumType) & dyn) == dyn;
            }
        }
        else throw UnsupportedType.ForAttribute<DefinedEnumValuesOnlyAttribute>(value.GetType());
    }

    [Pure]
    private static dynamic Mask(Type enumType)
    {
        if (!Flags.TryGetValue(enumType, out var mask))
        {
            var defined = Enum.GetValues(enumType);
            mask = defined.GetValue(0)!;

            for (var i = 1; i < defined.Length; i++)
            {
                mask |= (dynamic)defined.GetValue(i)!;
            }

            Flags[enumType] = mask;
        }

        return mask;
    }

    private static readonly ConcurrentDictionary<Type, dynamic> Flags = [];
}
