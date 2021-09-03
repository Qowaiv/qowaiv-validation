using Qowaiv.Validation.Abstractions;

namespace FluentAssertions.Qowaiv.Validation
{
    /// <summary>Contains a number of methods to assert the (in)validness of a <see cref="Result"/>.</summary>
    public sealed class ResultAssertions : ResultAssertionsBase<Result>
    {
        /// <summary>Creates a new instance of the <see cref="ResultActExtensions"/> class.</summary>
        internal ResultAssertions(Result subject) : base(subject) { }

        /// <summary>Asserts thats <see cref="Result"/> is invalid.</summary>
        public ResultValidnessAssertions BeValid(string because = "", params object[] becauseArgs)
        {
            ExecuteBeValid(because, becauseArgs);
            return new ResultValidnessAssertions(Subject);
        }
    }
}
