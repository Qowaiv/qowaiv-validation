using Qowaiv.Validation.DataAnnotations.Reflection;

namespace System.ComponentModel.DataAnnotations;

/// <summary>Extensions on <see cref="ValidationContext"/>.</summary>
public static class ValidationContextExtensions
{
    /// <summary>Returns the service that provides custom validation.</summary>
    [Pure]
    public static T? GetSevice<T>(this ValidationContext validationContext)
        => (T?)Guard.NotNull(validationContext).GetService(typeof(T));

    /// <summary>
    /// Looks up the display name using the DisplayAttribute attached to the respective type or property.
    /// </summary>
    /// <returns>
    /// A display-friendly name of the member represented by the <see cref="ValidationContext.MemberName"/>.
    /// </returns>
    /// <remarks>
    /// The logic of this method is essential, but not accessible otherwise, as it is private.
    /// </remarks>
    [Pure]
    internal static string GetDisplayName(this ValidationContext validationContext)
        => (string)NonPublic.ValidationContext.GetDisplayName.Invoke(validationContext, [])!;

    /// <summary>Gets the <see cref="ValidationContext.MemberName"/> as an array.</summary>
    [Pure]
    internal static string[] MemberNames(this ValidationContext? validationContext)
        => validationContext?.MemberName is { }
        ? [validationContext.MemberName]
        : [];
}
