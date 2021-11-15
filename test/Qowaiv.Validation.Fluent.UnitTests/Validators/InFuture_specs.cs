﻿namespace Validation.InFuture_specs;

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
    public void Nullable_DateTime()
    {
        using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
        {
            new NullableDateTimeModel { Prop = new Date(2017, 06, 12) }.Should().BeValidFor(new NullableDateTimeInFutureValidator());
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
    public void DateTime(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateTimeModel { Prop = new Date(2017, 06, 11) }.Should()
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
    public void Nullable_DateTime(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = new Date(2017, 06, 11) }.Should()
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

public class DateTimeInFutureValidator : ModelValidator<DateTimeModel>
{
    public DateTimeInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}

public class NullableDateInFutureValidator : ModelValidator<NullableDateModel>
{
    public NullableDateInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}

public class NullableDateTimeInFutureValidator : ModelValidator<NullableDateTimeModel>
{
    public NullableDateTimeInFutureValidator() => RuleFor(m => m.Prop).InFuture();
}
