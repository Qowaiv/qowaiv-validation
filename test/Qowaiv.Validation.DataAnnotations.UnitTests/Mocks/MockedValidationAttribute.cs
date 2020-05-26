using Qowaiv.Validation.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Qowaiv.Validation.DataAnnotations.UnitTests.Mocks
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MockedValidationAttribute : ValidationAttribute
    {
        public MockedValidationAttribute(string message, ValidationSeverity severity = ValidationSeverity.Error)
        {
            Message = message;
            Severity = severity;
        }

        public string Message { get; }
        public ValidationSeverity Severity { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = (ValidationMessage)ValidationMessage.For(Severity, Message, new[] { validationContext?.MemberName });
            return message;
        }
    }
}
