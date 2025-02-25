using FluentValidation;
using Qowaiv.Validation.Fluent;
using Specs.Fluent.Models;

namespace Fluent_validation.InFuture_specs;

public class Valid_for_in_future
{
    [Test]
    public void Date()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new DateModel { Prop = new Date(2017, 06, 12) }.ShouldBeValidFor(new DateInFutureValidator());
        }
    }

    [Test]
    public void DateOnly()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new DateOnlyModel { Prop = new DateOnly(2017, 06, 12) }.ShouldBeValidFor(new DateOnlyInFutureValidator());
        }
    }

    [Test]
    public void DateTime()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new DateTimeModel { Prop = new DateTime(2017, 06, 12, 00, 00, 00, DateTimeKind.Utc) }.ShouldBeValidFor(new DateTimeInFutureValidator());
        }
    }

    [Test]
    public void Nullable_Date()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateModel { Prop = new Date(2017, 06, 12) }.ShouldBeValidFor(new NullableDateInFutureValidator());
        }
    }

    [Test]
    public void Nullable_DateOnly()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateOnlyModel { Prop = new DateOnly(2017, 06, 12) }.ShouldBeValidFor(new NullableDateOnlyInFutureValidator());
        }
    }

    [Test]
    public void Nullable_DateTime()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateTimeModel { Prop = new DateTime(2017, 06, 12, 00, 00, 00, DateTimeKind.Utc) }.ShouldBeValidFor(new NullableDateTimeInFutureValidator());
        }
    }
}

public class Not_invalid_for_not_set
{
    [Test]
    public void Date()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateModel { Prop = null }.ShouldBeValidFor(new NullableDateInFutureValidator());
        }
    }

    [Test]
    public void DateOnly()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateOnlyModel { Prop = null }.ShouldBeValidFor(new NullableDateOnlyInFutureValidator());
        }
    }

    [Test]
    public void DateTime()
    {
        using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
        {
            new NullableDateTimeModel { Prop = null }.ShouldBeValidFor(new NullableDateTimeInFutureValidator());
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
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new DateModel { Prop = new Date(2017, 06, 11) }
                    .ShouldBeInvalidFor(new DateInFutureValidator())
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
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new DateOnlyModel { Prop = new DateOnly(2017, 06, 11) }
                    .ShouldBeInvalidFor(new DateOnlyInFutureValidator())
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
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new DateTimeModel { Prop = new DateTime(2017, 06, 11, 00, 00, 00, DateTimeKind.Utc) }
                    .ShouldBeInvalidFor(new DateTimeInFutureValidator())
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
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new NullableDateModel { Prop = new Date(2017, 06, 11) }
                    .ShouldBeInvalidFor(new NullableDateInFutureValidator())
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
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new NullableDateOnlyModel { Prop = new DateOnly(2017, 06, 11) }
                    .ShouldBeInvalidFor(new NullableDateOnlyInFutureValidator())
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
            using (Clock.SetTimeForCurrentContext(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = new DateTime(2017, 06, 11, 00, 00, 00, DateTimeKind.Utc) }
                    .ShouldBeInvalidFor(new NullableDateTimeInFutureValidator())
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
