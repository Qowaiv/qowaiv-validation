namespace System.ComponentModel.DataAnnotations;

/// <summary>Extensions on <see cref="ValidationContext"/>.</summary>
public static class ValidationContextExtensions
{
    /// <summary>Returns the service that provides custom validation.</summary>
    [Pure]
    public static T? GetSevice<T>(this ValidationContext validationContext)
        => (T?)Guard.NotNull(validationContext, nameof(validationContext)).GetService(typeof(T));
}
