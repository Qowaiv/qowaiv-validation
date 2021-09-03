using Qowaiv.Validation.Abstractions;

namespace FluentAssertions.Qowaiv.Validation
{
    /// <summary>Contains a number of methods to assert the messages of an invalid <see cref="Result"/>.</summary>
    public sealed class ResultInvalidnessAssertions : ResultValidnessAssertionsBase<Result>
    {
        /// <summary>Creates a new instance of the <see cref="ResultInvalidnessAssertions"/> class.</summary>
        internal ResultInvalidnessAssertions(Result subject) : base(subject) { }

        /// <summary>Asserts that the <see cref="Result"/> contains the specified message.</summary>
        public void WithMessage(IValidationMessage message) => ExecuteWithMessage(message);

        /// <summary>Asserts that the <see cref="Result"/> contains the specified messages.</summary>
        public void WithMessages(params IValidationMessage[] messages)
            => ExecuteWithMessages(messages);
    }
}

