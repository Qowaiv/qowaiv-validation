using FluentValidation;
using FluentValidation.Results;
using Qowaiv.Validation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Qowaiv.Validation.Fluent
{
    /// <summary>Represents a <see cref="ValidationFailure"/> as a <see cref="IValidationMessage"/>.</summary>
    [Serializable]
    public class ValidationMessage : ValidationFailure, IValidationMessage
    {
        /// <summary>Initializes a new instance of the <see cref="ValidationMessage"/> class.</summary>
        protected ValidationMessage(string propertyName, string errorMessage)
            : base(propertyName, errorMessage) => Do.Nothing();

        /// <inheritdoc />
        // This can be achieved by changing the public accessible Severity property.
        ValidationSeverity IValidationMessage.Severity => Severity.ToValidationSeverity();

        /// <inheritdoc />
        public string Message
        {
            get => ErrorMessage;
            set => ErrorMessage = value;
        }

        /// <summary>Gets a collection of <see cref="ValidationMessage"/>s
        /// based on a collection of <see cref="ValidationFailure"/>s.
        /// </summary>
        public static IEnumerable<ValidationMessage> For(IEnumerable<ValidationFailure> messages)
            => Guard.NotNull(messages, nameof(messages)).Select(message => For(message));

        /// <summary>Gets a <see cref="ValidationMessage"/> based on a <see cref="ValidationFailure"/>.</summary>
        public static ValidationMessage For(ValidationFailure failure)
            => Guard.NotNull(failure, nameof(failure)) is ValidationMessage message
            ? message
            : new(failure.PropertyName, failure.ErrorMessage)
            {
                AttemptedValue = failure.AttemptedValue,
                CustomState = failure.CustomState,
                ErrorCode = failure.ErrorCode,
                FormattedMessagePlaceholderValues = failure.FormattedMessagePlaceholderValues,
                Severity = failure.Severity,
            };

        /// <summary>Creates an error message.</summary>
        public static ValidationMessage Error(string message, string propertyName)
            => new(propertyName, message) { Severity = Severity.Error };

        /// <summary>Creates a warning message.</summary>
        public static ValidationMessage Warn(string message, string propertyName)
            => new(propertyName, message) { Severity = Severity.Warning };

        /// <summary>Creates an info message.</summary>
        public static ValidationMessage Info(string message, string propertyName)
            => new(propertyName, message) { Severity = Severity.Info };
    }
}
