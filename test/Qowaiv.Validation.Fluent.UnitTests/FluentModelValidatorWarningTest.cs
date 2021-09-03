using FluentAssertions;
using NUnit.Framework;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.Fluent.UnitTests.Models;

namespace Qowaiv.Validation.Fluent.UnitTests
{
    public class FluentModelValidatorWarningTest
    {
        [Test]
        public void Validate_WarningModel_ValidWithWarningAndInfo()
        {
            var model = new WarningModel();
            IValidator<WarningModel> validator = new WarningModelValidator();

            validator.Validate(model)
                .Should().BeValid().WithMessages(
                    ValidationMessage.Warn("Test warning.", nameof(model.Message)),
                    ValidationMessage.Info("Nice that you validated this.", nameof(model.Message)));
        }
    }

}
