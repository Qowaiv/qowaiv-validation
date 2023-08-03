using FluentValidation;
using Qowaiv.Validation.Fluent;
using Specs.Fluent.Models;

namespace Fluent_validation.In_past_specs;

public class Valid_for_in_past
{
    [Test]
    public void Date()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new DateModel { Prop = new Date(2017, 06, 10) }.Should().BeValidFor(new DateInPastValidator());
        }
    }

    [Test]
    public void DateOnly()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new DateOnlyModel { Prop = new DateOnly(2017, 06, 10) }.Should().BeValidFor(new DateOnlyInPastValidator());
        }
    }


    [Test]
    public void DateTime()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new DateTimeModel { Prop = new DateTime(2017, 06, 10, 00, 00, 00, DateTimeKind.Utc) }.Should().BeValidFor(new DateTimeInPastValidator());
        }
    }

    [Test]
    public void Nullable_Date()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateModel { Prop = new Date(2017, 06, 10) }.Should().BeValidFor(new NullableDateInPastValidator());
        }
    }

    [Test]
    public void Nullable_DateOnly()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateOnlyModel { Prop = new DateOnly(2017, 06, 10) }.Should().BeValidFor(new NullableDateOnlyInPastValidator());
        }
    }

    [Test]
    public void Nullable_DateTime()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateTimeModel { Prop = new DateTime(2017, 06, 10, 00, 00, 00, DateTimeKind.Utc) }.Should().BeValidFor(new NullableDateTimeInPastValidator());
        }
    }
}

public class Valid_for_not_set
{
    [Test]
    public void Date()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateModel { Prop = null }.Should().BeValidFor(new NullableDateInPastValidator());
        }
    }

    [Test]
    public void DateOnly()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateOnlyModel { Prop = null }.Should().BeValidFor(new NullableDateOnlyInPastValidator());
        }
    }

    [Test]
    public void DateTime()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateTimeModel { Prop = null }.Should().BeValidFor(new NullableDateTimeInPastValidator());
        }
    }
}

public class Invalid_for_not_in_past
{
    [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the past.", "en-GB")]
    public void Date(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new DateModel { Prop = new Date(2017, 06, 12) }.Should()
                    .BeInvalidFor(new DateInPastValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the past.", "en-GB")]
    public void DateOnly(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new DateOnlyModel { Prop = new DateOnly(2017, 06, 12) }.Should()
                    .BeInvalidFor(new DateOnlyInPastValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the past.", "en-GB")]
    public void DateTime(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new DateTimeModel { Prop = new DateTime(2017, 06, 12, 00, 00, 00, DateTimeKind.Utc) }.Should()
                    .BeInvalidFor(new DateTimeInPastValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the past.", "en-GB")]
    public void Nullable_Date(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new NullableDateModel { Prop = new Date(2017, 06, 12) }.Should()
                    .BeInvalidFor(new NullableDateInPastValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the past.", "en-GB")]
    public void Nullable_DateOnly(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new NullableDateOnlyModel { Prop = new DateOnly(2017, 06, 12) }.Should()
                    .BeInvalidFor(new NullableDateOnlyInPastValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the past.", "en-GB")]
    public void Nullable_DateTime(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = new DateTime(2017, 06, 12, 00, 00, 00, DateTimeKind.Utc) }.Should()
                    .BeInvalidFor(new NullableDateTimeInPastValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }
}

public class DateInPastValidator : ModelValidator<DateModel>
{
    public DateInPastValidator() => RuleFor(m => m.Prop).InPast();
}

public class DateOnlyInPastValidator : ModelValidator<DateOnlyModel>
{
    public DateOnlyInPastValidator() => RuleFor(m => m.Prop).InPast();
}

public class DateTimeInPastValidator : ModelValidator<DateTimeModel>
{
    public DateTimeInPastValidator() => RuleFor(m => m.Prop).InPast();
}

public class NullableDateInPastValidator : ModelValidator<NullableDateModel>
{
    public NullableDateInPastValidator() => RuleFor(m => m.Prop).InPast();
}

public class NullableDateOnlyInPastValidator : ModelValidator<NullableDateOnlyModel>
{
    public NullableDateOnlyInPastValidator() => RuleFor(m => m.Prop).InPast();
}

public class NullableDateTimeInPastValidator : ModelValidator<NullableDateTimeModel>
{
    public NullableDateTimeInPastValidator() => RuleFor(m => m.Prop).InPast();
}
