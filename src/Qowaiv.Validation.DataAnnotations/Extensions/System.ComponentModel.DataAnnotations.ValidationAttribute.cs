using System.Reflection;

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
        Guard.NotNull(attribute, nameof(attribute));

        var result = IsValid.Invoke(attribute, new object?[] { value, validationContext })!;
        return ValidationMessage.For((ValidationResult)result);
    }

    /// <summary>Access to the protected <see cref="ValidationAttribute.IsValid(object, ValidationContext)"/>.</summary>
    private static readonly MethodInfo IsValid = typeof(ValidationAttribute).GetMethod(nameof(IsValid), IsValidBindings)!;

#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
    // To access the protected <see cref="ValidationAttribute.IsValid(object, ValidationContext)"/>
    // We - unfortunately - have to use reflection.
    private const BindingFlags IsValidBindings = BindingFlags.Instance | BindingFlags.NonPublic;
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
}
