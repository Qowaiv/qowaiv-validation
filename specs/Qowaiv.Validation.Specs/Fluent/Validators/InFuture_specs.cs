using FluentValidation;
using Qowaiv.Validation.Fluent;
using Specs.Fluent.Models;

namespace Fluent_validation.InFuture_specs;

public class Valid_for_in_future
{
    [Test]
    public void Date()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new DateModel { Prop = new Date(2017, 06, 12) }.Should().BeValidFor(new DateInFutureValidator());
        }
    }

    [Test]
    public void DateOnly()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new DateOnlyModel { Prop = new DateOnly(2017, 06, 12) }.Should().BeValidFor(new DateOnlyInFutureValidator());
        }
    }

    [Test]
    public void DateTime()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new DateTimeModel { Prop = new DateTime(2017, 06, 12) }.Should().BeValidFor(new DateTimeInFutureValidator());
        }
    }

    [Test]
    public void Nullable_Date()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new NullableDateModel { Prop = new Date(2017, 06, 12) }.Should().BeValidFor(new NullableDateInFutureValidator());
        }
    }

    [Test]
    public void Nullable_DateOnly()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new NullableDateOnlyModel { Prop = new DateOnly(2017, 06, 12) }.Should().BeValidFor(new NullableDateOnlyInFutureValidator());
        }
    }

    [Test]
    public void Nullable_DateTime()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new NullableDateTimeModel { Prop = new DateTime(2017, 06, 12) }.Should().BeValidFor(new NullableDateTimeInFutureValidator());
        }
    }
}

public class Not_invalid_for_not_set
{
    [Test]
    public void Date()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new NullableDateModel { Prop = null }.Should().BeValidFor(new NullableDateInFutureValidator());
        }
    }

    [Test]
    public void DateOnly()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new NullableDateOnlyModel { Prop = null }.Should().BeValidFor(new NullableDateOnlyInFutureValidator());
        }
    }

    [Test]
    public void DateTime()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new NullableDateTimeModel { Prop = null }.Should().BeValidFor(new NullableDateTimeInFutureValidator());
        }
    }
}

public class Invalid_for_not_in_future
{
    [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the future.", "en-GB")]
    public void Date(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateModel { Prop = new Date(2017, 06, 11) }.Should()
                    .BeInvalidFor(new DateInFutureValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the future.", "en-GB")]
    public void DateOnly(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateOnlyModel { Prop = new DateOnly(2017, 06, 11) }.Should()
                    .BeInvalidFor(new DateOnlyInFutureValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the future.", "en-GB")]
    public void DateTime(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateTimeModel { Prop = new DateTime(2017, 06, 11) }.Should()
                    .BeInvalidFor(new DateTimeInFutureValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the future.", "en-GB")]
    public void Nullable_Date(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateModel { Prop = new Date(2017, 06, 11) }.Should()
                   .BeInvalidFor(new NullableDateInFutureValidator())
                   .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the future.", "en-GB")]
    public void Nullable_DateOnly(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateOnlyModel { Prop = new DateOnly(2017, 06, 11) }.Should()
                    .BeInvalidFor(new NullableDateOnlyInFutureValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }

    [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
    [TestCase("'Prop' should be in the future.", "en-GB")]
    public void Nullable_DateTime(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = new DateTime(2017, 06, 11) }.Should()
                    .BeInvalidFor(new NullableDateTimeInFutureValidator())
                    .WithMessage(ValidationMessage.Error(message, "Prop"));
            }
        }
    }
}

public class DateInFutureValidator : ModelValidator<DateModel>
{
    public DateInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}

public class DateOnlyInFutureValidator : ModelValidator<DateOnlyModel>
{
    public DateOnlyInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}

public class DateTimeInFutureValidator : ModelValidator<DateTimeModel>
{
    public DateTimeInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}

public class NullableDateOnlyInFutureValidator : ModelValidator<NullableDateOnlyModel>
{
    public NullableDateOnlyInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}

public class NullableDateInFutureValidator : ModelValidator<NullableDateModel>
{
    public NullableDateInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}

public class NullableDateTimeInFutureValidator : ModelValidator<NullableDateTimeModel>
{
    public NullableDateTimeInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}
