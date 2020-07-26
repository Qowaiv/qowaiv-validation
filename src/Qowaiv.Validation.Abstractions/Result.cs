using Qowaiv.Validation.Abstractions.Internals;
using System.Collections.Generic;
using System.Linq;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Represents a result of a validation, executed command, etcetera.</summary>
    public class Result
    {
        /// <summary>Creates a new instance of a <see cref="Result"/>.</summary>
        /// <param name="messages">
        /// The messages related to the result.
        /// </param>
        internal Result(FixedMessages messages)
        {
            Messages = Guard.NotNull(messages, nameof(messages));
        }

        /// <summary>Gets the messages related to the result.</summary>
        public IReadOnlyList<IValidationMessage> Messages { get; }

        /// <summary>Return true if there are no error messages, otherwise false.</summary>
        public bool IsValid => !Errors.Any();

        /// <summary>Gets all messages with <see cref="ValidationSeverity.Error"/>.</summary>
        public IEnumerable<IValidationMessage> Errors => Messages.GetErrors();

        /// <summary>Gets all messages with <see cref="ValidationSeverity.Warning"/>.</summary>
        public IEnumerable<IValidationMessage> Warnings => Messages.GetWarnings();

        /// <summary>Gets all messages with <see cref="ValidationSeverity.Info"/>.</summary>
        public IEnumerable<IValidationMessage> Infos => Messages.GetInfos();

        /// <summary>Creates an OK <see cref="Result"/>.</summary>
        public static Result OK => new Result(FixedMessages.Empty);

        /// <summary>Creates a <see cref="Result{T}"/> for the data.</summary>
        public static Result<T> For<T>(T data, IEnumerable<IValidationMessage> messages)
            => new Result<T>(data, FixedMessages.New(messages));

        /// <summary>Creates a <see cref="Result{T}"/> for the data.</summary>
        public static Result<T> For<T>(T data, params IValidationMessage[] messages)
            => new Result<T>(data, FixedMessages.New(messages));

        /// <summary>Creates a result with messages.</summary>
        public static Result WithMessages(IEnumerable<IValidationMessage> messages)
            => new Result(FixedMessages.New(messages));

        /// <summary>Creates a result with messages.</summary>
        public static Result WithMessages(params IValidationMessage[] messages) 
            => new Result(FixedMessages.New(messages));

        /// <summary>Creates a result with messages.</summary>
        public static Result<T> WithMessages<T>(IEnumerable<IValidationMessage> messages) 
            => new Result<T>(default, FixedMessages.New(messages));

        /// <summary>Creates a result with messages.</summary>
        public static Result<T> WithMessages<T>(params IValidationMessage[] messages)
            => new Result<T>(default, FixedMessages.New(messages));

        internal static bool IsNullValueOrInvalid<T>(Result<T> result) 
            => !result.IsValid || ReferenceEquals(null, result._value);
    }
}
