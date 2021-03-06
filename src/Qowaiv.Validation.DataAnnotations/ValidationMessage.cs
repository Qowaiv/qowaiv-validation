﻿using Qowaiv.Validation.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Qowaiv.Validation.DataAnnotations
{
    /// <summary>Represents a <see cref="ValidationResult"/> as a <see cref="IValidationMessage"/>.</summary>
    [Serializable]
    public class ValidationMessage : ValidationResult, IValidationMessage, ISerializable
    {
        /// <summary>Initializes a new instance of the <see cref="ValidationMessage"/> class.</summary>
        public ValidationMessage() : this(ValidationSeverity.None, null, null) => Do.Nothing();

        internal ValidationMessage(ValidationSeverity severity, string message, string[] memberNames)
            : base(message, memberNames)
        {
            Severity = severity;
        }

        /// <summary>Initializes a new instance of the <see cref="ValidationMessage"/> class.</summary>
        protected ValidationMessage(SerializationInfo info, StreamingContext context)
            : this(GetSeverity(info), GetMessage(info), GetMemberNames(info)) => Do.Nothing();

        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static ValidationSeverity GetSeverity(SerializationInfo info) => (ValidationSeverity)info.GetInt32(nameof(Severity));

        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static string GetMessage(SerializationInfo info) => info.GetString(nameof(Message));

        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static string[] GetMemberNames(SerializationInfo info) => info.GetValue(nameof(MemberNames), typeof(string[])) as string[];

        /// <inheritdoc />
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Guard.NotNull(info, nameof(info));

            info.AddValue(nameof(Severity), Severity);
            info.AddValue(nameof(Message), Message);
            info.AddValue(nameof(MemberNames), MemberNames.ToArray());
        }

        /// <inheritdoc />
        public ValidationSeverity Severity { get; }

        /// <inheritdoc />
        public string PropertyName => MemberNames.FirstOrDefault();

        /// <inheritdoc />
        public string Message => ErrorMessage;

        /// <summary>Creates a None message.</summary>
        public static ValidationMessage None => new ValidationMessage();

        /// <summary>Creates an error message.</summary>
        public static ValidationMessage Error(string message, params string[] memberNames) => new ValidationMessage(ValidationSeverity.Error, message, memberNames);

        /// <summary>Creates a warning message.</summary>
        public static ValidationMessage Warn(string message, params string[] memberNames) => new ValidationMessage(ValidationSeverity.Warning, message, memberNames);

        /// <summary>Creates an info message.</summary>
        public static ValidationMessage Info(string message, params string[] memberNames) => new ValidationMessage(ValidationSeverity.Info, message, memberNames);

        /// <summary>Creates a validation message for a validation result.</summary>
        /// <param name="validationResult">
        /// The validation result to convert.
        /// </param>
        public static ValidationMessage For(ValidationResult validationResult)
        {
            if (validationResult is null || validationResult == Success)
            {
                return None;
            }
            if (validationResult is ValidationMessage message)
            {
                return message;
            }
            return Error(validationResult.ErrorMessage, validationResult.MemberNames.ToArray());
        }

        /// <summary>Creates a validation message.</summary>
        public static IValidationMessage For(ValidationSeverity severity, string message, string[] memberNames)
        {
            switch (severity)
            {
                case ValidationSeverity.None: return None;
                case ValidationSeverity.Info: return Info(message, memberNames);
                case ValidationSeverity.Warning: return Warn(message, memberNames);
                case ValidationSeverity.Error:
                default: return Error(message, memberNames);
            }
        }
    }
}
