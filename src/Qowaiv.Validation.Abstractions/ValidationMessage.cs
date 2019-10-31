using System;
using System.Runtime.Serialization;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Implementation of an <see cref="IValidationMessage"/>.</summary>
    [Serializable]
    public class ValidationMessage : IValidationMessage, ISerializable
    {
        /// <summary>Creates a new instance of a <see cref="ValidationMessage"/>.</summary>
        public ValidationMessage() : this(ValidationSeverity.None, null, null) { }

        internal ValidationMessage(ValidationSeverity serverity, string message, string propertyName)
        {
            Severity = serverity;
            Message = message;
            PropertyName = propertyName;
        }

        /// <summary>Creates a new instance of <see cref="ValidationMessage"/>.</summary>
        protected ValidationMessage(SerializationInfo info, StreamingContext context) :
            this(GetSeverity(info), GetMessage(info), GetProperty(info))
        { }

        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static ValidationSeverity GetSeverity(SerializationInfo info) => (ValidationSeverity)info.GetInt32(nameof(Severity));

        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static string GetMessage(SerializationInfo info) => info.GetString(nameof(Message));
    
        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static string GetProperty(SerializationInfo info) => info.GetString(nameof(PropertyName));

        /// <inheritdoc />
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Guard.NotNull(info, nameof(info));

            info.AddValue(nameof(Severity), Severity);
            info.AddValue(nameof(Message), Message);
            info.AddValue(nameof(PropertyName), PropertyName);
        }

        /// <inheritdoc />
        public ValidationSeverity Severity { get; }

        /// <inheritdoc />
        public string PropertyName { get; }

        /// <inheritdoc />
        public string Message { get; }

        /// <summary>Creates a None message.</summary>
        public static ValidationMessage None => new ValidationMessage();

        /// <summary>Creates an error message.</summary>
        public static ValidationMessage Error(string message, string propertyName = null) => new ValidationMessage(ValidationSeverity.Error, message, propertyName);

        /// <summary>Creates a warning message.</summary>
        public static ValidationMessage Warn(string message, string propertyName = null) => new ValidationMessage(ValidationSeverity.Warning, message, propertyName);

        /// <summary>Creates an info message.</summary>
        public static ValidationMessage Info(string message, string propertyName = null) => new ValidationMessage(ValidationSeverity.Error, message, propertyName);

        /// <summary>Creates a validation message.</summary>
        public static IValidationMessage For(ValidationSeverity serverity, string message, string propertyName = null)
        {
            switch (serverity)
            {
                case ValidationSeverity.None: return None;
                case ValidationSeverity.Info: return Info(message, propertyName);
                case ValidationSeverity.Warning: return Warn(message, propertyName);
                case ValidationSeverity.Error:
                default: return Error(message, propertyName);
            }
        }
    }
}
