using Qowaiv;
using Qowaiv.Validation.Abstractions;

namespace FluentValidation
{
    /// <summary>Extensions on <see cref="Severity"/>.</summary>
    public static class SeverityExtensions
    {
        /// <summary>Converts <see cref="Severity"/> to <see cref="ValidationSeverity"/>.</summary>
        public static ValidationSeverity ToValidationSeverity(this Severity severity)
        {
            Guard.DefinedEnum(severity, nameof(severity));

            return severity switch
            {
                Severity.Error => ValidationSeverity.Error,
                Severity.Warning => ValidationSeverity.Warning,
                Severity.Info => ValidationSeverity.Info,
                _ => ValidationSeverity.None,
            };
        }
    }
}
