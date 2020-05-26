using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.DataAnnotations.UnitTests.Mocks;

namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models
{
    public class ModelThatReturnsNoneMessage
    {
        [MockedValidation(null, ValidationSeverity.None)]
        public string Value { get; set; }
    }
}
