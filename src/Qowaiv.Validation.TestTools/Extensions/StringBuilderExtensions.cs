using Qowaiv.Validation.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAssertions.Qowaiv
{
    internal static class StringBuilderExtensions
    {
        public static StringBuilder AppendMessages(this StringBuilder sb, IEnumerable<IValidationMessage> messages)
        {
            foreach (var message in messages.OrderByDescending(m => m.Severity))
            {
                sb.AppendLine().AppendMessage(message);
            }
            return sb;
        }

        public static StringBuilder AppendMessage(this StringBuilder sb, IValidationMessage message)
        {
            sb.AppendFormat("- {0,-7} ", message.Severity.ToString().ToUpperInvariant())
                .Append(message.Message);

            return string.IsNullOrEmpty(message.PropertyName)
                ? sb
                : sb.Append(" Prop: ").AppendLine(message.PropertyName);
        }
    }
}
