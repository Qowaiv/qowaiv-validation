using Qowaiv.Validation.Abstractions;

namespace FluentAssertions.Qowaiv.Validation
{
    public sealed class ResultValidnessAssertions : ResultValidnessAssertionsBase<Result>
    {
        internal ResultValidnessAssertions(Result subject) : base(subject) { }

        public void WithoutMessages() => ExecuteWithoutMessages();
        public void WithMessage(IValidationMessage message) => ExecuteWithMessage(message);
        public void WithMessages(params IValidationMessage[] messages)
            => ExecuteWithMessages(messages);
    }
}
