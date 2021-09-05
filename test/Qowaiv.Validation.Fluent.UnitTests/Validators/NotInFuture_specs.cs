using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using Qowaiv;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using System;
using System.Globalization;

namespace Validation.NotInFuture_specs
{
    public class IsValid_for_not_in_future
    {
        [Test]
        public void Date()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateModel { Prop = new Date(2017, 06, 10) }.Should().BeValidFor(new DateNotInFutureValidator());
            }
        }

        [Test]
        public void DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateTimeModel { Prop = new DateTime(2017, 06, 10) }.Should().BeValidFor(new DateTimeNotInFutureValidator());
            }
        }

        [Test]
        public void Nullable_Date()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateModel { Prop = new Date(2017, 06, 10) }.Should().BeValidFor(new NullableDateNotInFutureValidator());
            }
        }

        [Test]
        public void Nullable_DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = new Date(2017, 06, 10) }.Should().BeValidFor(new NullableDateTimeNotInFutureValidator());
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
                new NullableDateModel { Prop = null }.Should().BeValidFor(new NullableDateNotInFutureValidator());
            }
        }

        [Test]
        public void DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = null }.Should().BeValidFor(new NullableDateTimeNotInFutureValidator());
            }
        }
    }
    
    public class IsInvalid_for_in_future
    {
        [TestCase("'Prop' mag niet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the future.", "en-GB")]
        public void Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new DateModel { Prop = new Date(2017, 06, 12) }.Should()
                        .BeInvalidFor(new DateNotInFutureValidator())
                        .WithMessage(ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the future.", "en-GB")]
        public void DateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new DateTimeModel { Prop = new Date(2017, 06, 12) }.Should()
                        .BeInvalidFor(new DateTimeNotInFutureValidator())
                        .WithMessage(ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the future.", "en-GB")]
        public void Nullable_Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                     new NullableDateModel { Prop = new Date(2017, 06, 12) }.Should()
                        .BeInvalidFor(new NullableDateNotInFutureValidator())
                        .WithMessage(ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the future.", "en-GB")]
        public void Nullable_DateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new NullableDateTimeModel { Prop = new Date(2017, 06, 12) }.Should()
                        .BeInvalidFor(new NullableDateTimeNotInFutureValidator())
                        .WithMessage(ValidationMessage.Error(message, "Prop"));
                }
            }
        }
    }

    public class DateNotInFutureValidator : ModelValidator<DateModel>
    {
        public DateNotInFutureValidator() => RuleFor(m => m.Prop).NotInFuture();
    }

    public class DateTimeNotInFutureValidator : ModelValidator<DateTimeModel>
    {
        public DateTimeNotInFutureValidator() => RuleFor(m => m.Prop).NotInFuture();
    }

    public class NullableDateNotInFutureValidator : ModelValidator<NullableDateModel>
    {
        public NullableDateNotInFutureValidator() => RuleFor(m => m.Prop).NotInFuture();
    }

    public class NullableDateTimeNotInFutureValidator : ModelValidator<NullableDateTimeModel>
    {
        public NullableDateTimeNotInFutureValidator() => RuleFor(m => m.Prop).NotInFuture();
    }
}
