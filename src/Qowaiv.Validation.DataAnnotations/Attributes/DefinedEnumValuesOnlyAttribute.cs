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
        else
        {
            var enumType = value.GetType();

            if (!OnlyAllowDefinedFlagsCombinations && enumType.IsEnum && enumType.GetCustomAttributes<FlagsAttribute>().Any())
            {
                var defined = Enum.GetValues(enumType);
                if (defined.Length == 0) return false;

                dynamic max = defined.GetValue(0)!;
                for (var i = 1; i < defined.Length; i++)
                {
                    max |= (dynamic)defined.GetValue(i)!;
                }

                dynamic dyn = value;
                return (max & dyn) == dyn;
            }
            return Enum.IsDefined(enumType, value);
        }
    }
}
