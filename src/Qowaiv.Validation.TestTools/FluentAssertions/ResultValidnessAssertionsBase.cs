namespace FluentAssertions.Qowaiv.Validation;

/// <summary>Contains a number of methods to assert the state of a <see cref="Result"/>.</summary>
public class ResultValidnessAssertionsBase<TSubject>
    where TSubject : Result
{
    /// <summary>Initializes a new instance of the <see cref="ResultValidnessAssertionsBase{TSubject}"/> class.</summary>
    protected ResultValidnessAssertionsBase(TSubject? subject) => Subject = subject;

    /// <summary>Gets the object which value is being asserted.</summary>
    protected TSubject? Subject { get; }

    /// <summary>Gets the <see cref="Result.Messages"/>.</summary>
    protected IEnumerable<IValidationMessage> Messages => Subject?.Messages ?? Array.Empty<IValidationMessage>();

    internal void ExecuteWithoutMessages()
        => Execute.Assertion
        .ForCondition(!Messages.Any())
        .WithDefaultIdentifier()
        .FailWith(WithoutMessages(Messages));

    internal void ExecuteWithMessage(IValidationMessage message)
        => Execute.Assertion
        .ForCondition(Messages.Count() == 1 && Comparer().Equals(Messages.Single(), message))
        .WithDefaultIdentifier()
        .FailWith(WithMessage(message, Messages.ToArray()));

    internal void ExecuteWithMessages(IValidationMessage[] messages)
    {
        var missing = messages.Except(Messages, Comparer()).ToArray();
        var extra = Messages.Except(messages, Comparer()).ToArray();

        Execute.Assertion
            .ForCondition(missing.Length == 0 && extra.Length == 0)
            .WithDefaultIdentifier()
            .FailWith(Messages.Any()
                ? WithMessages(missing, extra)
                : "Expected messages, but found none.");
    }

    [Pure]
    internal static string WithoutMessages(IEnumerable<IValidationMessage> messages)
        => new StringBuilder()
            .Append("Expected no messages, but found:")
            .AppendMessages(messages)
            .ToString();

    [Pure]
    private static string WithMessage(IValidationMessage expected, IValidationMessage[] actuals)
    {
        if (actuals.Length == 0) return "Expected a message, but found none.";
        else if (actuals.Length == 1)
        {
            return new StringBuilder()
                .AppendLine("Expected:")
                .AppendMessage(expected)
                .AppendLine()
                .AppendLine("Actual:")
                .AppendMessage(actuals[0])
                .ToString();
        }
        else
        {
            var all = new[] { expected };
            return WithMessages(all.Except(actuals, Comparer()).ToArray(), actuals.Except(all, Comparer()).ToArray());
        }
    }

    [Pure]
    private static IEqualityComparer<IValidationMessage> Comparer() => ValidationMessageCompare.ByInterface;

    [Pure]
    private static string WithMessages(IValidationMessage[] missing, IValidationMessage[] extra)
    {
        var sb = new StringBuilder();
        if (missing.Length > 0)
        {
            sb.Append($"Missing message{(missing.Length == 1 ? string.Empty : "s")}:")
                .AppendMessages(missing);
        }
        if (extra.Length > 0)
        {
            sb.Append($"Extra message{(extra.Length == 1 ? string.Empty : "s")}:")
                .AppendMessages(extra);
        }
        return sb.ToString();
    }
}
