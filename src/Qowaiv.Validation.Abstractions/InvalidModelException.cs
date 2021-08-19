using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Represents an exception that is shown once tried to access the invalid model of a <see cref="Result{T}"/>.</summary>
    [Serializable]
    public class InvalidModelException : InvalidOperationException
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
        public InvalidModelException() => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
        public InvalidModelException(string message)
            : base(message) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
        public InvalidModelException(string message, Exception innerException)
            : base(message, innerException) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
        public InvalidModelException(string message, Exception innerException, IEnumerable<IValidationMessage> messages)
            : this(message, innerException)
            => Errors = new ReadOnlyCollection<IValidationMessage>(messages.Where(e => e.Severity >= ValidationSeverity.Error).ToArray());

        /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
        protected InvalidModelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Guard.NotNull(info, nameof(info));

            var errors = info.GetValue(nameof(Errors), typeof(IValidationMessage[])) as IValidationMessage[];
            Errors = new ReadOnlyCollection<IValidationMessage>(errors);
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Guard.NotNull(info, nameof(info));
            base.GetObjectData(info, context);
            info.AddValue(nameof(Errors), Errors.ToArray());
        }

        /// <summary>The related validation error(s).</summary>
        public IReadOnlyList<IValidationMessage> Errors { get; } = new ReadOnlyCollection<IValidationMessage>(new IValidationMessage[0]);

        /// <summary>Creates an <see cref="InvalidModelException"/> for the model.</summary>
        public static InvalidModelException For<T>(IEnumerable<IValidationMessage> messages)
        {
            var sb = new StringBuilder().AppendFormat(QowaivValidationMessages.InvalidModelException, typeof(T));
            
            foreach(var message in messages ?? Array.Empty<IValidationMessage>())
            {
                if (!string.IsNullOrEmpty(message.Message))
                {
                    sb.Append("* ");
                    var lines = message.Message.Split(NewLine, StringSplitOptions.None);
                    sb.AppendLine(lines[0]);
                    foreach(var line in lines.Skip(1))
                    {
                        sb.Append("  ").AppendLine(line);
                    }
                }
            }
            return new InvalidModelException(sb.ToString(), null, messages);
        }
        private static readonly string[] NewLine = new[] { "\r\n", "\n" };
    }
}
