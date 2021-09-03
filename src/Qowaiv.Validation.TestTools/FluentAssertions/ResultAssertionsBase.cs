using FluentAssertions.Execution;
using Qowaiv.Validation.Abstractions;
using System.Linq;
using System.Text;

namespace FluentAssertions.Qowaiv.Validation
{
    /// <summary>Contains a number of methods to assert the (in)validness of a <see cref="Result"/>.</summary>
    public abstract class ResultAssertionsBase<TSubject>
        where TSubject : Result
    {
        /// <summary>Creates a new instance of the <see cref="ResultActExtensions"/> class.</summary>
        protected ResultAssertionsBase(TSubject subject) => Subject = subject;

        /// <summary>Gets the object which validness is being asserted.</summary>
        public TSubject Subject { get; }

        /// <summary>Asserts thats <typeparamref name="TSubject"/> is invalid.</summary>
        public ResultInvalidnessAssertions BeInvalid(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(!Subject.IsValid)
                .FailWith(Subject.Messages.Any()
                ? Text("Expected {context} is valid{reason}:").AppendMessages(Subject.Messages).ToString()
                : "Expected {context} is valid{reason}.");

            return new ResultInvalidnessAssertions(Subject);
        }

        protected void ExecuteBeValid(string because, object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.IsValid)
                .FailWith(Subject.Messages.Any()
                ? Text("Expected {context} is invalid{reason}:").AppendMessages(Subject.Messages).ToString()
                : "Expected {context} is invalid{reason}.");
        }

        private static StringBuilder Text(string text) => new(text);
    }
}
