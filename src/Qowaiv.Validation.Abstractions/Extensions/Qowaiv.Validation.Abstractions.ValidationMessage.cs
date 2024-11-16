namespace Qowaiv.Validation.Abstractions;

/// <summary>Extensions on <see cref="IValidationMessage"/>.</summary>
public static class ValidationMessageExtensions
{
    /// <summary>Gets all messages excluding <see cref="ValidationSeverity.None"/>.</summary>
    [Pure]
    public static IEnumerable<IValidationMessage> GetWithSeverity(this IEnumerable<IValidationMessage> validationResults)
        => Guard.NotNull(validationResults)
            .Where(message => message.Severity > ValidationSeverity.None);

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Error"/>.</summary>
    [Pure]
    public static IEnumerable<IValidationMessage> GetErrors(this IEnumerable<IValidationMessage> validationResults)
        => Guard.NotNull(validationResults)
            .Where(message => message.Severity == ValidationSeverity.Error);

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Warning"/>.</summary>
    [Pure]
    public static IEnumerable<IValidationMessage> GetWarnings(this IEnumerable<IValidationMessage> validationResults)
        => Guard.NotNull(validationResults)
            .Where(message => message.Severity == ValidationSeverity.Warning);

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Info"/>.</summary>
    [Pure]
    public static IEnumerable<IValidationMessage> GetInfos(this IEnumerable<IValidationMessage> validationResults)
        => Guard.NotNull(validationResults)
        .Where(message => message.Severity == ValidationSeverity.Info);
}
