namespace Qowaiv.Validation.DataAnnotations;

internal sealed class EmptyProvider : IServiceProvider
{
    public static readonly EmptyProvider Instance = new();

    [Pure]
    public object? GetService(Type serviceType) => null;
}
