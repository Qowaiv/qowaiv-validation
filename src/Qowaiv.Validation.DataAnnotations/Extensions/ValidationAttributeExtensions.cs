using Qowaiv;
using Qowaiv.Validation.DataAnnotations;
using System.Reflection;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>Extensions on <see cref="ValidationAttribute"/>.</summary>
    public static class ValidationAttributeExtensions
    {
        /// <summary>Replacement of <see cref="ValidationAttribute.GetValidationResult(object, ValidationContext)"/>.</summary>
        /// <remarks>
        /// The original function messes up with <see cref="ValidationMessage.None"/>, as it has no error message.
        /// </remarks>
        public static ValidationMessage GetValidationMessage(this ValidationAttribute attribute, object value, ValidationContext validationContext)
        {
            Guard.NotNull(attribute, nameof(attribute));

            var result = (ValidationResult)IsValid.Invoke(attribute, new object[] { value, validationContext });
            return ValidationMessage.For(result);
        }

        /// <summary>Access to the protected <see cref="ValidationAttribute.IsValid(object, ValidationContext)"/>.</summary>
        private static readonly MethodInfo IsValid = typeof(ValidationAttribute).GetMethod(nameof(IsValid), BindingFlags.Instance | BindingFlags.NonPublic);
    }
}
