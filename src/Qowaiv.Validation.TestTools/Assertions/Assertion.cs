namespace Qowaiv.Validation.TestTools;

internal sealed record Assertion
{
    public string? Because { get; init; }

    public object[] BecauseArgs { get; init; } = [];

    public string? Expression { get; init; }

    public string? Message { get; init; }

    [Pure]
    public Assertion BecauseOf(string because, object[] becauseArgs) => this with
    {
        Because = because,
        BecauseArgs = becauseArgs,
    };

    [Pure]
    public static Assertion For(string expression) => new()
    {
        Expression = expression,
    };

    [Pure]
    public Assertion WithMessage(string message) => this with
    {
        Message = message,
    };

    public void Ensure(bool condition)
    {
        if (!condition)
        {
            var error = (Message ?? string.Empty).Replace("{context}", Expression);

            if (Because is { Length: > 0 })
            {
                error = error.Replace("{reason}", " because " + string.Format(Because ?? string.Empty, BecauseArgs));
            }

            throw new AssertionFailed(error);
        }
    }
}
