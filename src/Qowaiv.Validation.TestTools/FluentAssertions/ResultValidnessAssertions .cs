using Qowaiv.Validation.Abstractions;

namespace FluentAssertions.Qowaiv.Validation
{
    /// <summary>Contains a number of methods to assert the messages of a valid <see cref="Result"/>.</summary>
    public sealed class ResultValidnessAssertions : ResultValidnessAssertionsBase<Result>
    {
        internal ResultValidnessAssertions(Result subject) : base(subject) { }

        /// <summary>Asserts that the <see cref="Result"/> contains no messages.</summary>
        public void WithoutMessages() => ExecuteWithoutMessages();

        /// <summary>Asserts that the <see cref="Result"/> contains the specified message.</summary>
        public void WithMessage(IValidationMessage message) => ExecuteWithMessage(message);

        /// <summary>Asserts that the <see cref="Result"/> contains the specified messages.</summary>
        public void WithMessages(params IValidationMessage[] messages)
            => ExecuteWithMessages(messages);
    }
}
