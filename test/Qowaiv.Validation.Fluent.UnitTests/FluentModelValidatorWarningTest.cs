using NUnit.Framework;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;

namespace Qowaiv.Validation.Fluent.UnitTests
{
    public class FluentModelValidatorWarningTest
    {
        [Test]
        public void Validate_WarningModel_ValidWithWarningAndInfo()
        {
            var model = new WarningModel();
            IValidator<WarningModel> validator = new WarningModelValidator();

            var result = validator.Validate(model);

            ValidationMessageAssert.IsValid(result,
                ValidationMessage.Warn("Test warning.", nameof(model.Message)),
                ValidationMessage.Info("Nice that you validated this.", nameof(model.Message)));
        }
    }

}
