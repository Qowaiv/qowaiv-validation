using FluentValidation;
using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class ClockValidationNotInPastTest
    {
        [Test]
        public void NotInPast_DateSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new DateModel { Prop = new Date(2017, 06, 12) };
                FluentValidatorAssert.IsValid<DateNotInPastValidator, DateModel>(model);
            }
        }

        [Test]
        public void NotInPast_DateTimeSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new DateTimeModel { Prop = new Date(2017, 06, 12) };
                FluentValidatorAssert.IsValid<DateTimeNotInPastValidator, DateTimeModel>(model);
            }
        }

        [Test]
        public void NotInPast_NullableDateSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateModel { Prop = new Date(2017, 06, 12) };
                FluentValidatorAssert.IsValid<NullableDateNotInPastValidator, NullableDateModel>(model);
            }
        }

        [Test]
        public void NotInPast_NullableDateTimeSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateTimeModel { Prop = new Date(2017, 06, 12) };
                FluentValidatorAssert.IsValid<NullableDateTimeNotInPastValidator, NullableDateTimeModel>(model);
            }
        }

        [Test]
        public void NotInPast_NullableDateNotSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateModel { Prop = null };
                FluentValidatorAssert.IsValid<NullableDateNotInPastValidator, NullableDateModel>(model);
            }
        }

        [Test]
        public void NotInPast_NullableDateTimeNotSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateTimeModel { Prop = null };
                FluentValidatorAssert.IsValid<NullableDateTimeNotInPastValidator, NullableDateTimeModel>(model);
            }
        }

        [TestCase("'Prop' mag niet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the past.", "en-GB")]
        public void NotInPast_Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new DateModel { Prop = new Date(2017, 06, 10) };

                    FluentValidatorAssert.WithErrors<DateNotInPastValidator, DateModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the past.", "en-GB")]
        public void NotInPast_DateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new DateTimeModel { Prop = new Date(2017, 06, 10) };

                    FluentValidatorAssert.WithErrors<DateTimeNotInPastValidator, DateTimeModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the past.", "en-GB")]
        public void NotInPast_NullableDate(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new NullableDateModel { Prop = new Date(2017, 06, 10) };

                    FluentValidatorAssert.WithErrors<NullableDateNotInPastValidator, NullableDateModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the past.", "en-GB")]
        public void NotInPast_NullableDateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new NullableDateTimeModel { Prop = new Date(2017, 06, 10) };

                    FluentValidatorAssert.WithErrors<NullableDateTimeNotInPastValidator, NullableDateTimeModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }


        public class DateNotInPastValidator : FluentModelValidator<DateModel>
        {
            public DateNotInPastValidator() => RuleFor(m => m.Prop).NotInPast();
        }

        public class DateTimeNotInPastValidator : FluentModelValidator<DateTimeModel>
        {
            public DateTimeNotInPastValidator() => RuleFor(m => m.Prop).NotInPast();
        }

        public class NullableDateNotInPastValidator : FluentModelValidator<NullableDateModel>
        {
            public NullableDateNotInPastValidator() => RuleFor(m => m.Prop).NotInPast();
        }

        public class NullableDateTimeNotInPastValidator : FluentModelValidator<NullableDateTimeModel>
        {
            public NullableDateTimeNotInPastValidator() => RuleFor(m => m.Prop).NotInPast();
        }
    }
}
