using Qowaiv.Validation.Abstractions;

namespace FluentAssertions.Qowaiv.Validation
{
    /// <summary>Contains a number of methods to assert the (in)validness of a <see cref="Result{TModel}"/>.</summary>
    public sealed class ResultAssertions<TModel> : ResultAssertionsBase<Result<TModel>>
    {
        /// <summary>Creates a new instance of the <see cref="ResultActExtensions"/> class.</summary>
        internal ResultAssertions(Result<TModel> subject) : base(subject) { }

        /// <summary>Asserts thats <see cref="Result"/> is invalid.</summary>
        public ResultValidnessMessageAssertions<TModel> BeValid(string because = "", params object[] becauseArgs)
        {
            ExecuteBeValid(because, becauseArgs);
            return new ResultValidnessMessageAssertions<TModel>(Subject);
        }
    }
}
