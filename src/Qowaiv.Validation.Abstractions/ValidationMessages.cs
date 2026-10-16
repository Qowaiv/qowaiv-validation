namespace Qowaiv.Validation.Abstractions;

internal static class ValidationMessages
{
    [Pure]
    public static IValidationMessages New(IEnumerable<IValidationMessage> messages)
        => [.. Guard.NotNull(messages).OfType<IValidationMessage>()];

    extension(IValidationMessages messagse)
    {
        [Pure]
        [OverloadResolutionPriority(42)]
        public IValidationMessages Append(IEnumerable<IValidationMessage> additions)
            => [.. messagse, .. New(additions)];
    }
}
