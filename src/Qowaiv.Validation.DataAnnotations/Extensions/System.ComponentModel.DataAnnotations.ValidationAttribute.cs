using Qowaiv.Validation.DataAnnotations.Reflection;

namespace System.ComponentModel.DataAnnotations;

/// <summary>Extensions on <see cref="ValidationAttribute"/>.</summary>
public static class QowaivValidationAttributeExtensions
{
    /// <summary>Replacement of <see cref="ValidationAttribute.GetValidationResult(object, ValidationContext)"/>.</summary>
    /// <remarks>
    /// The original function messes up with <see cref="ValidationMessage.None"/>, as it has no error message.
    /// </remarks>
    [Pure]
    public static ValidationMessage? GetValidationMessage(this ValidationAttribute attribute, object? value, ValidationContext validationContext)
    {
        Guard.NotNull(attribute);

        var result = NonPublic.ValidationAttribute.IsValid.Invoke(attribute, [value, validationContext]);
        return ValidationMessage.For(result as ValidationResult);
    }

    /// <summary>Validates the attribute.</summary>
    /// <remarks>
    /// To support both custom error messages via <see cref="ValidationAttribute.ErrorMessage"/>
    /// and via the a custom resource, the latter should be reset when setting
    /// the first. However, when setting <see cref="ValidationAttribute.ErrorMessage"/>
    /// The <see cref="ValidationAttribute.ErrorMessageResourceName"/> is not
    /// reset, which would result in a runtime error. This construct is not
    /// ideal, but it is the only point where we can alter the default
    /// behavior.
    /// </remarks>
    [Pure]
    internal static bool Validates(this ValidationAttribute attr, Func<bool> isValid)
    {
        var result = isValid();

        if (!result && attr.ErrorMessage is { Length: > 0 })
        {
            attr.ErrorMessageResourceName = null;
            attr.ErrorMessageResourceType = null;
        }
        return result;
    }
}
