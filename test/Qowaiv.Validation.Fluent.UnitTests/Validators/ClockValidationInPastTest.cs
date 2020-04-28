using FluentValidation;
using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class ClockValidationInPastTest
    {
        [Test]
        public void InPast_DateSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new DateModel { Prop = new Date(2017, 06, 10) };
                FluentValidatorAssert.IsValid<DateInPastValidator, DateModel>(model);
            }
        }

        [Test]
        public void InPast_DateTimeSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new DateTimeModel { Prop = new Date(2017, 06, 10) };
                FluentValidatorAssert.IsValid<DateTimeInPastValidator, DateTimeModel>(model);
            }
        }

        [Test]
        public void InPast_NullableDateSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateModel { Prop = new Date(2017, 06, 10) };
                FluentValidatorAssert.IsValid<NullableDateInPastValidator, NullableDateModel>(model);
            }
        }

        [Test]
        public void InPast_NullableDateTimeSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateTimeModel { Prop = new Date(2017, 06, 10) };
                FluentValidatorAssert.IsValid<NullableDateTimeInPastValidator, NullableDateTimeModel>(model);
            }
        }

        [Test]
        public void InPast_NullableDateNotSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateModel { Prop = null };
                FluentValidatorAssert.IsValid<NullableDateInPastValidator, NullableDateModel>(model);
            }
        }

        [Test]
        public void InPast_NullableDateTimeNotSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateTimeModel { Prop = null };
                FluentValidatorAssert.IsValid<NullableDateTimeInPastValidator, NullableDateTimeModel>(model);
            }
        }

        [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the past.", "en-GB")]
        public void InPast_Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new DateModel { Prop = new Date(2017, 06, 12) };

                    FluentValidatorAssert.WithErrors<DateInPastValidator, DateModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the past.", "en-GB")]
        public void InPast_DateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new DateTimeModel { Prop = new Date(2017, 06, 12) };

                    FluentValidatorAssert.WithErrors<DateTimeInPastValidator, DateTimeModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the past.", "en-GB")]
        public void InPast_NullableDate(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new NullableDateModel { Prop = new Date(2017, 06, 12) };

                    FluentValidatorAssert.WithErrors<NullableDateInPastValidator, NullableDateModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' moet in het verleden liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the past.", "en-GB")]
        public void InPast_NullableDateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new NullableDateTimeModel { Prop = new Date(2017, 06, 12) };

                    FluentValidatorAssert.WithErrors<NullableDateTimeInPastValidator, NullableDateTimeModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }


        public class DateInPastValidator : FluentModelValidator<DateModel>
        {
            public DateInPastValidator() => RuleFor(m => m.Prop).InPast();
        }

        public class DateTimeInPastValidator : FluentModelValidator<DateTimeModel>
        {
            public DateTimeInPastValidator() => RuleFor(m => m.Prop).InPast();
        }

        public class NullableDateInPastValidator : FluentModelValidator<NullableDateModel>
        {
            public NullableDateInPastValidator() => RuleFor(m => m.Prop).InPast();
        }

        public class NullableDateTimeInPastValidator : FluentModelValidator<NullableDateTimeModel>
        {
            public NullableDateTimeInPastValidator() => RuleFor(m => m.Prop).InPast();
        }
    }
}
