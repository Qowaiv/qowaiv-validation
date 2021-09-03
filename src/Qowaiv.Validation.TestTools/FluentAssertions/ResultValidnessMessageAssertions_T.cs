using Qowaiv.Validation.Abstractions;

namespace FluentAssertions.Qowaiv.Validation
{
    public sealed class ResultValidnessMessageAssertions<TModel> : ResultValidnessAssertions<TModel>
    {
        internal ResultValidnessMessageAssertions(Result<TModel> subject) : base(subject) { }

        public ResultValidnessAssertions<TModel> WithoutMessages()
        {
            ExecuteWithoutMessages();
            return new ResultValidnessAssertions<TModel>(Subject);
        }
        public ResultValidnessAssertions<TModel> WithMessage(IValidationMessage message)
        {
            ExecuteWithMessage(message);
            return new ResultValidnessAssertions<TModel>(Subject);
        }
        public ResultValidnessAssertions<TModel> WithMessages(params IValidationMessage[] messages)
        {
            ExecuteWithMessages(messages);
            return new ResultValidnessAssertions<TModel>(Subject);
        }
    }
}
