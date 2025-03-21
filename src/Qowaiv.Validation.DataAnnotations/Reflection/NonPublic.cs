using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations.Reflection;

/// <summary>Contains non-public methods that we need to make this work.</summary>
internal static class NonPublic
{
#pragma warning disable S3011 // Reflection should not be used to increase accessibility.
    public const BindingFlags Instance = BindingFlags.Instance | BindingFlags.NonPublic;

    public static class ValidationAttribute
    {
        public static readonly MethodInfo IsValid = typeof(System.ComponentModel.DataAnnotations.ValidationAttribute)
            .GetMethod(nameof(IsValid), Instance)!;
    }

    public static class ValidationContext
    {
        public static readonly MethodInfo GetDisplayName = typeof(System.ComponentModel.DataAnnotations.ValidationContext)
        .GetMethod(nameof(GetDisplayName), Instance)!;
    }
}
