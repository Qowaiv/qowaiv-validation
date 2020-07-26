using System;
using System.Runtime.Serialization;
using System.Text;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Implementation of an <see cref="IValidationMessage"/>.</summary>
    [Serializable]
    public sealed class ValidationMessage : IValidationMessage, ISerializable, IEquatable<ValidationMessage>
    {
        /// <summary>Creates a new instance of a <see cref="ValidationMessage"/>.</summary>
        public ValidationMessage() : this(ValidationSeverity.None, null, null) { }

        internal ValidationMessage(ValidationSeverity severity, string message, string propertyName)
        {
            Severity = severity;
            Message = message;
            PropertyName = propertyName;
        }

        /// <summary>Creates a new instance of <see cref="ValidationMessage"/>.</summary>
        private ValidationMessage(SerializationInfo info, StreamingContext context) :
            this(GetSeverity(info), GetMessage(info), GetProperty(info))
        { }

        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static ValidationSeverity GetSeverity(SerializationInfo info) => (ValidationSeverity)info.GetInt32(nameof(Severity));

        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static string GetMessage(SerializationInfo info) => info.GetString(nameof(Message));
    
        /// <summary>Helper methods to deserialize the <see cref="ValidationMessage"/>.</summary>
        private static string GetProperty(SerializationInfo info) => info.GetString(nameof(PropertyName));

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context)
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

        /// <inheritdoc />
        public override string ToString()
        {
            if(Equals(None))
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            switch (Severity)
            {
                case ValidationSeverity.Info: 
                    sb.Append("INF: ");
                    break;
                case ValidationSeverity.Warning:
                    sb.Append("WRN: ");
                    break;
                case ValidationSeverity.Error:
                    sb.Append("ERR: ");
                    break;
                default:
                    sb.Append($"Severity: ");
                    break;
            }
            if(!string.IsNullOrEmpty(PropertyName))
            {
                sb.Append($"Property: {PropertyName}, ");
            }
            sb.Append(Message);

            return sb.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is ValidationMessage other && Equals(other);

        /// <inheritdoc />
        public bool Equals(ValidationMessage other)
        {
            return other != null
                && Severity == other.Severity
                && PropertyName == other.PropertyName
                && Message == other.Message;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Severity.GetHashCode()
                ^ (PropertyName ?? "").GetHashCode()
                ^ (Message ?? "").GetHashCode();
        }

        /// <summary>Creates a None message.</summary>
        public static ValidationMessage None => new ValidationMessage();

        /// <summary>Creates an error message.</summary>
        public static ValidationMessage Error(string message, string propertyName = null) => new ValidationMessage(ValidationSeverity.Error, message, propertyName);

        /// <summary>Creates a warning message.</summary>
        public static ValidationMessage Warn(string message, string propertyName = null) => new ValidationMessage(ValidationSeverity.Warning, message, propertyName);

        /// <summary>Creates an info message.</summary>
        public static ValidationMessage Info(string message, string propertyName = null) => new ValidationMessage(ValidationSeverity.Info, message, propertyName);

        /// <summary>Creates a validation message.</summary>
        public static IValidationMessage For(ValidationSeverity severity, string message, string propertyName = null)
        {
            switch (severity)
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
