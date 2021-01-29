using Qowaiv.Validation.Abstractions;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Qowaiv.Validation.Messages
{
    /// <summary>Message to communicate concurrency control failed.</summary>
    [Serializable]
    public class ConcurrencyIssue : InvalidOperationException, IValidationMessage
    {
        /// <summary>Initializes a new instance of the <see cref="ConcurrencyIssue"/> class.</summary>
        public ConcurrencyIssue()
            : base(ValidationMessages.ConcurrencyIssue) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="ConcurrencyIssue"/> class.</summary>
        protected ConcurrencyIssue(SerializationInfo info, StreamingContext context) : base(info, context) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="ConcurrencyIssue"/> class.</summary>
        public ConcurrencyIssue(string message) : base(message) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="ConcurrencyIssue"/> class.</summary>
        public ConcurrencyIssue(string message, Exception innerException) : base(message, innerException) => Do.Nothing();

        /// <inheritdoc />
        public ValidationSeverity Severity => ValidationSeverity.Error;

        /// <inheritdoc />
        public string PropertyName => null;

        /// <summary>Creates an <see cref="ConcurrencyIssue"/> for the version mismatch.</summary>
        public static ConcurrencyIssue VersionMismatch(object expectedVersion, object actualVersion)
            => new ConcurrencyIssue(string.Format(
                CultureInfo.CurrentCulture,
                ValidationMessages.VersionMismatch,
                expectedVersion,
                actualVersion));

        /// <summary>Creates an <see cref="ConcurrencyIssue"/> for mid-air collision.</summary>
        public static ConcurrencyIssue MidAirCollision()
            => new ConcurrencyIssue(ValidationMessages.MidAirCollision);
    }
}
