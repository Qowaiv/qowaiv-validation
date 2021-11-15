﻿namespace Validation.EmptyOrUnknown_specs;

public class Set_value
{
    [Test]
    public void Is_valid()
        => new UnknownModel { Country = Country.NL }
        .Should().BeValidFor(new UnknownModelValidator());
}

public class Unknown
{
    [TestCase("'Country' must not be empty or unknown.", "en-GB")]
    [TestCase("'Country' mag niet leeg of onbekend zijn.", "nl-BE")]
    public void Not_valid(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            new UnknownModel { Country = Country.Unknown }
                .Should().BeInvalidFor(new UnknownModelValidator())
                .WithMessage(ValidationMessage.Error(message, "Country"));
        }
    }

    [Test]
    public void Is_valid_with_warning_serverity()
=> new UnknownWithSeverityModel { Email = EmailAddress.Unknown }
.Should().BeValidFor(new UnknownWithSeverityModelValidator())
.WithMessage(ValidationMessage.Warn("'Email' must not be empty or unknown.", "Email"));
}

public class Empty
{
    [TestCase("'Country' must not be empty or unknown.", "en-GB")]
    [TestCase("'Country' mag niet leeg of onbekend zijn.", "nl-BE")]
    public void Is_not_valid(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            new UnknownModel { Country = Country.Empty }
                .Should().BeInvalidFor(new UnknownModelValidator())
                .WithMessage(ValidationMessage.Error(message, "Country"));
        }
    }

    [Test]
    public void Is_valid_with_warning_serverity()
        => new UnknownWithSeverityModel { Email = EmailAddress.Empty }
        .Should().BeValidFor(new UnknownWithSeverityModelValidator())
        .WithMessage(ValidationMessage.Warn("'Email' must not be empty or unknown.", "Email"));
}
