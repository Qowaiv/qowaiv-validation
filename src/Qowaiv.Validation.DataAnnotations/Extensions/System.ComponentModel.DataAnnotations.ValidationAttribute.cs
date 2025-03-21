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
    public static ValidationMessage GetValidationMessage(this ValidationAttribute attribute, object? value, ValidationContext validationContext)
    {
        Guard.NotNull(attribute);

        var result = NonPublic.ValidationAttribute.IsValid.Invoke(attribute, [value, validationContext])!;
        return ValidationMessage.For((ValidationResult)result);
    }
}
