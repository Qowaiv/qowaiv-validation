using Qowaiv.Validation.Abstractions.Internals;
using System.Collections.Generic;
using System.Linq;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Represents a result of a validation, executed command, etcetera.</summary>
    public class Result
    {
        /// <summary>Initializes a new instance of the <see cref="Result"/> class.</summary>
        /// <param name="messages">
        /// The messages related to the result.
        /// </param>
        internal Result(FixedMessages messages)
            => Messages = Guard.NotNull(messages, nameof(messages));

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

        /// <summary>Represents an OK <see cref="Result"/>.</summary>
        public static readonly Result OK = new(FixedMessages.Empty);

        /// <summary>Creates a valid null <see cref="Result{T}"/>.</summary>
        public static Result<T> Null<T>(IEnumerable<IValidationMessage> messages) where T : class
            => new(FixedMessages.New(messages));

        /// <summary>Creates a valid null <see cref="Result{T}"/>.</summary>
        public static Result<T> Null<T>(params IValidationMessage[] messages) where T : class
            => new(FixedMessages.New(messages));

        /// <summary>Creates a <see cref="Result{T}"/> for the data.</summary>
        public static Result<T> For<T>(T data, IEnumerable<IValidationMessage> messages)
            => new(data, FixedMessages.New(messages));

        /// <summary>Creates a <see cref="Result{T}"/> for the data.</summary>
        public static Result<T> For<T>(T data, params IValidationMessage[] messages)
            => new(data, FixedMessages.New(messages));

        /// <summary>Creates a result with messages.</summary>
        public static Result WithMessages(IEnumerable<IValidationMessage> messages)
            => new(FixedMessages.New(messages));

        /// <summary>Creates a result with messages.</summary>
        public static Result WithMessages(params IValidationMessage[] messages)
            => new(FixedMessages.New(messages));

        /// <summary>Creates a result with messages.</summary>
        public static Result<T> WithMessages<T>(IEnumerable<IValidationMessage> messages)
            => new(default, FixedMessages.New(messages));

        /// <summary>Creates a result with messages.</summary>
        public static Result<T> WithMessages<T>(params IValidationMessage[] messages)
            => new(default, FixedMessages.New(messages));
    }
}
