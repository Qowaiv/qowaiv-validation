using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Exception to communicate that <see cref="Result{TModel}.Value"/> has no value (unintentionally).</summary>
    [Serializable]
    public class NoValue : ArgumentNullException
    {
        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        public NoValue() : this(nameof(Result<object>.Value), QowaivValidationMessages.NoValue) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        public NoValue(string paramName) : base(paramName) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        public NoValue(string message, Exception innerException) : base(message, innerException) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        public NoValue(string paramName, string message) : base(paramName, message) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="NoValue"/> class.</summary>
        protected NoValue(SerializationInfo info, StreamingContext context) : base(info, context) => Do.Nothing();

        /// <summary>Creates a new <see cref="NoValue"/> exception for a <see cref="Result{TModel}"/>.</summary>
        /// <typeparam name="T">
        /// The type of the result.
        /// </typeparam>
        [Pure]
        public static NoValue For<T>() => new(nameof(Result<T>.Value), string.Format(QowaivValidationMessages.NoValue_ForT, typeof(T).Name));
    }
}
