namespace FluentAssertions.Qowaiv.Validation;

/// <summary>Contains a number of methods to assert the messages of a valid <see cref="Result"/>.</summary>
public sealed class ResultValidnessAssertions : ResultValidnessAssertionsBase<Result>
{
    internal ResultValidnessAssertions(Result? subject) : base(subject) { }

    /// <summary>Asserts that the <see cref="Result"/> contains no messages.</summary>
    [CustomAssertion]
    public void WithoutMessages() => ExecuteWithoutMessages();

    /// <summary>Asserts that the <see cref="Result"/> contains the specified message.</summary>
    [CustomAssertion]
    public void WithMessage(IValidationMessage message) => ExecuteWithMessage(message);

    /// <summary>Asserts that the <see cref="Result"/> contains the specified messages.</summary>
    [CustomAssertion]
    public void WithMessages(params IValidationMessage[] messages)
        => ExecuteWithMessages(messages);
}
