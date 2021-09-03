using FluentAssertions.Primitives;
using Qowaiv.Validation.Abstractions;

namespace FluentAssertions.Qowaiv.Validation
{
    public class ResultValidnessAssertions<TModel> : ResultValidnessAssertionsBase<Result<TModel>>
    {
        internal ResultValidnessAssertions(Result<TModel> subject) : base(subject) { }

        public ObjectAssertions WithValue() => new(Subject.Value);
    }
}
