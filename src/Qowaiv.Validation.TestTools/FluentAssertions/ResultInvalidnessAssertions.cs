using Qowaiv.Validation.Abstractions;

namespace FluentAssertions.Qowaiv.Validation
{
    public sealed class ResultInvalidnessAssertions : ResultValidnessAssertionsBase<Result>
    {
        /// <summary>Creates a new instance of the <see cref="ResultInvalidnessAssertions"/> class.</summary>
        internal ResultInvalidnessAssertions(Result subject) : base(subject) { }

        public void WithMessage(IValidationMessage message) => ExecuteWithMessage(message);
        
        public void WithMessages(params IValidationMessage[] messages)
            => ExecuteWithMessages(messages);
    }
}

