using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using Qowaiv;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using System;
using System.Globalization;

namespace Validators.NotInPast_specs
{
    public class IsValid_for_in_past
    {
        [Test]
        public void Date()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateModel { Prop = new Date(2017, 06, 11) }.Should().BeValidFor(new DateNotInPastValidator());
            }
        }

        [Test]
        public void DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new DateTime(2017, 06, 11)))
            {
                new DateTimeModel { Prop = new DateTime(2017, 06, 12) }.Should().BeValidFor(new DateTimeNotInPastValidator());
            }
        }

        [Test]
        public void Nullable_Date()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateModel { Prop = new Date(2017, 06, 11) }.Should().BeValidFor(new NullableDateNotInPastValidator());
            }
        }

        [Test]
        public void Nullable_DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new DateTime(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = new DateTime(2017, 06, 12) }.Should().BeValidFor(new NullableDateTimeNotInPastValidator());
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
                new NullableDateModel { Prop = null }.Should().BeValidFor(new NullableDateNotInPastValidator());
            }
        }

        [Test]
        public void DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = null }.Should().BeValidFor(new NullableDateTimeNotInPastValidator());
            }
        }
    }
    public class Invalid_for_not_past
    { 
        [TestCase("'Prop' mag niet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the past.", "en-GB")]
        public void Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new DateModel { Prop = new Date(2017, 06, 10) }.Should()
                        .BeInvalidFor(new DateNotInPastValidator())
                        .WithMessage(ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the past.", "en-GB")]
        public void DateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new DateTimeModel { Prop = new DateTime(2017, 06, 10) }.Should()
                        .BeInvalidFor(new DateTimeNotInPastValidator())
                        .WithMessage(ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the past.", "en-GB")]
        public void Nullable_Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new NullableDateModel { Prop = new Date(2017, 06, 10) }.Should()
                        .BeInvalidFor(new NullableDateNotInPastValidator())
                        .WithMessage(ValidationMessage.Error(message, "Prop")); 
                }
            }
        }

        [TestCase("'Prop' mag niet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the past.", "en-GB")]
        public void Nullable_DateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new NullableDateTimeModel { Prop = new DateTime(2017, 06, 10) }.Should()
                        .BeInvalidFor(new NullableDateTimeNotInPastValidator())
                        .WithMessage(ValidationMessage.Error(message, "Prop"));
                }
            }
        }
    }

    public class DateNotInPastValidator : ModelValidator<DateModel>
    {
        public DateNotInPastValidator() => RuleFor(m => m.Prop).NotInPast();
    }

    public class DateTimeNotInPastValidator : ModelValidator<DateTimeModel>
    {
        public DateTimeNotInPastValidator() => RuleFor(m => m.Prop).NotInPast();
    }

    public class NullableDateNotInPastValidator : ModelValidator<NullableDateModel>
    {
        public NullableDateNotInPastValidator() => RuleFor(m => m.Prop).NotInPast();
    }

    public class NullableDateTimeNotInPastValidator : ModelValidator<NullableDateTimeModel>
    {
        public NullableDateTimeNotInPastValidator() => RuleFor(m => m.Prop).NotInPast();
    }
}
