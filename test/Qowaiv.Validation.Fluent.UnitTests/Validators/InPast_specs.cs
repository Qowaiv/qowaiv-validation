﻿using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using Qowaiv;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using System;
using System.Globalization;

namespace Validators.InPast_specs
{
    public class Valid_for_in_past
    {
        [Test]
        public void Date()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateModel { Prop = new Date(2017, 06, 10) }.Should().BeValidFor(new DateInPastValidator());
            }
        }

        [Test]
        public void DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new DateTimeModel { Prop = new DateTime(2017, 06, 10) }.Should().BeValidFor(new DateTimeInPastValidator());
            }
        }

        [Test]
        public void Nullable_Date()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateModel { Prop = new Date(2017, 06, 10) }.Should().BeValidFor(new NullableDateInPastValidator());
            }
        }

        [Test]
        public void Nullable_DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = new DateTime(2017, 06, 10) }.Should().BeValidFor(new NullableDateTimeInPastValidator());
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
                new NullableDateModel { Prop = null }.Should().BeValidFor(new NullableDateInPastValidator());
            }
        }

        [Test]
        public void DateTime()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                new NullableDateTimeModel { Prop = null }.Should().BeValidFor(new NullableDateTimeInPastValidator());
            }
        }
    }
    public class Invalid_for_not_past
    { 
        [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the past.", "en-GB")]
        public void Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new DateModel { Prop = new Date(2017, 06, 12) }.Should()
                        .BeInvalidFor(new DateInPastValidator())
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
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new DateTimeModel { Prop = new DateTime(2017, 06, 12) }.Should()
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
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new NullableDateModel { Prop = new Date(2017, 06, 12) }.Should()
                        .BeInvalidFor(new NullableDateInPastValidator())
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
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    new NullableDateTimeModel { Prop = new DateTime(2017, 06, 12) }.Should()
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

    public class DateTimeInPastValidator : ModelValidator<DateTimeModel>
    {
        public DateTimeInPastValidator() => RuleFor(m => m.Prop).InPast();
    }

    public class NullableDateInPastValidator : ModelValidator<NullableDateModel>
    {
        public NullableDateInPastValidator() => RuleFor(m => m.Prop).InPast();
    }

    public class NullableDateTimeInPastValidator : ModelValidator<NullableDateTimeModel>
    {
        public NullableDateTimeInPastValidator() => RuleFor(m => m.Prop).InPast();
    }
}