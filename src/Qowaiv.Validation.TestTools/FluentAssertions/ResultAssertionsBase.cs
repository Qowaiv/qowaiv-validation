﻿using FluentAssertions.Execution;
using Qowaiv.Validation.Abstractions;
using System.Linq;
using System.Text;

namespace FluentAssertions.Qowaiv.Validation
{
    /// <summary>Contains a number of methods to assert the (in)validness of a <see cref="Result"/>.</summary>
    public abstract class ResultAssertionsBase<TSubject>
        where TSubject : Result
    {
        /// <summary>Creates a new instance of the <see cref="ResultAssertionsBase{TSubject}"/> class.</summary>
        protected ResultAssertionsBase(TSubject subject) => Subject = subject;

        /// <summary>Gets the object which validness is being asserted.</summary>
        public TSubject Subject { get; }

        /// <summary>Asserts thats <typeparamref name="TSubject"/> is invalid.</summary>
        [CustomAssertion]
        public ResultInvalidnessAssertions BeInvalid(string because = "", params object[] becauseArgs)
        {
            if (SubjectExists())
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(!Subject.IsValid)
                    .WithDefaultIdentifier()
                    .FailWith(Subject.Messages.Any()
                    ? Text("Actual {context} is not invalid{reason}:").AppendMessages(Subject.Messages).ToString()
                : "Actual {context} is not invalid{reason}.");
            }
            return new ResultInvalidnessAssertions(Subject);
        }

        internal void ExecuteBeValid(string because, object[] becauseArgs)
        {
            if (SubjectExists())
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(Subject.IsValid)
                    .WithDefaultIdentifier()
                    .FailWith(Subject.Messages.Any()
                    ? Text("Actual {context} is not valid{reason}:").AppendMessages(Subject.Messages).ToString()
                    : "Actual {context} is not invalid{reason}.");
            }
        }

        private static StringBuilder Text(string text) => new(text);

        private bool SubjectExists()
            => Execute.Assertion
              .WithDefaultIdentifier()
              .ForCondition(Subject is not null)
              .FailWith("{context} is <null>.");

    }
}
