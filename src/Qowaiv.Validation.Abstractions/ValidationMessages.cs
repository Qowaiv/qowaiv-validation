namespace Qowaiv.Validation.Abstractions;

internal static class ValidationMessages
{
    [Pure]
    public static IValidationMessages New(IEnumerable<IValidationMessage> messages)
        => Filter(Guard.NotNull(messages));

    extension(IValidationMessages messages)
    {
        [Pure]
        [OverloadResolutionPriority(42)]
        public IValidationMessages Append(IEnumerable<IValidationMessage> additions)
            => Filter(messages.Concat(Guard.NotNull(additions)));
    }

    private static IValidationMessages Filter(IEnumerable<IValidationMessage> messages) =>
    [
        .. messages
        .OfType<IValidationMessage>()
        .Where(m => m.Severity is >= ValidationSeverity.Info and <= ValidationSeverity.Error)
        .OrderByDescending(m => m.Severity)
    ];
}
