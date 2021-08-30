using System;
using System.Runtime.Serialization;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Message to communicate that the <see cref="Result{TModel}.Value"/> has no value (unintentionally).</summary>
    [Serializable]
    public class NoValue : ArgumentNullException, IValidationMessage
    {
        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        public NoValue() : this("value", QowaivValidationMessages.NoValue) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        public NoValue(string paramName) : base(paramName) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        public NoValue(string message, Exception innerException) : base(message, innerException) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        public NoValue(string paramName, string message) : base(paramName, message) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        protected NoValue(SerializationInfo info, StreamingContext context) : base(info, context) => Do.Nothing();

        /// <inheritdoc />
        public ValidationSeverity Severity => ValidationSeverity.Error;

        /// <inheritdoc />
        public string PropertyName => ParamName;
    }
}
