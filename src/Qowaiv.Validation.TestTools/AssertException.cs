using System;
using System.Runtime.Serialization;

namespace Qowaiv.Validation.TestTools
{
    /// <summary>Assert exception.</summary>
    /// <remarks>
    /// Exists to be independent to external test frameworks.
    /// </remarks>
    [Serializable]
    public class AssertException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="AssertException"/> class.</summary>
        public AssertException() : this("Assertion failed.") => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="AssertException"/> class.</summary>
        public AssertException(string message) : base(message) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="AssertException"/> class.</summary>
        public AssertException(string message, Exception innerException) : base(message, innerException) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="AssertException"/> class.</summary>
        protected AssertException(SerializationInfo info, StreamingContext context) : base(info, context) => Do.Nothing();
    }
}
